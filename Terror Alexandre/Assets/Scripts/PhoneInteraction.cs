using System.Collections;
using UnityEngine;

public class PhoneInteraction : MonoBehaviour, IInteractable
{
    public AudioSource ringAudio;
    public AudioSource answerAudio;
    public AudioSource voiceAudio;
    public AudioSource errorAudio;

    [Header("Último telefone")]
    public bool activateEnemy;
    public GameObject enemy;

    private bool isRinging;

    public void StartRinging()
    {
        if (ringAudio == null)
            return;

        isRinging = true;
        ringAudio.loop = true;
        ringAudio.Play();
    }

    public void Interact()
    {
        if (!isRinging)
        {
            if (errorAudio != null)
                errorAudio.Play();

            return;
        }

        ringAudio.Stop();
        isRinging = false;

        if (activateEnemy)
            JumpscareManagerAudio.Instance.PlayJumpscare();

        StartCoroutine(AnswerRoutine());
    }

    private IEnumerator AnswerRoutine()
    {
        if (answerAudio != null)
        {
            answerAudio.Play();
            yield return new WaitForSeconds(answerAudio.clip.length);
        }

        if (voiceAudio != null)
        {
            voiceAudio.Play();
            PhoneManager.Instance.PhoneAnswered(voiceAudio.clip.length);
        }
        else
        {
            PhoneManager.Instance.PhoneAnswered(4f);
        }
    }

    public string GetInteractionText()
    {
        return isRinging ? "[E] Atender" : "";
    }
}