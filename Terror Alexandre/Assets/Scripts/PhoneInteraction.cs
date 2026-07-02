using UnityEngine;

public class PhoneInteraction : MonoBehaviour
{
    public AudioSource ringAudio;

    private bool isRinging;

    public void StartRinging()
    {
        if (ringAudio == null)
        {
            Debug.LogError("Ring Audio não foi atribuído em " + gameObject.name);
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

    public bool IsRinging()
    {
        return isRinging;
    }
}