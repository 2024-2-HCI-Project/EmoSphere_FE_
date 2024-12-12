using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro ���ӽ����̽� �߰�

public class InputManager : MonoBehaviour
{
    // ����� �Է��� ���� UI ���
    public TMP_InputField emotionInputField;
    public Button submitButton;

    // �̺�Ʈ ��������Ʈ�� ���� �����͸� ����
    public delegate void EmotionInputHandler(string emotionText);
    public static event EmotionInputHandler OnEmotionInput;

    void Start()
    {
        // ��ư Ŭ�� �� �Է� ó�� �޼��� ȣ��
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
            Debug.Log("�Էµ� ����: " + inputText);

            // ���� �����͸� �̺�Ʈ�� ����
            OnEmotionInput?.Invoke(inputText);

            // �Է� �ʵ� �ʱ�ȭ
            emotionInputField.text = string.Empty;
        }
        else
        {
            Debug.LogWarning("���� �Է� �ʵ尡 ��� �ֽ��ϴ�.");
        }
    }
}

