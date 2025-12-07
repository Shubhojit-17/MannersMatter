using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class MannersMeter : MonoBehaviour
{
    public Image mannerBarImage;
    public Image candyBarImage;

    public float fillSpeed = 2f;

    public GameObject positiveCanvas;
    public GameObject negativeCanvas;

    public AudioSource audioSource;
    public AudioClip positiveSound;
    public AudioClip negativeSound;

    private float targetMannerFill;
    private float targetCandyFill;

    private Gradient colorGradient;
    private const int maxCandies = 12;
    public Image fadeImage;
    public float fadeDuration = 1f;

    void Start()
    {
        SetupColorGradient();
        LoadMannerScore();
        LoadCandyCount();
        SetInitialBars();
    }

    void Update()
    {
        // Smooth fill for manner bar
        if (Mathf.Abs(mannerBarImage.fillAmount - targetMannerFill) > 0.001f)
        {
            mannerBarImage.fillAmount = Mathf.Lerp(mannerBarImage.fillAmount, targetMannerFill, Time.deltaTime * fillSpeed);
            UpdateMannerColor();
        }

        // Smooth fill for candy bar
        if (Mathf.Abs(candyBarImage.fillAmount - targetCandyFill) > 0.001f)
        {
            candyBarImage.fillAmount = Mathf.Lerp(candyBarImage.fillAmount, targetCandyFill, Time.deltaTime * fillSpeed);
        }
    }

    public void ChangeMannerScore(int value)
    {
        if (PointsManager.Instance == null)
        {
            Debug.LogWarning("PointsManager instance is missing. ChangeMannerScore will be ignored.");
            return;
        }

        PointsManager.Instance.mannerPoints += value;
        PointsManager.Instance.mannerPoints = Mathf.Clamp(PointsManager.Instance.mannerPoints, 0, 100);
        targetMannerFill = PointsManager.Instance.mannerPoints / 100f;

        if (value > 0)
        {
            AddCandy(); // Increase candy only on positive points
            ShowCanvas(positiveCanvas);
            PlaySound(positiveSound);
        }
        else if (value < 0)
        {
            ShowCanvas(negativeCanvas);
            PlaySound(negativeSound);
        }

        // Check for game over condition (manner points = 0)
        if (PointsManager.Instance.mannerPoints == 0)
        {
            Invoke(nameof(LoadGameOverScene), 1f); // Optional delay
        }
    }

    void LoadGameOverScene()
    {
        StartCoroutine(FadeAndLoadScene(14)); // 14 is your Game Over scene
    }

    IEnumerator FadeAndLoadScene(int sceneIndex)
    {
        float timer = 0f;
        Color startColor = fadeImage.color;
        Color endColor = new Color(0, 0, 0, 1); // fully black

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            fadeImage.color = Color.Lerp(startColor, endColor, timer / fadeDuration);
            yield return null;
        }

        SceneManager.LoadScene(sceneIndex);
    }



    void AddCandy()
    {
        if (PointsManager.Instance == null)
        {
            Debug.LogWarning("PointsManager instance is missing. AddCandy will be ignored.");
            return;
        }

        if (PointsManager.Instance.candyCount < maxCandies)
        {
            PointsManager.Instance.candyCount++;
            targetCandyFill = (float)PointsManager.Instance.candyCount / maxCandies;
        }
    }

    void SetInitialBars()
    {
        if (PointsManager.Instance != null)
            targetMannerFill = PointsManager.Instance.mannerPoints / 100f;
        mannerBarImage.fillAmount = targetMannerFill;
        UpdateMannerColor();
        if (PointsManager.Instance != null)
            targetCandyFill = (float)PointsManager.Instance.candyCount / maxCandies;
        candyBarImage.fillAmount = targetCandyFill;
    }

    void LoadMannerScore()
    {
        if (PointsManager.Instance != null)
        {
            targetMannerFill = PointsManager.Instance.mannerPoints / 100f;
        }
    }

    void LoadCandyCount()
    {
        if (PointsManager.Instance != null)
        {
            targetCandyFill = (float)PointsManager.Instance.candyCount / maxCandies;
        }
    }

    void SetupColorGradient()
    {
        colorGradient = new Gradient();

        GradientColorKey[] colorKeys = new GradientColorKey[3];
        colorKeys[0].color = Color.red;
        colorKeys[0].time = 0.0f;

        colorKeys[1].color = Color.yellow;
        colorKeys[1].time = 0.5f;

        colorKeys[2].color = Color.green;
        colorKeys[2].time = 1.0f;

        GradientAlphaKey[] alphaKeys = new GradientAlphaKey[2];
        alphaKeys[0].alpha = 1.0f;
        alphaKeys[0].time = 0.0f;
        alphaKeys[1].alpha = 1.0f;
        alphaKeys[1].time = 1.0f;

        colorGradient.SetKeys(colorKeys, alphaKeys);
    }

    void UpdateMannerColor()
    {
        mannerBarImage.color = colorGradient.Evaluate(mannerBarImage.fillAmount);
    }

    void ShowCanvas(GameObject canvas)
    {
        canvas.SetActive(true);
        Invoke(nameof(HideCanvases), 1f);
    }

    void HideCanvases()
    {
        positiveCanvas.SetActive(false);
        negativeCanvas.SetActive(false);
    }

    void PlaySound(AudioClip clip)
    {
        if (audioSource != null && clip != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
