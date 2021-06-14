using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeController : MonoBehaviour {
    #region Init, Config
    [Header("Config")]
    [SerializeField] float moveSpeed;
    [SerializeField] float timeAddPool;

    void OnEnable() {
        StartCoroutine(DoAddPool());
    }
    #endregion

    #region Update
    void Update() {
        transform.position += moveSpeed * Time.deltaTime * Vector3.left;
        if (GameUIStateManager.CurrentState == GameUIState.Main) {
            Pooler.Instance.AddToPool("Tube", gameObject);
        }
    }
    #endregion

    #region Coroutine
    IEnumerator DoAddPool() {
        yield return new WaitForSeconds(timeAddPool);
        Pooler.Instance.AddToPool("Tube", gameObject);
    }
    #endregion
}
