using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform doorModel;
    [Header("Audio")]
[SerializeField] private AudioSource audioSource;
[SerializeField] private AudioClip openSound;
[SerializeField] private AudioClip closeSound;
[SerializeField] private AudioClip lockedSound;
[SerializeField] private AudioClip unlockSound;

    [Header("Settings")]
    [SerializeField] private bool locked = true;
    [SerializeField] private float openAngle = 90f;
    [SerializeField] private float openSpeed = 2f;
    [SerializeField] private float interactionDistance = 3f;

    private bool isOpen;

    private Quaternion closedRotation;
    private Quaternion openedRotation;

    private Camera playerCamera;
    private PlayerInventory inventory;

    private void Start()
    {
        playerCamera = Camera.main;

        inventory = FindFirstObjectByType<PlayerInventory>();

        closedRotation = doorModel.localRotation;
        openedRotation = Quaternion.Euler(
            doorModel.localEulerAngles + new Vector3(0, openAngle, 0));
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            TryInteract();
        }

        Quaternion target = isOpen ? openedRotation : closedRotation;

        doorModel.localRotation = Quaternion.Lerp(
            doorModel.localRotation,
            target,
            openSpeed * Time.deltaTime);
    }

    private void TryInteract()
{
    Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f));

    if (!Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
        return;

    if (hit.transform != doorModel)
        return;

    if (locked)
    {
        if (inventory == null || !inventory.HasKey)
        {
            Debug.Log("A porta está trancada.");

            if (audioSource && lockedSound)
                audioSource.PlayOneShot(lockedSound);

            return;
        }

        locked = false;
        Debug.Log("Você destrancou a porta.");

        if (audioSource && unlockSound)
            audioSource.PlayOneShot(unlockSound);
    }

    isOpen = !isOpen;

    if (audioSource)
    {
        if (isOpen && openSound)
            audioSource.PlayOneShot(openSound);
        else if (!isOpen && closeSound)
            audioSource.PlayOneShot(closeSound);
    }
}
}