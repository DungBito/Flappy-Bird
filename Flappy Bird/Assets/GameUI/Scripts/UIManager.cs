using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Game UI Panels
    [SerializeField] GameObject mainScreenPanel;
    [SerializeField] GameObject beforePlayPanel;
    [SerializeField] GameObject playPanel;
    [SerializeField] GameObject gameOverPanel;
    #endregion

    #region Unity Callback Functions
    private void Awake()
    {
        GameUIStateManager.GameStateChanged += UI_StateChanged;
    }
    #endregion

    #region Btn Click Functions
    public void Btn_PlayMainClick()
    {
        GameUIStateManager.CurrentState = GameUIState.BeforePlay;
    }

    public void Btn_MenuClick()
    {
        GameUIStateManager.CurrentState = GameUIState.Idle;
    }

    public void Btn_PlayClick()
    {
        GameUIStateManager.CurrentState = GameUIState.Ingame;
    }
    #endregion

    #region UI Changed State
    private void UI_StateChanged()
    {
        switch (GameUIStateManager.CurrentState)
        {
            case GameUIState.None:
                break;
            case GameUIState.Idle:
                Time.timeScale = 1;
                mainScreenPanel?.SetActive(true);
                beforePlayPanel?.SetActive(false);
                playPanel?.SetActive(false);
                gameOverPanel?.SetActive(false);
                break;
            case GameUIState.BeforePlay:
                mainScreenPanel?.SetActive(false);
                beforePlayPanel?.SetActive(true);
                playPanel?.SetActive(false);
                gameOverPanel?.SetActive(false);
                break;
            case GameUIState.Ingame:
                mainScreenPanel?.SetActive(false);
                beforePlayPanel?.SetActive(false);
                playPanel?.SetActive(true);
                gameOverPanel?.SetActive(false);
                break;
            case GameUIState.GameOver:
                Time.timeScale = 0;
                mainScreenPanel?.SetActive(false);
                beforePlayPanel?.SetActive(false);
                playPanel?.SetActive(false);
                gameOverPanel?.SetActive(true);
                break;
            default:
                break;
        }
    }
    #endregion
}
