using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Observer;

public class GameUIManager : MonoBehaviour {
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject beforePlayPanel;
    [SerializeField] GameObject playPanel;
    [SerializeField] GameObject gameOverPanel;

    [SerializeField] Text ingameScore;
    [SerializeField] Text gameOverScore;
    [SerializeField] Text gameOverBestScore;

    //Variables
    private Vector2 mainPanelStartPos;
    private Vector2 gameOverPanelStartPos;
    private int score;
    private int bestScore;

    private void Awake() {
        GameUIStateManager.GameStateChanged += UI_StateChanged;

        this.RegisterListener(EventID.OnPlayerDead, (param) => OnPlayerDead());
        this.RegisterListener(EventID.OnPlusScore, (param) => OnPlusScore());
    }

    private void Start() {
        mainPanelStartPos = mainPanel.transform.localPosition;
        gameOverPanelStartPos = gameOverPanel.transform.localPosition;
    }

    public void OnPlayClick() {
        mainPanel.transform.position = mainPanelStartPos;
        GameUIStateManager.CurrentState = GameUIState.BeforePlay;
    }

    public void OnTapClick() {
        GameUIStateManager.CurrentState = GameUIState.Ingame;
    }

    public void OnMenuClick() {
        gameOverPanel.transform.position = gameOverPanelStartPos;
        GameUIStateManager.CurrentState = GameUIState.Main;
    }

    private void OnPlayerDead() {
        bestScore = PlayerPrefs.GetInt("BestScore");
        if (score > bestScore) {
            PlayerPrefs.SetInt("BestScore", score);
            bestScore = score;
        }
        gameOverBestScore.text = bestScore.ToString();
        gameOverScore.text = score.ToString();
        GameUIStateManager.CurrentState = GameUIState.GameOver;
    }

    private void OnPlusScore() {
        score++;
        ingameScore.text = score.ToString();
    }

    private void UI_StateChanged() {
        switch (GameUIStateManager.CurrentState) {
            case GameUIState.None:
                mainPanel.SetActive(false);
                beforePlayPanel.SetActive(false);
                playPanel.SetActive(false);
                gameOverPanel.SetActive(false);
                break;
            case GameUIState.Main:
                Time.timeScale = 1;
                score = 0;
                mainPanel.SetActive(true);
                mainPanel.transform.DOLocalMove(Vector2.zero, .5f).SetUpdate(true);
                beforePlayPanel.SetActive(false);
                playPanel.SetActive(false);
                gameOverPanel.SetActive(false);
                break;
            case GameUIState.BeforePlay:
                Time.timeScale = 1;
                mainPanel.SetActive(false);
                beforePlayPanel.SetActive(true);
                playPanel.SetActive(false);
                gameOverPanel.SetActive(false);
                break;
            case GameUIState.Ingame:
                mainPanel.SetActive(false);
                beforePlayPanel.SetActive(false);
                playPanel.SetActive(true);
                gameOverPanel.SetActive(false);
                break;
            case GameUIState.GameOver:
                Time.timeScale = 0;
                mainPanel.SetActive(false);
                beforePlayPanel.SetActive(false);
                playPanel.SetActive(false);
                gameOverPanel.SetActive(true);
                gameOverPanel.transform.DOLocalMove(Vector2.zero, .5f).SetUpdate(true);
                break;
            default:
                break;
        }
    }
}
