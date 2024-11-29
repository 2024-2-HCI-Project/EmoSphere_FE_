using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginSceneLoader : MonoBehaviour
{
    public void LoadLoginScene()
    {
        SceneManager.LoadScene("LoginScene"); 
    }
}