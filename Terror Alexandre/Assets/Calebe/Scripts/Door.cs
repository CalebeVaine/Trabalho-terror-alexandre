using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Door")]
    [SerializeField] private Transform doorModel;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 2f;

    private bool isPlayerNearby;
    private bool isOpen;

    private Quaternion closedRotation;
    private Quaternion openedRotation;

    private void Start()
    {
        closedRotation = doorModel.localRotation;
        openedRotation = Quaternion.Euler(
            doorModel.localEulerAngles + new Vector3(0f, openAngle, 0f));
    }

    private void Update()
    {
        if (isPlayerNearby && Input.GetMouseButtonDown(0))
        {
            isOpen = !isOpen;
        }

        Quaternion targetRotation = isOpen ? openedRotation : closedRotation;

        doorModel.localRotation = Quaternion.Lerp(
            doorModel.localRotation,
            targetRotation,
            openSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}