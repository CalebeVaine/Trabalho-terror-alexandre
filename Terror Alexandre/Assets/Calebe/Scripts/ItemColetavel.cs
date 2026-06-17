using UnityEngine;

public class ItemColetavel : MonoBehaviour
{
    public string nomeItem = "Moeda";

    public void Coletar()
    {
        Debug.Log("Coletou: " + nomeItem);

        Destroy(gameObject);
    }
}