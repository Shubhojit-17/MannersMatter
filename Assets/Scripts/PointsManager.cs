using UnityEngine;

public class PointsManager : MonoBehaviour
{
    public static PointsManager Instance;
    public int mannerPoints = 20;
    public int candyCount = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ResetPoints()
    {
        mannerPoints = 20;
        candyCount = 0;
    }
}
