using System.Collections;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public float burnDuration = 2.0f; // ������Ʈ�� ��Ÿ�� �ִϸ��̼� ���� �ð�

    // ���� ������Ʈ�� �Ҹ��Ű�� �޼���
    public void DestroyObject(GameObject emotionObject)
    {
        if (emotionObject == null)
        {
            Debug.LogWarning("�Ҹ��ų ������Ʈ�� �����ϴ�.");
            return;
        }

        // ��Ÿ�� �ִϸ��̼� ����
        StartCoroutine(BurnAndDestroy(emotionObject));
    }

    // ������Ʈ�� ���¿�� �Ҹ��Ű�� �ڷ�ƾ
    private IEnumerator BurnAndDestroy(GameObject emotionObject)
    {
        // ������Ʈ�� Material�� ��Ÿ�� �������� ����
        Renderer renderer = emotionObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            Material material = renderer.material;
            Color originalColor = material.color;
            float elapsed = 0f;

            while (elapsed < burnDuration)
            {
                elapsed += Time.deltaTime;
                float progress = elapsed / burnDuration;

                // ��Ÿ�� ȿ��: ������ ���� �Ӱ� ����
                material.color = Color.Lerp(originalColor, Color.red, progress);
                yield return null;
            }
        }

        // ��Ÿ�� �ִϸ��̼� �� ������Ʈ ����
        Destroy(emotionObject);
        Debug.Log("������Ʈ�� �Ҹ�Ǿ����ϴ�.");
    }

    // ������: ������ ������Ʈ�� �Ҹ��Ű�� �׽�Ʈ �޼���
    public void TestDestroyObject(GameObject testObject)
    {
        DestroyObject(testObject);
    }
}
