using UnityEngine;

public class PhoneInteraction : MonoBehaviour
{
    public AudioSource ringAudio;

    private bool isRinging;
    private bool playerNearby;

    public void StartRinging()
    {
        isRinging = true;

        ringAudio.loop = true;
        ringAudio.Play();
    }

    public void Interact()
    {
        if (!isRinging)
            return;

        ringAudio.Stop();

        isRinging = false;

        PhoneManager.Instance.PhoneAnswered();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerNearby = false;
    }
}