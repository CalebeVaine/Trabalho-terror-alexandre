using UnityEngine;

public class CameraParallax : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementAmount = 0.5f;
    [SerializeField] private float smoothSpeed = 3f;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        MoveCamera();
    }

    private void MoveCamera()
    {
        float mouseX = Input.mousePosition.x / Screen.width;
        float mouseY = Input.mousePosition.y / Screen.height;

        float offsetX = (mouseX - 0.5f) * movementAmount;
        float offsetY = (mouseY - 0.5f) * movementAmount;

        Vector3 targetPosition = startPosition + new Vector3(
            offsetX,
            offsetY,
            0
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPosition,
            smoothSpeed * Time.deltaTime
        );
    }
}