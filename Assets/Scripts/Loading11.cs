using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class Loading11 : MonoBehaviour
{
    public TextMeshProUGUI textBox;  // Assign this in the Inspector
    public float typingSpeed = 0.05f; // Speed of letter appearance

    private string description = "The next scene is in your sister's room, and she has a request...";

    void Start()
    {
        StartCoroutine(DisplayAndLoadScene());
    }

    IEnumerator DisplayAndLoadScene()
    {
        textBox.text = ""; // Clear text before starting

        foreach (char letter in description.ToCharArray())
        {
            textBox.text += letter;  // Add each letter one by one
            yield return new WaitForSeconds(typingSpeed); // Delay for effect
        }

        // Wait for a short delay after text finishes before changing scene
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Level1Scenario1");
    }
}
