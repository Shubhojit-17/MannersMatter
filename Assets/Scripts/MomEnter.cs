using UnityEngine;
using System.Collections;

public class MomEnter : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 targetPosition;

    public GameObject dialogueBox;  // Assign in Inspector
    public GameObject dialogBoxImg; // Assign in Inspector

    public DialogueManager dialogueManager; // Assign in Inspector

    void Start()
    {
        transform.position = new Vector3(16.2f, -0.96f, 0f);
        targetPosition = new Vector3(8.49f, -1.2f, 0f);

        if (dialogueBox != null) dialogueBox.SetActive(false);
        if (dialogBoxImg != null) dialogBoxImg.SetActive(false);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            StartCoroutine(ShowDialogueUIAndStartDialogue(0.5f));
            enabled = false;
        }
    }

    IEnumerator ShowDialogueUIAndStartDialogue(float delay)
    {
        yield return new WaitForSeconds(delay);

        if (dialogueBox != null) dialogueBox.SetActive(true);
        if (dialogBoxImg != null) dialogBoxImg.SetActive(true);

        if (dialogueManager != null)
        {
            dialogueManager.StartDialogue();
        }
    }
}
