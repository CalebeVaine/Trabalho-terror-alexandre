using UnityEngine;

public class PhoneInteraction : MonoBehaviour, IInteractable
{
    public AudioSource ringAudio;

    private bool isRinging;

    public void StartRinging()
{
    Debug.Log(gameObject.name + " começou a tocar");

    if (ringAudio == null)
        return;

    isRinging = true;
    ringAudio.loop = true;
    ringAudio.Play();
}

public void Interact()
{
    Debug.Log(gameObject.name + " | isRinging = " + isRinging);

    if (!isRinging)
        return;

    Debug.Log("Atendeu o telefone!");

    ringAudio.Stop();
    isRinging = false;

    PhoneManager.Instance.PhoneAnswered();
}

    public string GetInteractionText()
    {
        return isRinging ? "[E] Atender" : "";
    }
}