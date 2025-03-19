using UnityEngine;

public class OpenPortal : MonoBehaviour
{
    public Animation anim;
    private bool isOpened = false;

    public void OnTriggerStay2D(Collider2D other)
    {
        if (!isOpened && other.CompareTag("Player"))
        {
            if (GameManager.Instance.CurrentRings == 0)
            {
                anim.Play();
                isOpened = true;
            }
        }
    }
}
