using UnityEngine;

public class EmotionColorManager : MonoBehaviour
{
    private Light objectLight;

    private Color happinessColor = new Color(1f, 0.647f, 0f); // 주황색 (기쁨)
    private Color calmnessColor = new Color(1f, 1f, 0f);      // 노란색 (평온)
    private Color anxietyColor = new Color(0f, 1f, 0f);       // 초록색 (불안)
    private Color sadnessColor = new Color(0f, 0f, 1f);       // 파란색 (슬픔)
    private Color angerColor = new Color(1f, 0f, 0f);         // 빨간색 (분노)
    private Color fatigueColor = new Color(1f, 1f, 1f);       // 흰색 (무기력)

    void Start()
    {
        objectLight = GetComponent<Light>();

        if (objectLight == null)
        {
            Debug.LogError("Light 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        if (EmotionDataManager.Instance == null)
        {
            Debug.LogError("EmotionDataManager.Instance가 null입니다. 기본값(Default)으로 처리합니다.");
            ApplyEmotionLightColor("Default");
            return;
        }

        string selectedEmotion = EmotionDataManager.Instance.GetEmotion();
        ApplyEmotionLightColor(selectedEmotion);
    }

    public void ApplyEmotionLightColor(string emotionType)
    {
        Color emotionColor = GetEmotionColor(emotionType);

        if (objectLight != null)
        {
            objectLight.color = emotionColor; // Light 색상 변경
            Debug.Log($"{emotionType} 빛 색상이 적용되었습니다.");
        }
    }

    private Color GetEmotionColor(string emotionType)
    {
        switch (emotionType)
        {
            case "Happiness": return happinessColor;
            case "Calmness": return calmnessColor;
            case "Anxiety": return anxietyColor;
            case "Sadness": return sadnessColor;
            case "Anger": return angerColor;
            case "Fatigue": return fatigueColor;
            default:
                Debug.LogWarning($"알 수 없는 감정 타입: {emotionType}. 기본 색상(흰색)을 반환합니다.");
                return Color.white;
        }
    }
}
