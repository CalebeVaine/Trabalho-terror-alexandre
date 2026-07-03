using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("References")]
    public Camera playerCamera;
    public TextMeshProUGUI interactText;

    [Header("Settings")]
    public float interactDistance = 3f;
    public LayerMask interactLayers;

    private IInteractable currentInteractable;

    private void Awake()
    {
        if (playerCamera == null)
            playerCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, interactLayers))
        {
             Debug.Log("Acertou: " + hit.collider.name);

    currentInteractable = hit.collider.GetComponent<IInteractable>();

    Debug.Log("Interactable: " + currentInteractable);

            if (currentInteractable != null)
            {
                interactText.text = currentInteractable.GetInteractionText();

                if (Keyboard.current.eKey.wasPressedThisFrame)
                {
                    currentInteractable.Interact();
                }

                return;
            }
        }

        currentInteractable = null;
        interactText.text = "";
    }
}