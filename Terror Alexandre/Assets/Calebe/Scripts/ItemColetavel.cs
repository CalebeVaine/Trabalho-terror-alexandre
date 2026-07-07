using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public void Coletar()
    {
        PlayerInventory inventory = FindFirstObjectByType<PlayerInventory>();

        if (inventory != null)
        {
            inventory.AddKey();
            Destroy(gameObject);
        }
    }
}