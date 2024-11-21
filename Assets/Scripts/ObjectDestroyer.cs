using System.Collections;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    public float burnDuration = 2.0f; // 오브젝트가 불타는 애니메이션 지속 시간

    // 감정 오브젝트를 소멸시키는 메서드
    public void DestroyObject(GameObject emotionObject)
    {
        if (emotionObject == null)
        {
            Debug.LogWarning("소멸시킬 오브젝트가 없습니다.");
            return;
        }

        // 불타는 애니메이션 시작
        StartCoroutine(BurnAndDestroy(emotionObject));
    }

    // 오브젝트를 불태우고 소멸시키는 코루틴
    private IEnumerator BurnAndDestroy(GameObject emotionObject)
    {
        // 오브젝트의 Material을 불타는 색상으로 변경
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

                // 불타는 효과: 색상을 점점 붉게 변경
                material.color = Color.Lerp(originalColor, Color.red, progress);
                yield return null;
            }
        }

        // 불타는 애니메이션 후 오브젝트 제거
        Destroy(emotionObject);
        Debug.Log("오브젝트가 소멸되었습니다.");
    }

    // 디버깅용: 임의의 오브젝트를 소멸시키는 테스트 메서드
    public void TestDestroyObject(GameObject testObject)
    {
        DestroyObject(testObject);
    }
}
