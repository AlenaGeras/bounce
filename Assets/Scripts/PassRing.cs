using UnityEngine;

public class PassRing : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        BallController ballController = collision.GetComponent<BallController>();
        if (ballController != null)
        {
            GameManager.Instance.RemoveRing();
            Destroy(gameObject);
        }
    }
}
