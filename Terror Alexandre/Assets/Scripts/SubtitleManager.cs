using System.Collections;
using TMPro;
using UnityEngine;

public class SubtitleManager : MonoBehaviour
{
    public static SubtitleManager Instance;

    [SerializeField] private TextMeshProUGUI subtitleText;

    private Coroutine currentRoutine;

    private void Awake()
    {
        Instance = this;
    }

    public void ShowSubtitle(string text, float duration = 4f)
    {
        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(ShowRoutine(text, duration));
    }

    private IEnumerator ShowRoutine(string text, float duration)
    {
        subtitleText.text = text;

        yield return new WaitForSeconds(duration);

        subtitleText.text = "";
    }
}