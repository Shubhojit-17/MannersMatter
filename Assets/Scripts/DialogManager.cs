using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public float typingSpeed = 0.5f;

    public GameObject candyCanvas;  // Assign in Inspector
    public GameObject mannerCanvas; // Assign in Inspector

    private string[] dialogues = {
        "(Press Space to continue)",
        "Mom: Alex, the way we treat others is important.",
        "This is your Manners Meter — it shows how respectful, kind, and helpful you're being.",
        "The more thoughtful your actions, the higher your manners bar goes!",
        "Now, here’s something sweet...",
        "This is your sweet Meter — you earn candies when you do good things!",
        "The more good deeds you do, the more candy you collect.",
        "Ready to see how well you handle different situations today? Let's go!"
    };

    private int index = 0;
    private bool isTyping = false;
    private bool dialogueStarted = false;

    void Start()
    {
        dialogueText.text = "";

        if (candyCanvas != null) candyCanvas.SetActive(false);
        if (mannerCanvas != null) mannerCanvas.SetActive(false);
    }

    void Update()
    {
        if (!dialogueStarted) return;

        if (Input.GetKeyDown(KeyCode.Space) && !isTyping)
        {
            NextDialogue();
        }
    }

    public void StartDialogue()
    {
        index = 0;
        dialogueStarted = true;
        StartCoroutine(DisplayTextLetterByLetter(dialogues[index]));
    }

    void NextDialogue()
    {
        index++;

        // Show relevant canvases before describing them
        if (index == 2 && mannerCanvas != null)
        {
            mannerCanvas.SetActive(true);
        }

        if (index == 5 && candyCanvas != null)
        {
            candyCanvas.SetActive(true);
        }

        if (index < dialogues.Length)
        {
            StartCoroutine(DisplayTextLetterByLetter(dialogues[index]));
        }
        else
        {
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator DisplayTextLetterByLetter(string sentence)
    {
        isTyping = true;
        dialogueText.text = "";

        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        isTyping = false;
    }

    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(2); // Make sure Scene 2 is in Build Settings
    }
}
