using System.Collections.Generic;
using System;
using UnityEngine;

public class EmotionVisualizer : MonoBehaviour
{
    [Serializable]
    public class EmotionObject
    {
        public GameObject ObjectPrefab; // 감정을 시각화할 3D 오브젝트 프리팹
        public string EmotionType;      // 감정 유형
    }

    public List<EmotionObject> emotionPrefabs = new List<EmotionObject>(); // 감정 유형별 오브젝트 프리팹 리스트

    private Dictionary<string, GameObject> emotionPrefabDict = new Dictionary<string, GameObject>(); // 빠른 참조를 위한 딕셔너리

    void Start()
    {
        // 딕셔너리에 감정 유형과 프리팹 매핑
        foreach (var emotion in emotionPrefabs)
        {
            if (!emotionPrefabDict.ContainsKey(emotion.EmotionType))
            {
                emotionPrefabDict[emotion.EmotionType] = emotion.ObjectPrefab;
            }
        }
    }

    // 감정 유형에 따른 3D 오브젝트를 생성하는 메서드
    public GameObject VisualizeEmotion(string emotionType, Vector3 position, float size, Color color)
    {
        if (!emotionPrefabDict.ContainsKey(emotionType))
        {
            Debug.LogWarning($"'{emotionType}'는 지원하지 않는 감정 유형입니다.");
            return null;
        }

        // 오브젝트 생성
        GameObject prefab = emotionPrefabDict[emotionType];
        GameObject instance = Instantiate(prefab, position, Quaternion.identity);

        // 오브젝트 크기와 색상 설정
        instance.transform.localScale = Vector3.one * size;
        var renderer = instance.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }

        Debug.Log($"{emotionType} 감정 오브젝트 생성 완료: 위치 {position}, 크기 {size}, 색상 {color}");
        return instance;
    }

    // 디버깅용: 임의의 감정 오브젝트를 생성하는 테스트 메서드
    public void TestVisualizeEmotion()
    {
        VisualizeEmotion("Joy", new Vector3(0, 1, 0), 1.5f, Color.yellow);
    }
}
