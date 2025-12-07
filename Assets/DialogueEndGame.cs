using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueEndGame : MonoBehaviour
{
    [Header("References")]
    public TextMeshProUGUI dialogueText;
    public Image candyProgressBar;
    public GameObject candyBarParent; // parent object of the bar, so we can disable it
    public float typingSpeed = 0.05f;

    private string[] dialogueLines = {
        "Mom: Alex, you’ve come a long way!",
        "Let’s take a look at your progress."
    };

    private int currentLine = 0;
    private bool isTyping = false;

    private int maxCandies = 12;
    private int currentCandies = 0; // this will be set based on score
    public float fillSpeed = 1f;

    void Start()
    {
        dialogueText.text = "";
        candyProgressBar.fillAmount = 0f;
        candyBarParent.SetActive(false);
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
            NextLine();
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
            // Show candy progress after dialogue
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

        while (candyProgressBar.fillAmount < targetFill)
        {
            candyProgressBar.fillAmount += Time.deltaTime * fillSpeed;
            yield return null;
        }

        yield return new WaitForSeconds(1f);

        candyBarParent.SetActive(false);

        // Show final compliment
        yield return StartCoroutine(ShowCompliment());
    }

    IEnumerator ShowCompliment()
    {
        string compliment;

        if (currentCandies == maxCandies)
            compliment = "Mom: Wow! A perfect score! I'm so proud of you!";
        else if (currentCandies >= 8)
            compliment = "Mom: Great job, Alex! Just a few steps away from perfect!";
        else if (currentCandies >= 4)
            compliment = "Mom: Not bad! Let’s aim higher next time!";
        else
            compliment = "Mom: I know you can do better with some more effort!";

        yield return TypeLine(compliment);
    }
}
