using UnityEngine;

public class PhoneInteraction : MonoBehaviour, IInteractable
{
    public AudioSource ringAudio;

    private bool isRinging;

    public void StartRinging()
    {
        if (ringAudio == null)
        {
            Debug.LogError("AudioSource não atribuído em " + gameObject.name);
            return;
        }

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

    public string GetInteractionText()
    {
        return isRinging ? "[E] Atender" : "";
    }
}