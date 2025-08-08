using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { DrawPathPhase, ExplorationPhase, GameLoss, GameWin, GamePaused }
    public GameState currentGameState;
    public GameState prevGameState;
    bool drawnPathIsClosed = false;
    List<MissingPart> foundParts = new();
    int missingPartsInGame = 0;
    [SerializeField] float gameSpeed = 2;

    public GoalUI goalUI;
    public PhaseDisplayUI phaseUI;
    public Teleporter teleporter;

    [SerializeField] GameObject WinScreen, LoseScreen, PauseMenu;
    void OnEnable()
    {
        Events.Level.PathDrawnIsClosedLoop += SetPathIsClosedToTrue;
        Events.Level.PathDrawnIsOpen += SetPathIsClosedToFalse;
        Events.Level.LoopComplete += SetPathIsClosedToFalse;
        Events.Level.LoopComplete += ExitExplorationState;
        Events.Level.LoopComplete += InitiateLaunchCutscene;

        Events.Level.CollectedMissingPart += AddMissingPart;
        Events.Level.MissingPartsGenerated += SetNumberOfMissingParts;

        Events.Health.AllHealthLost += LoseGame;
    }
    void OnDisable()
    {
        Events.Level.PathDrawnIsClosedLoop -= SetPathIsClosedToTrue;
        Events.Level.PathDrawnIsOpen -= SetPathIsClosedToFalse;
        Events.Level.LoopComplete -= SetPathIsClosedToFalse;
        Events.Level.LoopComplete -= ExitExplorationState;
        Events.Level.LoopComplete -= InitiateLaunchCutscene;

        Events.Level.CollectedMissingPart -= AddMissingPart;
        Events.Level.MissingPartsGenerated -= SetNumberOfMissingParts;

        Events.Health.AllHealthLost -= LoseGame;
    }
    void Start()
    {
        Time.timeScale = 1f;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (currentGameState == GameState.DrawPathPhase)
        {
            WinScreen.SetActive(false);
            LoseScreen.SetActive(false);

            if (Input.GetKeyDown(KeyCode.T))
            {
                if (!teleporter.teleporterActive)
                {
                    teleporter.ActivateTeleportMode();
                    Events.Level.Reset?.Invoke();
                }
                else
                {
                    teleporter.DeactivateTeleportMode();
                }
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (drawnPathIsClosed)
                {
                    ChangeGameState(GameState.ExplorationPhase);
                }
                else
                {
                    phaseUI.DisplayError();
                    Debug.Log("path is not a closed loop yet!");
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                SFXManager.instance.PlayUndoSFX();
                Events.Level.Reset?.Invoke();

                SetPathIsClosedToFalse();
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //undo last path drawn
                Events.Level.Undo?.Invoke();

                SFXManager.instance.PlayUndoSFX();
            }
        }

        if (currentGameState == GameState.ExplorationPhase)
        {
            phaseUI.UpdateText("Exploration");

            Time.timeScale = 1f;
            //hold space to speed up the game
            if (Input.GetKey(KeyCode.Space))
            {
                Time.timeScale = gameSpeed;
            }
        }
    }

    void ChangeGameState(GameState newState)
    {
        prevGameState = currentGameState;

        switch (newState)
        {
            case GameState.DrawPathPhase:
                break;
            case GameState.ExplorationPhase:
                if (prevGameState != GameState.GamePaused)
                    Events.Level.StartLoop?.Invoke();
                break;
            case GameState.GameLoss:
                LoseScreen.SetActive(true);
                break;
            case GameState.GameWin:
                WinScreen.SetActive(true);
                break;
            case GameState.GamePaused:
                PauseMenu.SetActive(true);
                Time.timeScale = 0f;
                break;
        }
        currentGameState = newState;
    }

    void SetPathIsClosedToTrue()
    {
        drawnPathIsClosed = true;
    }
    void SetPathIsClosedToFalse()
    {
        drawnPathIsClosed = false;
    }

    void ExitExplorationState()
    {
        phaseUI.UpdateText("Planning");
        ChangeGameState(GameState.DrawPathPhase);
    }

    void AddMissingPart(MissingPart foundPart)
    {
        if (foundParts.Contains(foundPart))
            return;

        Debug.Log("found part");
        foundParts.Add(foundPart);

        goalUI.foundPartsRemaining = missingPartsInGame - foundParts.Count;
    }

    void SetNumberOfMissingParts(int _amt)
    {
        missingPartsInGame = _amt;

        goalUI.foundPartsRemaining = missingPartsInGame - foundParts.Count;
    }

    void InitiateLaunchCutscene()
    {
        if (foundParts.Count >= missingPartsInGame)
        {
            ChangeGameState(GameState.GameWin);
        }
    }
    void LoseGame(int i)
    {
        GameOverScreen gameOverScreen = LoseScreen.GetComponent<GameOverScreen>();
        gameOverScreen.causeOfDeath = i;

        ChangeGameState(GameState.GameLoss);
    }

    public void PauseGame()
    {
        SFXManager.instance.PlayMenuSFX();

        if (currentGameState != GameState.GamePaused)
        {
            prevGameState = currentGameState;
            ChangeGameState(GameState.GamePaused);
        }
        else
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1f;
            ChangeGameState(prevGameState);
        }
    }
}

