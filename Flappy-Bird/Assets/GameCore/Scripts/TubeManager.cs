using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeManager : MonoBehaviour {
    #region Init, Config
    [Header("Config")]
    [SerializeField] float timeSpawn;
    [SerializeField] float heightPipe;

    //Variable
    private Vector2 workSpace;
    private float lastTimeSpawn;
    #endregion

    #region Update
    private void Update() {
        if (GameUIStateManager.CurrentState == GameUIState.Ingame) {
            if (Time.time > lastTimeSpawn + timeSpawn) {
                SpawnPipe();
                lastTimeSpawn = Time.time;
            }
        }
    }
    #endregion

    #region Other Functions
    private void SpawnPipe() {
        var tube = Pooler.Instance.SpawnFromPool("Tube", transform.position, transform.rotation);
        workSpace.Set(transform.position.x, Random.Range(-heightPipe, heightPipe + 3f));
        tube.transform.position = workSpace;
    }
    #endregion
}
