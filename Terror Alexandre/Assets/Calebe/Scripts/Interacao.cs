using UnityEngine;

public class Interacao : MonoBehaviour
{
    public float distancia = 3f;
    public Camera cameraPlayer;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cameraPlayer.ScreenPointToRay(
                new Vector3(Screen.width / 2, Screen.height / 2)
            );

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, distancia))
            {
                ItemColetavel item = hit.collider.GetComponent<ItemColetavel>();

                if (item != null)
                {
                    item.Coletar();
                }
            }
        }
    }
}