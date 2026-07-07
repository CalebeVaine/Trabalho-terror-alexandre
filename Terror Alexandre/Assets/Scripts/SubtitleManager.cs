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
    Debug.Log("Legenda: " + text);

    if (currentRoutine != null)
        StopCoroutine(currentRoutine);

    currentRoutine = StartCoroutine(ShowRoutine(text, duration));
}

   private IEnumerator ShowRoutine(string text, float duration)
{
    subtitleText.gameObject.SetActive(true);

    subtitleText.text = text;

    yield return new WaitForSeconds(duration);

    subtitleText.text = "";
    subtitleText.gameObject.SetActive(false);
}
}