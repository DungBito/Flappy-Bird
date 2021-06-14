using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Observer;
using System;

public class PlayerController : MonoBehaviour {
    #region Init, Config
    [Header("Config")]
    [SerializeField] float jumpForce;
    [SerializeField] float rotationSmoothing;

    //Component
    private Rigidbody2D rigid;

    //Variables
    private Quaternion downRotation;
    private Quaternion fowardRotation;
    private Quaternion startRotation;

    private void Awake() {
        rigid = GetComponent<Rigidbody2D>();

        downRotation = Quaternion.Euler(0, 0, -90);
        fowardRotation = Quaternion.Euler(0, 0, 50);
        startRotation = Quaternion.Euler(0, 0, 0);

        GameUIStateManager.GameStateChanged += Player_StateChanged;
    }
    #endregion

    #region Update
    private void Update() {
        if (GameUIStateManager.CurrentState == GameUIState.Ingame) {
            Jump();
            Rotate();
        }
    }
    #endregion

    #region Changed State UI Function
    private void Player_StateChanged() {
        switch (GameUIStateManager.CurrentState) {
            case GameUIState.None:
                break;
            case GameUIState.Main:
                rigid.gravityScale = 0;
                rigid.velocity = Vector3.zero;
                transform.position = Vector3.zero;
                transform.rotation = startRotation;
                rigid.constraints = RigidbodyConstraints2D.FreezeAll;
                break;
            case GameUIState.BeforePlay:
                rigid.constraints = RigidbodyConstraints2D.None;
                break;
            case GameUIState.Ingame:
                rigid.gravityScale = 2;
                break;
            case GameUIState.GameOver:
                break;
            default:
                break;
        }
    }
    #endregion

    #region Other Functions
    private void Jump() {
        if (Input.GetMouseButtonDown(0)) {
            transform.rotation = fowardRotation;
            rigid.velocity = Vector3.zero;
            rigid.AddForce(jumpForce * Vector2.up, ForceMode2D.Impulse);
        }
    }

    private void Rotate() {
        transform.rotation = Quaternion.Lerp(transform.rotation, downRotation, rotationSmoothing * Time.deltaTime);
    }
    #endregion

    #region Collision
    private void OnCollisionEnter2D(Collision2D collision) {
        this.PostEvent(EventID.OnPlayerDead);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        this.PostEvent(EventID.OnPlusScore);
    }
    #endregion
}
