using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    #region Component
    private Rigidbody2D rigid2D;
    #endregion

    #region Player Variables
    [SerializeField] float jumpForce = 500f;
    int playerScore;
    #endregion

    #region UI
    [SerializeField] Text txtScore;
    [SerializeField] Text txtGameOverScore;
    #endregion

    #region Other Variables
    [SerializeField] float rotationSmoothing = 1.5f;
    Quaternion downRotation;
    Quaternion fowardRotation;
    Quaternion startRotation;
    #endregion

    #region Unity Callback Functions
    private void Start()
    {
        rigid2D = GetComponent<Rigidbody2D>();
        downRotation = Quaternion.Euler(0, 0, -90);
        fowardRotation = Quaternion.Euler(0, 0, 45);
        startRotation = Quaternion.Euler(0, 0, 0);
        GameUIStateManager.GameStateChanged += Player_StateChanged;
    }

    private void Update()
    {
        if (GameUIStateManager.CurrentState == GameUIState.Ingame)
        {
            Jump();
            Rotate();
        }
    }
    #endregion

    #region Changed State UI Function
    private void Player_StateChanged()
    {
        switch (GameUIStateManager.CurrentState)
        {
            case GameUIState.None:
                break;
            case GameUIState.Idle:
                playerScore = 0;
                txtScore.text = playerScore.ToString();
                transform.position = Vector3.zero;
                transform.rotation = startRotation;
                break;
            case GameUIState.BeforePlay:
                break;
            case GameUIState.Ingame:
                rigid2D.gravityScale = 3;
                break;
            case GameUIState.GameOver:
                break;
            default:
                break;
        }
    }
    #endregion

    #region Other Functions
    private void Jump()
    {
        if (Input.GetMouseButtonDown(0))
        {
            transform.rotation = fowardRotation;
            rigid2D.velocity = Vector3.zero;
            rigid2D.AddForce(jumpForce * Vector2.up, ForceMode2D.Force);
        }
    }

    private void Rotate()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, rotationSmoothing * Time.deltaTime);
    }
    #endregion

    #region Collision Functions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        rigid2D.gravityScale = 0;
        rigid2D.velocity = Vector3.zero;
        txtGameOverScore.text = playerScore.ToString();
        GameUIStateManager.CurrentState = GameUIState.GameOver;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerScore++;
        txtScore.text = playerScore.ToString();
    }
    #endregion
}
