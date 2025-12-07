using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameOverManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI dialogueText;
    public Button restartButton;

    [Header("Dialogue Settings")]
    public float typingSpeed = 0.05f;

    private string[] dialogueLines = {
        "Mom: Alex, we need to talk.",
        "Your actions today weren’t kind or respectful.",
        "Every choice we make affects the people around us.",
        "Let’s try again, and make better choices this time, okay?"
    };

    private int currentLineIndex = 0;
    private bool isTyping = false;

    void Start()
    {
        dialogueText.text = "";

        if (restartButton != null)
        {
            restartButton.gameObject.SetActive(false);
            restartButton.onClick.RemoveAllListeners();
            restartButton.onClick.AddListener(RestartGame);
        }

        if (dialogueLines.Length > 0)
            StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            ShowNextLine();
        }
    }

    void ShowNextLine()
    {
        currentLineIndex++;

        if (currentLineIndex < dialogueLines.Length)
        {
            StartCoroutine(TypeLine(dialogueLines[currentLineIndex]));
        }
        else
        {
            if (restartButton != null)
                restartButton.gameObject.SetActive(true);
        }
    }

    IEnumerator TypeLine(string line)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    void RestartGame()
    {
        if (PointsManager.Instance != null)
            PointsManager.Instance.ResetPoints();

        SceneFader fader = FindObjectOfType<SceneFader>();
        if (fader != null)
        {
            fader.FadeToScene(2);
        }
        else
        {
            Debug.LogWarning("SceneFader not found. Loading scene directly.");
            SceneManager.LoadScene(2);
        }
    }

}
