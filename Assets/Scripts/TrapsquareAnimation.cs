using UnityEngine;

public class TrapsquareAnimation : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float height;
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    public void SetParametrs(float speed, float height)
    {
        this.speed = speed;
        this.height = height;
    }

    void FixedUpdate()
    {
        float newY = startPos.y + Mathf.PingPong(Time.time * speed, height * 2) - height;
        transform.position = new Vector3(startPos.x, newY, startPos.z);
    }
}