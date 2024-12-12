using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    // ����� �Է��� ���� UI ���
    public TMP_InputField emotionInputField; // ���� �Է� �ʵ� (TextMeshPro)
    public Button submitButton; // ���� �߰� ��ư
    public TMP_Dropdown emotionTypeDropdown; // ���� ���� ���� ��Ӵٿ� (TextMeshPro)
    public Slider intensitySlider; // ���� ���� ���� �����̴�

    // EmotionManager�� ����
    public EmotionManager emotionManager;

    void Start()
    {
        // ��ư Ŭ�� �� HandleInputSubmit �޼��� ȣ��
        if (submitButton != null)
        {
            submitButton.onClick.AddListener(HandleInputSubmit);
        }

        // ��Ӵٿ� �ʱ�ȭ
        InitializeEmotionTypeDropdown();

        // �����̴� �⺻�� ����
        if (intensitySlider != null)
        {
            intensitySlider.minValue = 1;
            intensitySlider.maxValue = 10;
            intensitySlider.value = 5; // �⺻��
        }
    }

    void HandleInputSubmit()
    {
        if (emotionInputField != null && emotionManager != null)
        {
            // �Է� �ʵ�� ��Ӵٿ� �� ��������
            string description = emotionInputField.text;
            string emotionType = emotionTypeDropdown.options[emotionTypeDropdown.value].text;
            int intensity = (int)intensitySlider.value;

            // ��ȿ�� �˻�
            if (string.IsNullOrEmpty(description))
            {
                Debug.LogWarning("���� ������ �Է��ϼ���.");
                return;
            }

            // EmotionManager�� ������ �߰�
            emotionManager.AddEmotion(emotionType, description, intensity);

            // �Է� �ʵ� �ʱ�ȭ
            emotionInputField.text = string.Empty;
            Debug.Log($"���� �߰� �Ϸ�: {emotionType}, {description}, ����: {intensity}");
        }
        else
        {
            Debug.LogError("EmotionInputField �Ǵ� EmotionManager�� �������� �ʾҽ��ϴ�.");
        }
    }

    void InitializeEmotionTypeDropdown()
    {
        if (emotionTypeDropdown != null)
        {
            // EmotionManager���� ���� ���� ��� ��������
            var allowedEmotions = new string[] { "Joy", "Happy", "Sad", "Anger", "Anxiety", "Calm", "Lethargic" };

            // ��Ӵٿ� �ɼ� ����
            emotionTypeDropdown.ClearOptions();
            emotionTypeDropdown.AddOptions(new System.Collections.Generic.List<string>(allowedEmotions));
        }
    }
}
