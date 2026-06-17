using UnityEngine;

public class RotacaoObjeto : MonoBehaviour
{
    public float velocidade = 100f;

    void Update()
    {
        transform.rotation = Quaternion.Euler(
            transform.eulerAngles.x,
            transform.eulerAngles.y + velocidade * Time.deltaTime,
            transform.eulerAngles.z
        );
    }
}