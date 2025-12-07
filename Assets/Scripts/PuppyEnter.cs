using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PuppyEnter : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 targetPosition;
    private DialogueManagerPuppy dialogueManager;
    private bool hasTriggeredDialogue = false;
    private bool isExiting = false;
    public Image fadeImage; // assign this in the Inspector
    public float fadeDuration = 1.5f;

    void Start()
    {
        transform.position = new Vector3(2.37f, -6.58f, 0f); // Start position (offscreen)
        targetPosition = new Vector3(-0.63f, -2.97f, 0f); // Stop position inside the scene

        dialogueManager = FindFirstObjectByType<DialogueManagerPuppy>();
    }

    void Update()
    {
        if (!isExiting) 
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            
            if (transform.position == targetPosition && !hasTriggeredDialogue)
            {
                hasTriggeredDialogue = true;
                dialogueManager?.StartDialogue();
            }
        }
        else // Move Mia out of the scene
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(2.37f, -6.58f, 0f), speed * Time.deltaTime);
            
            if (transform.position.y <= -6.5f) // When Mia is off-screen, destroy her
            {
                LoadNextScene(); // ✅ Correctly starts the coroutine
            }
        }
    }

    public void ExitScene()
    {
        if (!isExiting)
        {
            isExiting = true;
            targetPosition = new Vector3(11.51f, -1.7f, 0f);
        }
    }

    void LoadNextScene()
    {
        StartCoroutine(FadeAndLoadScene());
    }

    IEnumerator FadeAndLoadScene()
    {
        float timer = 0f;
        Color startColor = fadeImage.color;
        Color endColor = new Color(0, 0, 0, 1); // Fully black

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            yield return null;
        }

        SceneManager.LoadScene(13);
    }
}
