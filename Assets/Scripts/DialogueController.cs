using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueController : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI dialogueText;
    public Image candyProgressBar;
    public GameObject candyBarParent;
    public GameObject endCanvas;
    public Button restartButton;
    public Button quitButton;

    [Header("Typing Settings")]
    public float typingSpeed = 0.05f;
    public float fillSpeed = 1f;

    [Header("Audio")]
    public AudioSource progressAudio;

    private string[] dialogueLines = {
        "Mom: Alex, you’ve come a long way!",
        "Let’s take a look at your progress."
    };

    private string finalCompliment = "";
    private bool isTyping = false;
    private bool isInCompliment = false;
    private bool complimentShown = false;
    private int currentLine = 0;

    private int maxCandies = 12;
    private int currentCandies = 0;

    void Start()
    {
        dialogueText.text = "";
        candyProgressBar.fillAmount = 0f;
        candyBarParent.SetActive(false);
        endCanvas.SetActive(false);

        restartButton.onClick.AddListener(RestartGame);
        quitButton.onClick.AddListener(QuitGame);
    }

    public void StartDialogue(int candies)
    {
        currentCandies = Mathf.Clamp(candies, 0, maxCandies);
        StartCoroutine(TypeLine(dialogueLines[currentLine]));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            if (isInCompliment && complimentShown)
            {
                endCanvas.SetActive(true);
                isInCompliment = false;
            }
            else
            {
                NextLine();
            }
        }
    }

    void NextLine()
    {
        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            StartCoroutine(TypeLine(dialogueLines[currentLine]));
        }
        else
        {
            StartCoroutine(ShowCandyProgress());
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

    IEnumerator ShowCandyProgress()
    {
        candyBarParent.SetActive(true);
        float targetFill = (float)currentCandies / maxCandies;

        // 🎵 Start playing the audio
        if (progressAudio != null)
        {
            progressAudio.Play();
        }

        while (candyProgressBar.fillAmount < targetFill)
        {
            candyProgressBar.fillAmount += Time.deltaTime * fillSpeed;
            yield return null;
        }

        // 🎵 Stop the audio
        if (progressAudio != null && progressAudio.isPlaying)
        {
            progressAudio.Stop();
        }

        yield return new WaitForSeconds(1f);
        candyBarParent.SetActive(false);

        yield return StartCoroutine(ShowCompliment());
    }


    IEnumerator ShowCompliment()
    {
        if (currentCandies == maxCandies)
            finalCompliment = "Mom: Wow! A perfect score! I'm so proud of you!";
        else if (currentCandies >= 8)
            finalCompliment = "Mom: Great job, Alex! Just a few steps away from perfect!";
        else if (currentCandies >= 4)
            finalCompliment = "Mom: Not bad! Let’s aim higher next time!";
        else
            finalCompliment = "Mom: I know you can do better with some more effort!";

        isInCompliment = true;
        complimentShown = false;

        yield return TypeLine(finalCompliment);
        complimentShown = true;
    }

    void RestartGame()
    {
        if (PointsManager.Instance != null)
        {
            PointsManager.Instance.ResetPoints();
        }

        SceneManager.LoadScene(2);
    }

    void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
