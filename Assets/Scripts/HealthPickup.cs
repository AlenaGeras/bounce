using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [SerializeField] private int livesToAdd = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        BallController ballController = collision.GetComponent<BallController>();
        if (ballController != null)
        {

            GameManager.Instance.AddLives(livesToAdd);


            Destroy(gameObject);
        }
    }
}