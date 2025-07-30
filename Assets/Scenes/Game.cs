using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Game : MonoBehaviour
{
    public enum GameState {
        Play,
        Win,
        Lose
    }
    public GameState gameState;
    public UnityEvent OnLevelComplete;
    void Start()
    {
        StartLevel();
    }
    void Update() {
        switch(gameState) {
            case GameState.Play:
                
                break;
            case GameState.Win:
                OnLevelComplete?.Invoke();
                break;
            case GameState.Lose:
                OnLevelComplete?.Invoke();
                break;
        }
    }
    public void StartLevel() {
        gameState = GameState.Play;    
    }
    public void WinLevel() {
        gameState = GameState.Win;    
    }
    public void LoseLevel() {
        gameState = GameState.Lose;    
    }
}
