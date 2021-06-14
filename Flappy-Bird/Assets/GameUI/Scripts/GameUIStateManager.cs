using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUIStateManager : MonoBehaviour {
    #region Singleton
    public static GameUIStateManager Instance { get; private set; }

    private void Awake() {
        Instance = this;
    }
    #endregion

    #region Game State Variables
    public static System.Action GameStateChanged;

    [SerializeField] GameUIState currentState = GameUIState.None;

    public static GameUIState CurrentState {
        get => Instance.currentState;
        set {
            if (Instance.currentState != value) {
                Instance.currentState = value;
                GameStateChanged.Invoke();
            }
        }
    }
    #endregion

    #region Unity Callback Functions
    private void Start() {
        CurrentState = GameUIState.Main;
    }
    #endregion
}
