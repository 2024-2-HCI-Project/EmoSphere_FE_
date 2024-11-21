using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseConnector : MonoBehaviour
{
    // 서버 URL 설정
    public string serverUrl = "https://yourserver.com/api/emotions";

    // 감정 데이터를 서버에 저장하는 메서드
    public void SaveEmotionToDatabase(string emotionType, string description, int intensity)
    {
        StartCoroutine(SendEmotionData(emotionType, description, intensity));
    }

    // 서버에서 감정 데이터를 가져오는 메서드
    public void FetchEmotionData(System.Action<List<EmotionData>> callback)
    {
        StartCoroutine(GetEmotionData(callback));
    }

    // 서버로 감정 데이터를 전송하는 코루틴
    private IEnumerator SendEmotionData(string emotionType, string description, int intensity)
    {
        // JSON 데이터 생성
        EmotionData emotion = new EmotionData
        {
            EmotionType = emotionType,
            Description = description,
            Intensity = intensity
        };
        string jsonData = JsonUtility.ToJson(emotion);

        // HTTP POST 요청 설정
        using (UnityWebRequest request = new UnityWebRequest(serverUrl, "POST"))
        {
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            // 요청 전송 및 응답 대기
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("감정 데이터 전송 성공: " + request.downloadHandler.text);
            }
            else
            {
                Debug.LogError("감정 데이터 전송 실패: " + request.error);
            }
        }
    }

    // 서버에서 감정 데이터를 가져오는 코루틴
    private IEnumerator GetEmotionData(System.Action<List<EmotionData>> callback)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(serverUrl))
        {
            // 요청 전송 및 응답 대기
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("감정 데이터 가져오기 성공: " + request.downloadHandler.text);

                // JSON 응답 데이터를 리스트로 변환
                string jsonResponse = request.downloadHandler.text;
                EmotionDataList emotionDataList = JsonUtility.FromJson<EmotionDataList>(jsonResponse);

                callback?.Invoke(emotionDataList.emotions);
            }
            else
            {
                Debug.LogError("감정 데이터 가져오기 실패: " + request.error);
                callback?.Invoke(new List<EmotionData>());
            }
        }
    }

    // 감정 데이터 클래스
    [System.Serializable]
    public class EmotionData
    {
        public string EmotionType;
        public string Description;
        public int Intensity;
    }

    // 감정 데이터 리스트를 위한 래퍼 클래스
    [System.Serializable]
    public class EmotionDataList
    {
        public List<EmotionData> emotions;
    }
}

