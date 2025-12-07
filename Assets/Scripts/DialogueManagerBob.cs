using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueManagerBob : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public GameObject dialogueBox;
    public GameObject choicesPanel;
    public Button optionYesButton;
    public Button optionNoButton;
    
    private BobEnter bob;
    private string[] dialogues = {
        "Alex, I broke my crayon.",
        "Can I use your crayon?"
    };
    private int currentDialogueIndex = 0;
    private bool hasInteracted = false;

    void Start()
    {
        dialogueBox.SetActive(false);
        choicesPanel.SetActive(false);

        bob = FindFirstObjectByType<BobEnter>();

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

    StartCoroutine(DelayedBobResponse(isHelpful)); // Start coroutine with delay
}
IEnumerator DelayedBobResponse(bool isHelpful)
{
    yield return new WaitForSeconds(1f); // Wait for 1 second
    StartCoroutine(BobResponse(isHelpful)); // Properly start the coroutine
}


    IEnumerator BobResponse(bool isHelpful)
{
    choicesPanel.SetActive(false);

    string responseText = isHelpful ? "Thank you, Alex. You are my best friend." : "What will I do now?";
    dialogueText.text = ""; // Clear previous text

    foreach (char letter in responseText.ToCharArray())
    {
        dialogueText.text += letter;
        yield return new WaitForSeconds(0.05f); // Adjust speed if needed
    }

    yield return new WaitForSeconds(1.5f);

    dialogueBox.SetActive(false);
    dialogueText.text = ""; // Clear text
    bob.ExitScene();

}




}
