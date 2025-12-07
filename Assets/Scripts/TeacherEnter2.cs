using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class TeacherEnter2 : MonoBehaviour
{
    public float speed = 2f;
    private Vector3 targetPosition;
    private DialogueManagerTeacher2 dialogueManager;
    private bool hasTriggeredDialogue = false;
    private bool isExiting = false;
    public Image fadeImage; // assign this in the Inspector
    public float fadeDuration = 1.5f;

    void Start()
    {
        transform.position = new Vector3(11.75f, -5.3f, 0f); 
        targetPosition = new Vector3(4.95f, -1.83f, 0f); 

        dialogueManager = FindFirstObjectByType<DialogueManagerTeacher2>(); 
    }

    void Update()
    {
        if (!isExiting) 
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            
            if (Vector3.Distance(transform.position, targetPosition) < 0.01f && !hasTriggeredDialogue) // Fixed precision issue
            {
                hasTriggeredDialogue = true;
                dialogueManager?.StartDialogue();
            }
        }
        else // Move Mia out of the scene
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(11.75f, -5.3f, 0f), speed * Time.deltaTime);
            
            if (transform.position.x >= 11f) // When Mia is off-screen, destroy her
            {
                LoadNextScene();
            }
        }
    }

    public void ExitScene()
    {
        if (!isExiting)
        {
            isExiting = true;
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

        SceneManager.LoadScene(9);
}
}
