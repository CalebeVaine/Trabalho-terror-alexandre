using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneInteraction : MonoBehaviour
{
    public AudioSource ringingAudio;
    public AudioClip pickupSound;
    public GameObject interactText;

    private bool playerInside;

    public void Interact(InputAction.CallbackContext context)
    {
        if (!context.performed)
            return;

        if (!playerInside)
            return;

        AnswerPhone();
    }

    private void AnswerPhone()
    {
        ringingAudio.Stop();

        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(
                pickupSound,
                Camera.main.transform.position
            );
        }

        interactText.SetActive(false);

        Debug.Log("Telefone atendido!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
            interactText.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            interactText.SetActive(false);
        }
    }
}