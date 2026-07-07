using System.Collections;
using UnityEngine;

public class JumpscareManagerAudio : MonoBehaviour
{
    public static JumpscareManagerAudio Instance;

    public AudioSource jumpscareAudio;
    public GameObject enemy;

    private void Awake()
    {
        Instance = this;

        if (enemy != null)
            enemy.SetActive(false);
    }

    public void PlayJumpscare()
    {
        StartCoroutine(JumpscareRoutine());
    }

    private IEnumerator JumpscareRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        if (jumpscareAudio != null)
            jumpscareAudio.Play();

        yield return new WaitForSeconds(0.2f);

        if (enemy != null)
            enemy.SetActive(true);
    }
}