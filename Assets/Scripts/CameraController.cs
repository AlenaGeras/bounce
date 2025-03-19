using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance { get; private set; }

    [SerializeField] private Transform ballTransform;
    [SerializeField, Range(0f, 1f)] private float rightEdgeThreshold = 0.8f;
    [SerializeField, Range(0f, 1f)] private float leftEdgeThreshold = 0.2f;
    [SerializeField] private float cameraOffsetX = 0.1f;

    [SerializeField] private float smoothTime = 0.8f;

    private Camera mainCamera;
    private Vector3 velocity = Vector3.zero;

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        mainCamera = GetComponent<Camera>();
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }

    }


    private void LateUpdate()
    {
        FollowBallSmoothly();
    }

    private void FollowBallSmoothly()
    {
        if (ballTransform == null) return;


        Vector3 ballViewportPos = mainCamera.WorldToViewportPoint(ballTransform.position);
        float targetPosX = transform.position.x;


        if (ballViewportPos.x > rightEdgeThreshold)
        {
            targetPosX = ballTransform.position.x + cameraOffsetX;
        }

        else if (ballViewportPos.x < leftEdgeThreshold)
        {
            targetPosX = ballTransform.position.x - cameraOffsetX;
        }


        Vector3 targetPosition = new Vector3(targetPosX, transform.position.y, transform.position.z);


        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }

    public void SetObj(Transform transformObj)
    {
        ballTransform = transformObj;
    }
}
