using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool HasKey { get; private set; }

    public void AddKey()
    {
        HasKey = true;
        Debug.Log("Chave coletada!");
    }
}