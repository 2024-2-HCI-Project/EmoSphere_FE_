using System.Collections.Generic;
using System;
using UnityEngine;

public class EmotionVisualizer : MonoBehaviour
{
    [Serializable]
    public class EmotionObject
    {
        public GameObject ObjectPrefab; // ������ �ð�ȭ�� 3D ������Ʈ ������
        public string EmotionType;      // ���� ����
    }

    public List<EmotionObject> emotionPrefabs = new List<EmotionObject>(); // ���� ������ ������Ʈ ������ ����Ʈ

    private Dictionary<string, GameObject> emotionPrefabDict = new Dictionary<string, GameObject>(); // ���� ������ ���� ��ųʸ�

    void Start()
    {
        // ��ųʸ��� ���� ������ ������ ����
        foreach (var emotion in emotionPrefabs)
        {
            if (!emotionPrefabDict.ContainsKey(emotion.EmotionType))
            {
                emotionPrefabDict[emotion.EmotionType] = emotion.ObjectPrefab;
            }
        }
    }

    // ���� ������ ���� 3D ������Ʈ�� �����ϴ� �޼���
    public GameObject VisualizeEmotion(string emotionType, Vector3 position, float size, Color color)
    {
        if (!emotionPrefabDict.ContainsKey(emotionType))
        {
            Debug.LogWarning($"'{emotionType}'�� �������� �ʴ� ���� �����Դϴ�.");
            return null;
        }

        // ������Ʈ ����
        GameObject prefab = emotionPrefabDict[emotionType];
        GameObject instance = Instantiate(prefab, position, Quaternion.identity);

        // ������Ʈ ũ��� ���� ����
        instance.transform.localScale = Vector3.one * size;
        var renderer = instance.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }

        Debug.Log($"{emotionType} ���� ������Ʈ ���� �Ϸ�: ��ġ {position}, ũ�� {size}, ���� {color}");
        return instance;
    }

    // ������: ������ ���� ������Ʈ�� �����ϴ� �׽�Ʈ �޼���
    public void TestVisualizeEmotion()
    {
        VisualizeEmotion("Joy", new Vector3(0, 1, 0), 1.5f, Color.yellow);
    }
}
