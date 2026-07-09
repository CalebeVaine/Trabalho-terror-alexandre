using UnityEngine;

public class EnemyKeeper : MonoBehaviour
{
    [SerializeField] private GameObject enemy;

    void Update()
    {
        if (enemy != null && !enemy.activeSelf)
        {
            enemy.SetActive(true);
        }
    }
}