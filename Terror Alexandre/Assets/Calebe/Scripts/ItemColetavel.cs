using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public AudioClip pickupSound;

    public void Coletar()
    {
        PlayerInventory inventory = FindFirstObjectByType<PlayerInventory>();

        if (inventory != null)
        {
            inventory.AddKey();

            if (pickupSound != null)
                AudioSource.PlayClipAtPoint(pickupSound, transform.position);

            Destroy(gameObject, pickupSound != null ? pickupSound.length : 0f);
        }
    }
}