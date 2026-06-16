using UnityEngine;
using System.Collections.Generic;

public class JogadorInventario : MonoBehaviour
{
    private List<string> itens = new List<string>();

    public void ColetarItem(string item)
    {
        itens.Add(item);

        Debug.Log("Item coletado: " + item);
        Debug.Log("Total de itens: " + itens.Count);
    }
}