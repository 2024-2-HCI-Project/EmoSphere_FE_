using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 네임스페이스 추가

public class EmotionManager : MonoBehaviour
{
    // Emotion 클래스 정의
    [Serializable]
    public class Emotion
    {
        public string EmotionType; // 감정 유형 (예: 행복, 슬픔 등)
        public string Description; // 감정에 대한 설명
        public DateTime RecordedAt; // 감정이 기록된 시간

        public Emotion(string type, string description)
        {
            EmotionType = type;
            Description = description;
            RecordedAt = DateTime.Now;
        }
    }

    // 선택 가능한 감정 목록 (고정된 값)
    private readonly List<string> allowedEmotions = new List<string> { "Happiness", "Calmness", "Anxiety", "Sadness", "Anger", "Fatigue" };

    // 감정 데이터를 저장할 리스트
    private List<Emotion> emotionList = new List<Emotion>();

    // 감정 데이터를 추가하는 메서드
    public void AddEmotion(string type, string description)
    {
        if (!allowedEmotions.Contains(type))
        {
            Debug.LogWarning($"'{type}'는 유효하지 않은 감정 유형입니다.");
            return;
        }

        Emotion newEmotion = new Emotion(type, description);
        emotionList.Add(newEmotion);
        Debug.Log($"감정 추가됨: {type}, {description}");
    }

    // 저장된 감정 데이터를 반환하는 메서드
    public List<Emotion> GetEmotions()
    {
        return emotionList;
    }

    // 특정 감정을 검색하는 메서드
    public List<Emotion> SearchEmotions(string type)
    {
        if (string.IsNullOrEmpty(type) || !allowedEmotions.Contains(type))
        {
            Debug.LogWarning("검색할 감정 유형이 유효하지 않습니다.");
            return new List<Emotion>();
        }

        List<Emotion> result = emotionList.FindAll(e => e.EmotionType.Equals(type, StringComparison.OrdinalIgnoreCase));
        Debug.Log($"{result.Count}개의 감정이 '{type}' 유형으로 검색됨.");
        return result;
    }

    // 디버깅용: 모든 감정 데이터를 출력하는 메서드
    public void PrintAllEmotions()
    {
        Debug.Log("현재 저장된 감정 목록:");
        foreach (var emotion in emotionList)
        {
            Debug.Log($"- {emotion.EmotionType}: {emotion.Description} (기록 시간: {emotion.RecordedAt})");
        }
    }

    // UI 버튼을 위한 메서드
    public void OnEmotionButtonClick(string emotionType)
    {
        // 감정에 맞는 설명을 추가
        string description = emotionType switch
        {
            "Happiness" => "감정 선택 : 행복",
            "Calmness" => "감정 선택 : 평온",
            "Anxiety" => "감정 선택 : 불안",
            "Sadness" => "감정 선택 : 슬픔",
            "Anger" => "감정 선택 : 분노",
            "Fatigue" => "감정 선택 : 무기력",
            _ => "알 수 없는 감정"
        };

        // 감정 추가
        AddEmotion(emotionType, description);
        
        // 저장된 모든 감정을 출력
        PrintAllEmotions();
    }

    void Start()
    {
        // 감정 버튼과 감정 유형을 매핑한 딕셔너리
        Dictionary<string, string> emotionButtonMap = new Dictionary<string, string>
        {
            { "HappinessButton", "Happiness" },
            { "CalmnessButton", "Calmness" },
            { "AnxietyButton", "Anxiety" },
            { "SadnessButton", "Sadness" },
            { "AngerButton", "Anger" },
            { "FatigueButton", "Fatigue" }
        };

        // 각 버튼에 해당하는 감정 클릭 이벤트 연결
        foreach (var entry in emotionButtonMap)
        {
            Button button = GameObject.Find(entry.Key).GetComponent<Button>();
            string emotionType = entry.Value;

            button.onClick.AddListener(() => OnEmotionButtonClick(emotionType));
        }
    }
}
