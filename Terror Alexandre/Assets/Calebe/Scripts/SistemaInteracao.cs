using UnityEngine;
using TMPro;

public class SistemaInteracao : MonoBehaviour
{
    public float distancia = 4f;
    public Camera cameraPlayer;

    public TMP_Text textoInteracao;

    private ObjetoInterativo objetoAtual;


    void Update()
    {
        DetectarObjeto();
    }


    void DetectarObjeto()
    {
        Ray ray = cameraPlayer.ScreenPointToRay(
            new Vector3(Screen.width / 2, Screen.height / 2)
        );

        RaycastHit hit;


        if (Physics.Raycast(ray, out hit, distancia))
        {
            ObjetoInterativo objeto = hit.collider.GetComponent<ObjetoInterativo>();

            if (objeto != null)
            {
                if (objetoAtual != objeto)
                {
                    RemoverOutline();
                    objetoAtual = objeto;
                }

                objeto.Mostrar();

                textoInteracao.text = objeto.GetTexto();
                textoInteracao.gameObject.SetActive(true);

                return;
            }
        }

        RemoverOutline();
    }


    void RemoverOutline()
    {
        if (objetoAtual != null)
        {
            objetoAtual.Esconder();
            objetoAtual = null;
        }

        textoInteracao.gameObject.SetActive(false);
    }
}