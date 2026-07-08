using System.Collections;
using UnityEngine;

public class PhoneManager : MonoBehaviour
{
    public static PhoneManager Instance;
    [SerializeField] private GameObject enemy;

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

    private void Start()
    {
        StartPhone(currentPhone);
    }

    private void StartPhone(int index)
    {
        if (index >= phones.Length)
            return;

        phones[index].StartRinging();
    }

    public void PhoneAnswered(float voiceDuration)
    {
        if (SubtitleManager.Instance != null && currentPhone < phoneTexts.Length)
        {
            SubtitleManager.Instance.ShowSubtitle(phoneTexts[currentPhone], voiceDuration);
        }

        currentPhone++;

        if (currentPhone >= phones.Length)
        {
            StartCoroutine(LastEvent());
            return;
        }

        StartCoroutine(StartNextPhone(voiceDuration));
    }

    private IEnumerator StartNextPhone(float voiceDuration)
    {
        yield return new WaitForSeconds(voiceDuration + 10f);

        StartPhone(currentPhone);
    }

    private IEnumerator LastEvent()
    {
        AmbientManager.Instance.SwitchToDanger();

        if (enemy != null)
            enemy.SetActive(true);

        yield return new WaitForSeconds(8f);

        if (jumpscareAudio != null)
            jumpscareAudio.Play();
    }
}