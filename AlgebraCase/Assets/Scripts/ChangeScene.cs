using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    [Header("Change Scene Parameters")]
    public int countdown;
    public int sceneIndex;

    private void Start()
    {
        StartCoroutine("LoadSceneWithTime", countdown);
    }

    private IEnumerator LoadSceneWithTime(int countdown)
    {
        yield return new WaitForSeconds(countdown);
        SceneManager.LoadScene(sceneIndex);
    }
}
