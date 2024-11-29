using UnityEngine;
using TMPro; 
using System; 

public class DiaryManager : MonoBehaviour
{
    public TMP_InputField inputField;
    public GameObject saveButton; 

    private void Start()
    {
        saveButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SaveEntry);
    }

    private void SaveEntry()
    {
        string userInput = inputField.text;

        string currentTime = DateTime.Now.ToString("yyyy-MM-dd tt h:mm:ss");

        Debug.Log($"기록된 내용: {userInput} (기록 시간: {currentTime})");

        inputField.text = "";
    }
}
