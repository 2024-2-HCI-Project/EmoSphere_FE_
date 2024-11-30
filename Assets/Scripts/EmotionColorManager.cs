using UnityEngine;

public class EmotionColorManager : MonoBehaviour
{
    // 감정 색상 정의
    private Color happinessColor = new Color(1f, 0.647f, 0f);  // 주황색 (기쁨)
    private Color calmnessColor = new Color(1f, 1f, 0f);  // 노란색 (평온)
    private Color anxietyColor = new Color(0f, 1f, 0f);  // 초록색 (불안)
    private Color sadnessColor = new Color(0f, 0f, 1f);  // 파란색 (슬픔)
    private Color angerColor = new Color(1f, 0f, 0f);  // 빨간색 (분노)
    private Color fatigueColor = new Color(1f, 1f, 1f);  // 흰색 (무기력)

    // 3D 오브젝트의 Renderer를 저장할 변수
    private Renderer objectRenderer;

    void Start()
    {
        // 오브젝트의 Renderer 컴포넌트를 찾음
        objectRenderer = GetComponent<Renderer>();
    }

    // 감정을 선택하는 메서드
    public void OnEmotionSelected(string emotionType)
    {
        Color emotionColor = GetEmotionColor(emotionType);

        // 색상을 3D 오브젝트에 적용
        if (objectRenderer != null)
        {
            objectRenderer.material.color = emotionColor;
            Debug.Log($"{emotionType} 색상 적용됨.");
        }
    }

    // 감정에 따른 색상을 반환하는 메서드
    private Color GetEmotionColor(string emotionType)
    {
        switch (emotionType)
        {
            case "Happiness":
                return happinessColor;
            case "Calmness":
                return calmnessColor;
            case "Anxiety":
                return anxietyColor;
            case "Sadness":
                return sadnessColor;
            case "Anger":
                return angerColor;
            case "Fatigue":
                return fatigueColor;
            default:
                return Color.white;  // 기본 색상 (알 수 없는 감정)
        }
    }
}
