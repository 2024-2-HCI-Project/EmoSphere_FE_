using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement; // 씬 전환을 위한 네임스페이스 추가

public class CreateAccountScript : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public TMP_InputField idInputField;
    public TMP_InputField passwordInputField;
    public TMP_InputField checkPasswordInputField;

    public string nextSceneName; // 인스펙터에서 설정할 씬 이름

    public struct Account
    {
        public string Name;
        public string ID;
        public string Password;

        public Account(string name, string id, string password)
        {
            Name = name;
            ID = id;
            Password = password;
        }
    }

    public void OnCreateAccountButtonClicked()
    {
        string name = nameInputField.text;
        string id = idInputField.text;
        string password = passwordInputField.text;
        string checkPassword = checkPasswordInputField.text;

        if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(id) || string.IsNullOrWhiteSpace(password))
        {
            Debug.Log("Please fill out all fields!");
            return;
        }

        if (password != checkPassword)
        {
            Debug.Log("Passwords do not match!");
            return;
        }

        Account newAccount = new Account(name, id, password);

        Debug.Log($"Account Created:\nName: {newAccount.Name}\nID: {newAccount.ID}\nPassword: {newAccount.Password}");

        // DB에 계정 저장 로직 추가 필요

        // 씬 전환
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning("Next scene name is not set in the Inspector!");
        }
    }
}
