using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { DrawPathPhase, ExplorationPhase }
    public GameState currentGameState;
    bool drawnPathIsClosed = false;
    void OnEnable()
    {
        Events.Level.PathDrawnIsClosedLoop += SetPathIsClosedToTrue;
        Events.Level.PathDrawnIsOpen += SetPathIsClosedToFalse;
        Events.Level.LoopComplete += SetPathIsClosedToFalse;
        Events.Level.LoopComplete += ExitExplorationState;
    }
    void OnDisable()
    {
        Events.Level.PathDrawnIsClosedLoop -= SetPathIsClosedToTrue;
        Events.Level.PathDrawnIsOpen -= SetPathIsClosedToFalse;
        Events.Level.LoopComplete -= SetPathIsClosedToFalse;
        Events.Level.LoopComplete -= ExitExplorationState;
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
}

