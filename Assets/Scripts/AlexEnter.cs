using UnityEngine;

public class AlexEnter : MonoBehaviour
{
    public float moveSpeed = 2f;
    public DialogueController dialogueController; // Assign in Inspector

    private Vector3 startPosition = new Vector3(-11.07f, -2.71f, 0f);
    private Vector3 targetPosition = new Vector3(-5.36f, -2.58f, 0f);

    private bool hasReached = false;

    void Start()
    {
        transform.position = startPosition;
    }

    void Update()
    {
        if (!hasReached)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
            {
                hasReached = true;

                if (dialogueController != null)
                {
                    int currentCandies = 0;
                    if (PointsManager.Instance != null)
                        currentCandies = PointsManager.Instance.candyCount;

                    dialogueController.StartDialogue(currentCandies);
                }
            }
        }
    }
}
