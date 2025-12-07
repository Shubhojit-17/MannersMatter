using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class StartGame : MonoBehaviour
{
    public float delayBeforeLoading = 1f; // Delay in seconds

    public void StartTheGame()
    {
        StartCoroutine(LoadLevelWithDelay());
    }

    IEnumerator LoadLevelWithDelay()
    {
        yield return new WaitForSeconds(delayBeforeLoading);
        SceneManager.LoadScene("Introduction"); // Change "Level1" if needed
    }
}
