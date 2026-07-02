using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    public Camera playerCamera;
    public float interactDistance = 3f;
    public TextMeshProUGUI interactText;

    private IInteractable currentInteractable;

    void Update()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);

        Debug.DrawRay(ray.origin, ray.direction * interactDistance, Color.red);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            currentInteractable = hit.collider.GetComponent<IInteractable>();

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