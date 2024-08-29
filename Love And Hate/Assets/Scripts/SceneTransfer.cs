using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    public string sceneName;
    public float delayInSeconds = 10f;

    private void Start()
    {
        StartCoroutine(TransferToScene(sceneName));
    }

    private IEnumerator TransferToScene(string scene)
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(scene);
    }
}