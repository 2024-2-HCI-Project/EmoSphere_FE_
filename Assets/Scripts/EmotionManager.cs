using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking; // UnityWebRequest를 사용하기 위한 네임스페이스
using UnityEngine.SceneManagement;

public class EmotionManager : MonoBehaviour
{
    // Emotion 클래스 정의
    [Serializable]
    public class Emotion
    {
        public string EmotionType; // 감정 유형
        public string Description; // 감정 설명
        public string InputMode;   // 입력 방식 (예: text, voice)
        public DateTime Timestamp; // 감정 기록 시간

        public Emotion(string type, string description, string inputMode)
        {
            EmotionType = type;
            Description = description;
            InputMode = inputMode;
            Timestamp = DateTime.Now;
        }
    }

    // 선택 가능한 감정 목록
    private readonly List<string> allowedEmotions = new List<string>
    {
        "Happiness", "Calmness", "Anxiety", "Sadness", "Anger", "Fatigue"
    };

    // 감정 데이터를 저장할 리스트
    private List<Emotion> emotionList = new List<Emotion>();

    // 감정을 추가하고 API로 전송
    public void AddEmotion(string type, string description)
    {
        if (!allowedEmotions.Contains(type))
        {
            Debug.LogWarning($"'{type}'는 유효하지 않은 감정 유형입니다.");
            return;
        }

        Emotion newEmotion = new Emotion(type, description, "text");
        emotionList.Add(newEmotion);
        Debug.Log($"감정 추가됨: {type}, {description}");

        // API로 전송
        OnEmotionSubmit(type, description, "text");
    }

    // API 호출 (POST)
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

    // 감정 전송 메서드
    public void OnEmotionSubmit(string emotionType, string description, string inputMode)
    {
        string jsonData = $"{{\"emotion_type\":\"{emotionType}\",\"description\":\"{description}\",\"input_mode\":\"{inputMode}\"}}";
        string apiUrl = "http://127.0.0.1:8000/api/emotions/";

        StartCoroutine(PostRequest(apiUrl, jsonData,
            response =>
            {
                Debug.Log("Emotion successfully saved: " + response);
            },
            error =>
            {
                Debug.LogError("Error saving emotion: " + error);
            }
        ));
    }

    // Scene 전환 메서드
    private void LoadNextScene()
    {
        string nextSceneName = "KeepYourDiary";
        if (SceneManager.GetSceneByName(nextSceneName) != null)
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogWarning($"'{nextSceneName}' Scene을 찾을 수 없습니다. Build Settings에서 추가했는지 확인하세요.");
        }
    }

    void Start()
    {
        // 버튼 이벤트 설정
        Dictionary<string, string> emotionButtonMap = new Dictionary<string, string>
        {
            { "HappinessButton", "Happiness" },
            { "CalmnessButton", "Calmness" },
            { "AnxietyButton", "Anxiety" },
            { "SadnessButton", "Sadness" },
            { "AngerButton", "Anger" },
            { "FatigueButton", "Fatigue" }
        };

        foreach (var entry in emotionButtonMap)
        {
            Button button = GameObject.Find(entry.Key).GetComponent<Button>();
            string emotionType = entry.Value;

            button.onClick.AddListener(() => OnEmotionButtonClick(emotionType));
        }
    }

    public void OnEmotionButtonClick(string emotionType)
    {
        string description = $"{emotionType} 감정을 선택하셨습니다.";
        AddEmotion(emotionType, description);
        LoadNextScene();
    }
}
