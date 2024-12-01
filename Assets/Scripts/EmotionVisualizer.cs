using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class EmotionVisualizer : MonoBehaviour
{
    [System.Serializable]
    public class EmotionObject
    {
        public GameObject ObjectPrefab; // 감정을 나타낼 3D 오브젝트 프리팹
        public string EmotionType;      // 감정 유형
    }

    public List<EmotionObject> emotionPrefabs = new List<EmotionObject>(); // 감정 프리팹 리스트
    private Dictionary<string, GameObject> emotionPrefabDict = new Dictionary<string, GameObject>(); // 감정 유형 -> 프리팹 매핑

    void Start()
    {
        // 감정 프리팹 매핑 생성
        foreach (var emotion in emotionPrefabs)
        {
            if (!emotionPrefabDict.ContainsKey(emotion.EmotionType))
            {
                emotionPrefabDict[emotion.EmotionType] = emotion.ObjectPrefab;
            }
        }

        // 테스트 데이터 가져오기 및 시각화
        FetchAndVisualizeEmotions();
    }

    // 감정 데이터를 기반으로 3D 오브젝트를 생성하는 메서드
    public GameObject VisualizeEmotion(string emotionType, Vector3 position, float size, Color color)
    {
        if (!emotionPrefabDict.ContainsKey(emotionType))
        {
            Debug.LogWarning($"'{emotionType}'는 정의되지 않은 감정 유형입니다.");
            return null;
        }

        // 해당 감정의 프리팹 가져오기
        GameObject prefab = emotionPrefabDict[emotionType];
        GameObject instance = Instantiate(prefab, position, Quaternion.identity);

        // 크기와 색상 설정
        instance.transform.localScale = Vector3.one * size;
        Renderer renderer = instance.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }

        Debug.Log($"{emotionType} 감정을 시각화: 위치 {position}, 크기 {size}, 색상 {color}");
        return instance;
    }

    // API에서 감정 데이터를 가져와 시각화하는 메서드
    public void FetchAndVisualizeEmotions()
    {
        StartCoroutine(GetRequest("http://127.0.0.1:8000/api/emotions/",
            response =>
            {
                // JSON 데이터를 Emotion 클래스 리스트로 변환
                EmotionData[] emotions = JsonUtility.FromJson<EmotionDataWrapper>(response).data;

                foreach (var emotion in emotions)
                {
                    // 감정 시각화
                    VisualizeEmotion(emotion.emotion_type, new Vector3(0, 1, 0), 1.0f, Color.green);
                }
            },
            error => Debug.LogError("감정 데이터를 가져오는 데 실패했습니다: " + error)));
    }

    // API GET 요청을 보내는 코루틴 메서드
    private IEnumerator GetRequest(string url, System.Action<string> onSuccess, System.Action<string> onError)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
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

    // 테스트용 감정 시각화
    public void TestVisualizeEmotion()
    {
        VisualizeEmotion("Happiness", new Vector3(0, 1, 0), 1.5f, Color.yellow);
    }

    // 감정 데이터를 나타내는 클래스 (API 응답용)
    [System.Serializable]
    public class EmotionData
    {
        public string emotion_type;
        public string description;
        public string input_mode;
        public string timestamp;
    }

    [System.Serializable]
    public class EmotionDataWrapper
    {
        public EmotionData[] data;
    }
}
