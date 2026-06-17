using UnityEngine;

public class PhoneInteraction : MonoBehaviour
{
    public AudioSource ringingAudio;
    public AudioClip pickupSound;

    private bool playerInside;

    void Update()
    {
        if (playerInside && Input.GetKeyDown(KeyCode.E))
        {
            AnswerPhone();
        }
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

        Debug.Log("Telefone atendido.");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
        }
    }
}