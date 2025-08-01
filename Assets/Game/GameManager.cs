using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { DrawPathPhase, ExplorationPhase, GameLoss, GameWin }
    public GameState currentGameState;
    bool drawnPathIsClosed = false;
    List<MissingPart> foundParts = new();
    int missingPartsInGame = 0;
    [SerializeField] float gameSpeed = 2;
    void OnEnable()
    {
        Events.Level.PathDrawnIsClosedLoop += SetPathIsClosedToTrue;
        Events.Level.PathDrawnIsOpen += SetPathIsClosedToFalse;
        Events.Level.LoopComplete += SetPathIsClosedToFalse;
        Events.Level.LoopComplete += ExitExplorationState;
        Events.Level.LoopComplete += InitiateLaunchCutscene;

        Events.Level.CollectedMissingPart += AddMissingPart;
        Events.Level.MissingPartsGenerated += SetNumberOfMissingParts;
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
    }

    void Update()
    {
        if (currentGameState == GameState.DrawPathPhase)
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (drawnPathIsClosed)
                    ChangeGameState(GameState.ExplorationPhase);
                else
                    Debug.Log("path is not a closed loop yet!");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Events.Level.LoopComplete?.Invoke();
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
        switch (newState)
        {
            case GameState.DrawPathPhase:
                break;
            case GameState.ExplorationPhase:
                Events.Level.StartLoop?.Invoke();
                break;
            case GameState.GameLoss:
                break;
            case GameState.GameWin:
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
    }

    void SetNumberOfMissingParts(int _amt)
    {
        missingPartsInGame = _amt;
    }

    void InitiateLaunchCutscene()
    {
        if (foundParts.Count >= missingPartsInGame)
        {
            ChangeGameState(GameState.GameWin);
        }
    }
}

