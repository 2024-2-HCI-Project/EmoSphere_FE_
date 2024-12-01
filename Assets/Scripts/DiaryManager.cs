using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.Networking;

public class DiaryManager : MonoBehaviour
{
    public TMP_InputField inputField; // 다이어리 입력 필드
    public GameObject saveButton;    // 저장 버튼

    private void Start()
    {
        saveButton.GetComponent<UnityEngine.UI.Button>().onClick.AddListener(SaveEntry);
    }

    private void SaveEntry()
    {
        string userInput = inputField.text;

        string currentTime = DateTime.Now.ToString("yyyy-MM-dd tt h:mm:ss");

        Debug.Log($"기록된 내용: {userInput} (기록 시간: {currentTime})");

        // JSON 데이터 생성
        string jsonData = $"{{\"description\":\"{userInput}\",\"input_mode\":\"text\"}}";

        // API 호출
        StartCoroutine(PostRequest("http://127.0.0.1:8000/api/emotions/", jsonData,
            response => Debug.Log("Diary entry saved: " + response),
            error => Debug.LogError("Failed to save diary entry: " + error)
        ));

        inputField.text = ""; // 입력 필드 초기화
    }

    // HTTP POST 요청 메서드
    private IEnumerator PostRequest(string url, string jsonData, Action<string> onSuccess, Action<string> onError)
    {
        using (UnityWebRequest request = new UnityWebRequest(url, "POST"))
        {
            byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onSuccess?.Invoke(request.downloadHandler.text);
            }
            else
            {
                onError?.Invoke(request.error);
            }
        }
    }
}
