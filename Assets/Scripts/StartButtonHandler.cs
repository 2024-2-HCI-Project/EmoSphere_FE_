using UnityEngine;
using System.Collections;

public class StartButtonHandler : MonoBehaviour
{
    public ParticleSystem fireEffect;    // 불꽃 파티클 시스템
    public GameObject targetObject;     // 3D 오브젝트
    private Material dynamicMaterial;   // 동적으로 생성한 Material
    private float fadeSpeed = 0.15f;    // 투명도 감소 속도 (10초 안에 완료)
    private float scaleSpeed = 0.2f;    // 크기 감소 속도 (10초 안에 완료)
    private float alpha = 1.0f;         // 초기 투명도

    public void OnStartButtonClick()
    {
        // 불꽃 효과 실행
        if (fireEffect != null)
        {
            fireEffect.Play();
        }

        // Material 동적 생성 및 적용
        if (targetObject != null)
        {
            MeshRenderer renderer = targetObject.GetComponent<MeshRenderer>();
            if (renderer != null)
            {
                // 새 Material 생성 및 설정
                dynamicMaterial = new Material(Shader.Find("Standard"));
                dynamicMaterial.SetFloat("_Mode", 2); // Fade 모드
                dynamicMaterial.color = new Color(1, 1, 1, 1); // 초기 투명도
                renderer.material = dynamicMaterial;
            }

            // 크기 및 투명도 처리 시작
            StartCoroutine(ScaleAndFadeObject());
        }
    }

    private IEnumerator ScaleAndFadeObject()
    {
        Transform objectTransform = targetObject.transform;

        float elapsedTime = 0f; // 진행 시간

        while (alpha > 0 && objectTransform.localScale.x > 0 && elapsedTime < 10f)
        {
            // 10초 안에 끝나도록 시간 기반으로 조정
            elapsedTime += Time.deltaTime;

            // 투명도 감소
            alpha -= Time.deltaTime * fadeSpeed * 10; // 10초 안에 투명화 완료
            if (dynamicMaterial != null)
            {
                Color color = dynamicMaterial.color;
                color.a = Mathf.Clamp01(alpha); // 알파 값이 0~1 사이로 유지되도록
                dynamicMaterial.color = color;
            }

            // 크기 감소
            Vector3 scale = objectTransform.localScale;
            scale -= Vector3.one * Time.deltaTime * scaleSpeed * 10; // 10초 안에 크기 감소 완료
            scale = Vector3.Max(scale, Vector3.zero); // 음수로 가지 않도록 최소값 제한
            objectTransform.localScale = scale;

            yield return null;
        }
    }
}
