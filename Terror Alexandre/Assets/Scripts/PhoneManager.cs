using System.Collections;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public static PhoneManager Instance;

    [Header("Phones")]
    public PhoneInteraction[] phones;

    [Header("Subtitles")]
    public string[] phoneTexts =
    {
        "...",
        "Can you hear me?",
        "Why aren't you talking?",
        "I know where you are.",
        "I'm coming."
    };

    [Header("Jumpscare")]
    public AudioSource jumpscareAudio;

    private int currentPhone = 0;

    private void Awake()
    {
        Instance = this;
    }

    private IEnumerator Start()
    {
        yield return new WaitForSeconds(15f);

        StartPhone(currentPhone);
    }

    private void StartPhone(int index)
    {
        if (index >= phones.Length)
            return;

        phones[index].StartRinging();
    }

    public void PhoneAnswered()
    {
        if (currentPhone < phoneTexts.Length)
        {
            SubtitleManager.Instance.ShowSubtitle(phoneTexts[currentPhone], 4f);
        }

        currentPhone++;

        if (currentPhone >= phones.Length)
        {
            StartCoroutine(LastEvent());
            return;
        }

        StartCoroutine(StartNextPhone());
    }

    private IEnumerator StartNextPhone()
    {
        yield return new WaitForSeconds(10f);

        StartPhone(currentPhone);
    }

    private IEnumerator LastEvent()
    {
        AmbientManager.Instance.SwitchToDanger();

        yield return new WaitForSeconds(8f);

        jumpscareAudio.Play();
    }
}