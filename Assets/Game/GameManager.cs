using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public enum GameState { DrawPathPhase, ExplorationPhase }
    public GameState currentGameState;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ChangeGameState(GameState.ExplorationPhase);
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
}

