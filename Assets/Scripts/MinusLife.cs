using UnityEngine;

public class MinusLife : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BallController ballController = collision.GetComponent<BallController>();
        if (ballController != null)
        {
            GameManager.Instance.RemoveLives(1);
        }
    }
}
