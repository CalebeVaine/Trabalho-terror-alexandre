using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    public GameObject victoryPanel;
    public MonoBehaviour[] playerScripts;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
            return;

        if (victoryPanel != null)
            victoryPanel.SetActive(true);

        foreach (MonoBehaviour script in playerScripts)
        {
            if (script != null)
                script.enabled = false;
        }

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}