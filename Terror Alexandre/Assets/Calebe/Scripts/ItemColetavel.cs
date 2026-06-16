using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public string nomeItem = "Moeda";

    private void OnTriggerEnter(Collider other)
    {
        JogadorInventario inventario = other.GetComponent<JogadorInventario>();

        if (inventario != null)
        {
            inventario.ColetarItem(nomeItem);
            Destroy(gameObject);
        }
    }
}