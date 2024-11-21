using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    // 사용자 입력을 받을 UI 요소
    public TMP_InputField emotionInputField; // 감정 입력 필드 (TextMeshPro)
    public Button submitButton; // 감정 추가 버튼
    public TMP_Dropdown emotionTypeDropdown; // 감정 유형 선택 드롭다운 (TextMeshPro)
    public Slider intensitySlider; // 감정 강도 선택 슬라이더

    // EmotionManager와 연동
    public EmotionManager emotionManager;

    void Start()
    {
        // 버튼 클릭 시 HandleInputSubmit 메서드 호출
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(HandleInputSubmit);
        }

        // 드롭다운 초기화
        InitializeEmotionTypeDropdown();

        // 슬라이더 기본값 설정
        if (intensitySlider != null)
        {
            intensitySlider.minValue = 1;
            intensitySlider.maxValue = 10;
            intensitySlider.value = 5; // 기본값
        }
    }

    void HandleInputSubmit()
    {
        if (emotionInputField != null && emotionManager != null)
        {
            // 입력 필드와 드롭다운 값 가져오기
            string description = emotionInputField.text;
            string emotionType = emotionTypeDropdown.options[emotionTypeDropdown.value].text;
            int intensity = (int)intensitySlider.value;

            // 유효성 검사
            if (string.IsNullOrEmpty(description))
            {
                Debug.LogWarning("감정 설명을 입력하세요.");
                return;
            }

            // EmotionManager에 데이터 추가
            emotionManager.AddEmotion(emotionType, description, intensity);

            // 입력 필드 초기화
            emotionInputField.text = string.Empty;
            Debug.Log($"감정 추가 완료: {emotionType}, {description}, 강도: {intensity}");
        }
        else
        {
            Debug.LogError("EmotionInputField 또는 EmotionManager가 설정되지 않았습니다.");
        }
    }

    void InitializeEmotionTypeDropdown()
    {
        if (emotionTypeDropdown != null)
        {
            // EmotionManager에서 허용된 감정 목록 가져오기
            var allowedEmotions = new string[] { "Joy", "Happy", "Sad", "Anger", "Anxiety", "Calm", "Lethargic" };

            // 드롭다운 옵션 설정
            emotionTypeDropdown.ClearOptions();
            emotionTypeDropdown.AddOptions(new System.Collections.Generic.List<string>(allowedEmotions));
        }
    }
}
