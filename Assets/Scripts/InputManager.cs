using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro 네임스페이스 추가

public class InputManager : MonoBehaviour
{
    // 사용자 입력을 받을 UI 요소
    public TMP_InputField emotionInputField;
    public Button submitButton;

    // 이벤트 델리게이트로 감정 데이터를 전달
    public delegate void EmotionInputHandler(string emotionText);
    public static event EmotionInputHandler OnEmotionInput;

    void Start()
    {
        // 버튼 클릭 시 입력 처리 메서드 호출
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(HandleInputSubmit);
        }
    }

    void HandleInputSubmit()
    {
        if (emotionInputField != null && !string.IsNullOrEmpty(emotionInputField.text))
        {
            string inputText = emotionInputField.text;
            Debug.Log("입력된 감정: " + inputText);

            // 감정 데이터를 이벤트로 전달
            OnEmotionInput?.Invoke(inputText);

            // 입력 필드 초기화
            emotionInputField.text = string.Empty;
        }
        else
        {
            Debug.LogWarning("감정 입력 필드가 비어 있습니다.");
        }
    }
}

