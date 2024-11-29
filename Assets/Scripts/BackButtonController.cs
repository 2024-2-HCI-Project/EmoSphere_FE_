using UnityEngine;
using UnityEngine.SceneManagement; 

public class BackButtonController : MonoBehaviour
{

    public void OnBackButtonClick()
    {

        SceneManager.LoadScene("KeepYourDiary");
    }
}
