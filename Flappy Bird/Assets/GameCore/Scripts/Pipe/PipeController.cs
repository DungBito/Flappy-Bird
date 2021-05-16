using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    #region Pipe Variables
    [SerializeField] float moveSpeed = 3f;
    [SerializeField] float timeDestroyPipe = 5f;
    #endregion

    #region Unity Callback Functions
    void Start()
    {
        StartCoroutine(DoDestroy());
    }

    void Update()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;
    }
    #endregion

    #region Coroutine
    IEnumerator DoDestroy()
    {
        yield return new WaitForSeconds(timeDestroyPipe);
        Destroy(gameObject);
    }
    #endregion
}
