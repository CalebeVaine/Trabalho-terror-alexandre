using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    public float interactDistance = 5f;

    private InteractableObject currentObject;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance))
        {
            InteractableObject interactable = hit.collider.GetComponent<InteractableObject>();

            if (interactable != null)
            {
                if (currentObject != interactable)
                {
                    DisableCurrentOutline();
                    currentObject = interactable;
                }

                interactable.ShowOutline();
                return;
            }
        }

        DisableCurrentOutline();
    }


    void DisableCurrentOutline()
    {
        if (currentObject != null)
        {
            currentObject.HideOutline();
            currentObject = null;
        }
    }
}