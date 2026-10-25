using UnityEngine;

public class Goal : MonoBehaviour
{
    [Range(1, 2)][SerializeField] private int teamNumber = 1; // The team number that this goal belongs to
    private int health = 3;

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Ball"))
        {
            health--;
            if (health > 0)
            {
                Debug.Log("Goal for Team " + teamNumber + " hit! Remaining health: " + health);
            }
            else
            {
                health = 1;
                GameManager.Instance.OnGoalScored(teamNumber);
            }
        }
    }
}
