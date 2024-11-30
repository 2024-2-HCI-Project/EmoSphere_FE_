using UnityEngine;
using TMPro; 
using UnityEngine.SceneManagement; 

public class LoginScript : MonoBehaviour
{
    public TMP_InputField idInputField;
    public TMP_InputField passwordInputField;

    private string storedID = "testUser";
    private string storedPassword = "1234";

    public void OnLoginButtonClicked()
    {
        string inputID = idInputField.text;
        string inputPassword = passwordInputField.text;

        if (string.IsNullOrWhiteSpace(inputID) || string.IsNullOrWhiteSpace(inputPassword))
        {
            Debug.Log("Please fill out all fields!");
            return;
        }

        if (inputID == storedID && inputPassword == storedPassword)
        {
            Debug.Log($"Login Successful: ID = {inputID}, Password = {inputPassword}");

            SceneManager.LoadScene("ChooseYourEmotion");
        }
        else
        {
            Debug.Log("Login Failed: Invalid ID or Password");
        }
    }
}

