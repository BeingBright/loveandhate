using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransfer : MonoBehaviour
{
    public string sceneName;
    public float delayInSeconds = 10f;

    public TMP_Text restartText;

    private void Start()
    {
        StartCoroutine(TransferToScene(sceneName));
        StartCoroutine(UpdateText());
    }

    private IEnumerator TransferToScene(string scene)
    {
        yield return new WaitForSeconds(delayInSeconds);
        SceneManager.LoadScene(scene);
    }

    private IEnumerator UpdateText()
    {
        for (int i = 1; i < delayInSeconds; i++)
        {
            yield return new WaitForSeconds(1);
            restartText.text = $"Restart ({delayInSeconds - i})";
        }
    }


    public void RestartGame()
    {
        SceneManager.LoadScene(sceneName);
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}