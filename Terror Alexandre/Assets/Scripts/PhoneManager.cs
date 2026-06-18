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
        "Look behind you.",
        "I'm coming."
    };

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

    public void PhoneAnswered()
    {
        currentPhone++;

        if (currentPhone >= phones.Length)
        {
            Debug.Log("Todos os telefones foram atendidos.");
            return;
        }

        StartCoroutine(StartNextPhone());
    }

    private IEnumerator StartNextPhone()
    {
        yield return new WaitForSeconds(5f);

        StartPhone(currentPhone);
    }

    private void StartPhone(int index)
    {
        phones[index].StartRinging();

        if (SubtitleManager.Instance != null &&
            index < phoneTexts.Length)
        {
            SubtitleManager.Instance.ShowSubtitle(
                phoneTexts[index],
                4f
            );
        }
    }
}