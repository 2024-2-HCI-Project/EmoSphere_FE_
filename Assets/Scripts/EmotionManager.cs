using System;
using System.Collections.Generic;
using UnityEngine;

public class EmotionManager : MonoBehaviour
{
    // Emotion Ŭ���� ����
    [Serializable]
    public class Emotion
    {
        public string EmotionType; // ���� ���� (��: �ູ, ���� ��)
        public string Description; // ������ ���� ����
        public int Intensity; // ������ ����
        public DateTime RecordedAt; // ������ ��ϵ� �ð�

        public Emotion(string type, string description, int intensity)
        {
            EmotionType = type;
            Description = description;
            Intensity = intensity;
            RecordedAt = DateTime.Now;
        }
    }

    // ���� ������ ���� ��� (������ ��)
    private readonly List<string> allowedEmotions = new List<string> { "Joy", "Happy", "Sad", "Anger", "Anxiety", "Calm", "Lethargic" };

    // ���� �����͸� ������ ����Ʈ
    private List<Emotion> emotionList = new List<Emotion>();

    // ���� �����͸� �߰��ϴ� �޼���
    public void AddEmotion(string type, string description, int intensity)
    {
        if (!allowedEmotions.Contains(type))
        {
            Debug.LogWarning($"'{type}'�� ��ȿ���� ���� ���� �����Դϴ�.");
            return;
        }

        if (intensity < 1 || intensity > 10)
        {
            Debug.LogWarning("���� ������ 1���� 10 ���̿��� �մϴ�.");
            return;
        }

        Emotion newEmotion = new Emotion(type, description, intensity);
        emotionList.Add(newEmotion);
        Debug.Log($"���� �߰���: {type}, {description}, ����: {intensity}");
    }

    // ����� ���� �����͸� ��ȯ�ϴ� �޼���
    public List<Emotion> GetEmotions()
    {
        return emotionList;
    }

    // Ư�� ������ �˻��ϴ� �޼���
    public List<Emotion> SearchEmotions(string type)
    {
        if (string.IsNullOrEmpty(type) || !allowedEmotions.Contains(type))
        {
            Debug.LogWarning("�˻��� ���� ������ ��ȿ���� �ʽ��ϴ�.");
            return new List<Emotion>();
        }

        List<Emotion> result = emotionList.FindAll(e => e.EmotionType.Equals(type, StringComparison.OrdinalIgnoreCase));
        Debug.Log($"{result.Count}���� ������ '{type}' �������� �˻���.");
        return result;
    }

    // ������: ��� ���� �����͸� ����ϴ� �޼���
    public void PrintAllEmotions()
    {
        Debug.Log("���� ����� ���� ���:");
        foreach (var emotion in emotionList)
        {
            Debug.Log($"- {emotion.EmotionType}: {emotion.Description} (����: {emotion.Intensity}, ��� �ð�: {emotion.RecordedAt})");
        }
    }
    void Start()
    {
        // EmotionManager ��ũ��Ʈ�� ���� ���� �����͸� �߰�
        EmotionManager manager = GetComponent<EmotionManager>();

        // ���� �����͸� �߰� (��: Joy, ����)
        manager.AddEmotion("Joy", "����� �����ϴ�.", 5);
        manager.AddEmotion("Sad", "������ �����ϴ�.", 8);

        // ����� ��� ������ ����� �α׿� ���
        manager.PrintAllEmotions();
    }

}
