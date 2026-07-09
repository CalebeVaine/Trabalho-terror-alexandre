using UnityEngine;

public class JumpscareController : MonoBehaviour
{
    [Header("Player")]
    public Transform playerCamera;
    public MonoBehaviour[] playerScripts;

    [Header("Enemy")]
    public GameObject enemy;
    public MonoBehaviour enemyAI;
    public Transform headTarget;
    public float distanceFromCamera = 1.5f;

    [Header("UI & Audio")]
    public GameObject deathScreen;
    public AudioSource sound;
    public float deathScreenDelay = 0.8f;

    bool triggered;
    bool lockCamera;

    void Start()
    {
        if (deathScreen) deathScreen.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (!triggered && other.CompareTag("Player"))
            TriggerJumpscare();
    }

    void LateUpdate()
    {
        if (lockCamera && headTarget)
        {
            Vector3 dir = (headTarget.position - playerCamera.position).normalized;
            playerCamera.rotation = Quaternion.LookRotation(dir);
        }
    }

    public void TriggerJumpscare()
    {
        Debug.Log("Jumpscare iniciado!");
        triggered = true;
        lockCamera = true;
        Debug.Log("Scripts encontrados: " + playerScripts.Length);

        foreach (var script in playerScripts)
{
    Debug.Log(script);

    if (script)
        script.enabled = false;
}
        if (enemyAI) enemyAI.enabled = false;

        Vector3 pos = playerCamera.position + playerCamera.forward * distanceFromCamera;
        pos.y = enemy.transform.position.y;

        enemy.transform.position = pos;
        enemy.transform.LookAt(playerCamera);
        Debug.Log("Inimigo reposicionado.");

        if (sound) sound.Play();

        Invoke(nameof(ShowDeathScreen), deathScreenDelay);
    }

    void ShowDeathScreen()
    {
        if (deathScreen) deathScreen.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}