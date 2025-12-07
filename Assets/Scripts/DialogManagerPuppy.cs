using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueManagerPuppy : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    public GameObject choicesPanel;
    public Button optionYesButton;
    public Button optionNoButton;
    
    private PuppyEnter puppy;
    private string[] dialogues = {
        "Woof! Woof!",
        "Alex: Oh, it's a puppy!",
        "What should I do?"
    };
    private int currentDialogueIndex = 0;
    private bool hasInteracted = false;

    void Start()
    {
        dialogueBox.SetActive(false);
        choicesPanel.SetActive(false);

        puppy = FindFirstObjectByType<PuppyEnter>();

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

    StartCoroutine(DelayedPuppyResponse(isHelpful)); // Start coroutine with delay
}
IEnumerator DelayedPuppyResponse(bool isHelpful)
{
    yield return new WaitForSeconds(1f); // Wait for 1 second
    StartCoroutine(PuppyResponse(isHelpful)); // Properly start the coroutine
}


    IEnumerator PuppyResponse(bool isHelpful)
{
    choicesPanel.SetActive(false);

    string responseText = isHelpful ? "The dog is very happy." : "The dog became sad and ran away.";
    dialogueText.text = ""; // Clear previous text

    foreach (char letter in responseText.ToCharArray())
    {
        dialogueText.text += letter;
        yield return new WaitForSeconds(0.05f); // Adjust speed if needed
    }

    yield return new WaitForSeconds(1.5f);

    dialogueBox.SetActive(false);
    dialogueText.text = ""; // Clear text
    puppy.ExitScene();

}




}
