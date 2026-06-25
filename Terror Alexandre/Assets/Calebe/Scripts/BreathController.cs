using UnityEngine;
using System.Collections;

public class BreathEffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem breathParticle;

    [SerializeField] private float breathInterval = 10f;

    [Header("Breath Size")]
    [SerializeField] private float normalSize = 0.1f;
    [SerializeField] private float strongSize = 0.3f;
    [SerializeField] private float transitionTime = 0.5f;

    private ParticleSystem.MainModule particleMain;

    private void Awake()
    {
        particleMain = breathParticle.main;
    }

    private void Start()
    {
        StartCoroutine(BreathLoop());
    }

    private IEnumerator BreathLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(breathInterval);

            yield return StartCoroutine(ChangeSize(normalSize, strongSize));

            breathParticle.Play();

            yield return StartCoroutine(ChangeSize(strongSize, normalSize));
        }
    }

    private IEnumerator ChangeSize(float start, float target)
    {
        float time = 0f;

        while (time < transitionTime)
        {
            time += Time.deltaTime;

            float size = Mathf.Lerp(
                start,
                target,
                time / transitionTime
            );

            particleMain.startSize = size;

            yield return null;
        }

        particleMain.startSize = target;
    }
}