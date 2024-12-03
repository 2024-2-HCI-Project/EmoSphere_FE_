using UnityEngine;
using System;

public class EmotionDataManager : MonoBehaviour
{
    public static EmotionDataManager Instance; // Singleton 인스턴스
    public string SelectedEmotion = "Happiness"; // 기본 감정 값
    public DateTime SelectionTime; // 감정을 선택한 시간

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this; // Singleton 초기화
            DontDestroyOnLoad(gameObject); // 씬 전환 시 삭제 방지
        }
        else
        {
            Destroy(gameObject); // 중복된 인스턴스 제거
        }
    }

    public void SetEmotion(string emotion)
    {
        if (string.IsNullOrEmpty(emotion))
        {
            Debug.LogWarning("[EmotionDataManager] 감정 값이 비어 있습니다. 설정이 취소됩니다.");
            return;
        }

        SelectedEmotion = emotion;
        SelectionTime = DateTime.Now;

        Debug.Log($"[EmotionDataManager] 감정: {SelectedEmotion}, 시간: {SelectionTime}");
    }

    public string GetEmotion()
    {
        return SelectedEmotion ?? "Happiness"; // 저장된 감정이 없으면 기본값 반환
    }
}
