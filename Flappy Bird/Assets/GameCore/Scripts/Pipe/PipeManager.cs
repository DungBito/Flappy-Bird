using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeManager : MonoBehaviour
{
    #region Pipe Variables
    [SerializeField] float timeSpawn = 1f;
    [SerializeField] float heightPipe = 3f;
    [SerializeField] GameObject pipePrefab;
    float timer;
    #endregion

    #region Unity Callback Functions
    private void Start()
    {
        GameUIStateManager.GameStateChanged += Pipe_StateChanged;
    }

    private void Update()
    {
        if (GameUIStateManager.CurrentState == GameUIState.Ingame)
        {
            if (Time.time > timer + timeSpawn)
            {
                SpawnPipe();
                timer = Time.time;
            }
        }
    }
    #endregion

    #region Changed State UI Function
    private void Pipe_StateChanged()
    {
        if (GameUIStateManager.CurrentState == GameUIState.Idle)
        {
            IdleUIState();
        }
    }
    #endregion

    #region Other Functions
    private void SpawnPipe()
    {
        var pipeClone = Instantiate(pipePrefab, transform);
        pipeClone.transform.position = new Vector3(transform.position.x, Random.Range(-heightPipe, heightPipe + 1f));
    }

    private void IdleUIState()
    {
        var allPipe = GetComponentsInChildren<PipeController>();
        foreach (var pipe in allPipe)
        {
            Destroy(pipe.gameObject);
        }
    }
    #endregion
}
