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
            SFXManager.instance.PlayMenuSFX();

            if (currentGameState != GameState.GamePaused)
            {
                prevGameState = currentGameState;
                ChangeGameState(GameState.GamePaused);
            }
            else
            {
                PauseMenu.SetActive(false);
                ChangeGameState(prevGameState);
            }
        }

        if (currentGameState == GameState.DrawPathPhase)
        {
            WinScreen.SetActive(false);
            LoseScreen.SetActive(false);

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (drawnPathIsClosed)
                    ChangeGameState(GameState.ExplorationPhase);
                else
                    Debug.Log("path is not a closed loop yet!");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SFXManager.instance.CancelPathSFX();
                Events.Level.Reset?.Invoke();
            }
        }

        if (currentGameState == GameState.ExplorationPhase)
        {
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
    void LoseGame()
    {
        ChangeGameState(GameState.GameLoss);
    }
}

