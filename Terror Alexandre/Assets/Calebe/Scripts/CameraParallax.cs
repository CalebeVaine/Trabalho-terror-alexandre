using UnityEngine;

public class CameraParallax : MonoBehaviour
{
    [Header("Rotation Settings")]
    [SerializeField] private float rotationAmount = 5f;
    [SerializeField] private float smoothSpeed = 3f;

    private Quaternion startRotation;

    private void Start()
    {
        startRotation = transform.rotation;
    }

    private void Update()
    {
        RotateCamera();
    }

    private void RotateCamera()
    {
        float mouseX = Input.mousePosition.x / Screen.width;

        float rotationY = (mouseX - 0.5f) * rotationAmount;

        Quaternion targetRotation = startRotation * Quaternion.Euler(
            0,
            rotationY,
            0
        );

        transform.rotation = Quaternion.Lerp(
            transform.rotation,
            targetRotation,
            smoothSpeed * Time.deltaTime
        );
    }
}