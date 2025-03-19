using UnityEngine;

public class Portal : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (GameManager.Instance.CurrentRings == 0)
                GameManager.Instance.LoadNextLevel();
        }

    }
}
