using UnityEngine;
using UnityEngine.UI;

public class ObjetoInterativo : MonoBehaviour
{
    public string texto = "Pegar";

    private Outline outline;

    void Awake()
    {
        outline = GetComponent<Outline>();

        if (outline != null)
            outline.enabled = false;
    }

    public void Mostrar()
    {
        if (outline != null)
            outline.enabled = true;
    }

    public void Esconder()
    {
        if (outline != null)
            outline.enabled = false;
    }

    public string GetTexto()
    {
        return texto;
    }
}