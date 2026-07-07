using System.Collections;
using UnityEngine;

public class JumpscareManagerAudio : MonoBehaviour
{
    public static JumpscareManagerAudio Instance;

    public GameObject jumpscareImage;
    public AudioSource jumpscareAudio;
    public GameObject enemy;

    private void Awake()
    {
        Instance = this;

        if (jumpscareImage != null)
            jumpscareImage.SetActive(false);

        if (enemy != null)
            enemy.SetActive(false);
    }

    public void PlayJumpscare()
    {
        StartCoroutine(JumpscareRoutine());
    }

    IEnumerator JumpscareRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        if (jumpscareImage != null)
            jumpscareImage.SetActive(true);

        if (jumpscareAudio != null)
            jumpscareAudio.Play();

        yield return new WaitForSeconds(0.25f);

        if (jumpscareImage != null)
            jumpscareImage.SetActive(false);

        if (enemy != null)
            enemy.SetActive(true);
    }
}