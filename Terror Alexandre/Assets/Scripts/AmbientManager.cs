using UnityEngine;

public class AmbientManager : MonoBehaviour
{
    public static AmbientManager Instance;

    public AudioSource ambientSource;

    public AudioClip normalAmbient;
    public AudioClip dangerAmbient;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ambientSource.clip = normalAmbient;
        ambientSource.loop = true;
        ambientSource.Play();
    }

    public void SwitchToDanger()
    {
        ambientSource.clip = dangerAmbient;
        ambientSource.Play();
    }
}