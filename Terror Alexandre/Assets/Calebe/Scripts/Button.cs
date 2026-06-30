using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [SerializeField] private string Scene;

    public void ChangeScene()
    {
        SceneManager.LoadScene(Scene);
    }
}