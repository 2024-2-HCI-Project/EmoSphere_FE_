using System;
using System.Collections.Generic;
using UnityEngine;

public class EmotionManager : MonoBehaviour
{
    // Emotion 클래스 정의
    [Serializable]
    public class Emotion
    {
        public string EmotionType; // 감정 유형 (예: 행복, 슬픔 등)
        public string Description; // 감정에 대한 설명
        public int Intensity; // 감정의 강도
        public DateTime RecordedAt; // 감정이 기록된 시간

        public Emotion(string type, string description, int intensity)
        {
            EmotionType = type;
            Description = description;
            Intensity = intensity;
            RecordedAt = DateTime.Now;
        }
    }

    // 선택 가능한 감정 목록 (고정된 값)
    private readonly List<string> allowedEmotions = new List<string> { "Joy", "Happy", "Sad", "Anger", "Anxiety", "Calm", "Lethargic" };

    // 감정 데이터를 저장할 리스트
    private List<Emotion> emotionList = new List<Emotion>();

    // 감정 데이터를 추가하는 메서드
    public void AddEmotion(string type, string description, int intensity)
    {
        if (!allowedEmotions.Contains(type))
        {
            Debug.LogWarning($"'{type}'는 유효하지 않은 감정 유형입니다.");
            return;
        }

        if (intensity < 1 || intensity > 10)
        {
            Debug.LogWarning("감정 강도는 1에서 10 사이여야 합니다.");
            return;
        }

        Emotion newEmotion = new Emotion(type, description, intensity);
        emotionList.Add(newEmotion);
        Debug.Log($"감정 추가됨: {type}, {description}, 강도: {intensity}");
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
            Debug.Log($"- {emotion.EmotionType}: {emotion.Description} (강도: {emotion.Intensity}, 기록 시간: {emotion.RecordedAt})");
        }
    }
    void Start()
    {
        // EmotionManager 스크립트를 통해 감정 데이터를 추가
        EmotionManager manager = GetComponent<EmotionManager>();

        // 감정 데이터를 추가 (예: Joy, 슬픔)
        manager.AddEmotion("Joy", "기쁨을 느낍니다.", 5);
        manager.AddEmotion("Sad", "슬픔을 느낍니다.", 8);

        // 저장된 모든 감정을 디버그 로그에 출력
        manager.PrintAllEmotions();
    }

}
