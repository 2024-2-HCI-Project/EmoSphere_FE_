using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    public void LoadCreateNewAccountScene()
    {
        SceneManager.LoadScene("CreateNewAccountScene");
    }
}