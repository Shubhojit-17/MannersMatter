using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueManagerTeacher2 : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    public GameObject choicesPanel;
    public Button optionYesButton;
    public Button optionNoButton;
    
    private TeacherEnter2 teach;
    private string[] dialogues = {
        "Good morning, students.",
        "Today we will learn about simple multiplications.",
        "Can anyone tell me what is 4 times 3?"
    };
    private int currentDialogueIndex = 0;
    private bool hasInteracted = false;

    void Start()
    {
        dialogueBox.SetActive(false);
        choicesPanel.SetActive(false);

        teach = FindFirstObjectByType<TeacherEnter2>();

        optionYesButton.onClick.AddListener(() => ChoiceSelected(true));
        optionNoButton.onClick.AddListener(() => ChoiceSelected(false));
    }

    public void StartDialogue()
    {
        if (!hasInteracted) 
        {
            hasInteracted = true;
            dialogueBox.SetActive(true);
            choicesPanel.SetActive(false);
            currentDialogueIndex = 0;
            StartCoroutine(DisplayDialogue());
        }
    }

    IEnumerator DisplayDialogue()
    {
        while (currentDialogueIndex < dialogues.Length)
        {
            dialogueText.text = "";
            foreach (char letter in dialogues[currentDialogueIndex])
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.05f);
            }

            yield return new WaitForSeconds(0.5f);

            currentDialogueIndex++;

            if (currentDialogueIndex < dialogues.Length)
            {
                yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));
            }
        }

        ShowChoices();
    }

    void ShowChoices()
    {
        choicesPanel.SetActive(true);
    }

    public void ChoiceSelected(bool isHelpful)
    {
        MannersMeter mannersMeter = FindFirstObjectByType<MannersMeter>();

        if (mannersMeter != null)
        {
            mannersMeter.ChangeMannerScore(isHelpful ? 10 : -10);
        }

        StartCoroutine(DelayedTeacherResponse(isHelpful));
    }

    IEnumerator DelayedTeacherResponse(bool isHelpful)
    {
        yield return new WaitForSeconds(1f);
        StartCoroutine(TeacherResponse(isHelpful));
    }

    IEnumerator TeacherResponse(bool isHelpful)
{
    choicesPanel.SetActive(false);

    string responseText = isHelpful ? "Yes, Alex what is your answer?" : "Alex, raise your hand first!";
    dialogueText.text = ""; // Clear previous text

    foreach (char letter in responseText.ToCharArray())
    {
        dialogueText.text += letter;
        yield return new WaitForSeconds(0.05f); // Adjust speed if needed
    }

    if(responseText == "Yes, Alex what is your answer?")
    {
        string replyText = "The answer is 12, teacher.";
        dialogueText.text = ""; 
        foreach (char letter in replyText.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.05f); 
        }
    }

    yield return new WaitForSeconds(1.5f);

    dialogueBox.SetActive(false);
    dialogueText.text = ""; // Clear text
    teach.ExitScene();
}

}
