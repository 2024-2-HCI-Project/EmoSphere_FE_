using UnityEngine;
using UnityEngine.UI;

public class StartButtonController : MonoBehaviour
{
    public ParticleSystem particleSystem; // 파티클 시스템
    public GameObject emotionObject;      // 변화할 오브젝트
    public Image fadeImage;               // 알파값이 진해질 Image
    private bool isRunning = false;
    private float duration = 50f;         // 전체 과정 시간
    private float elapsedTime = 0f;       // 경과 시간

    private Vector3 initialScale;         // 초기 크기
    private Color initialColor;           // 오브젝트 초기 색상
    private Color fadeImageInitialColor;  // Image 초기 색상

    void Start()
    {
        // 오브젝트 초기 크기와 색상 저장
        initialScale = emotionObject.transform.localScale;
        Renderer renderer = emotionObject.GetComponent<Renderer>();
        if (renderer != null)
        {
            initialColor = renderer.material.color;
        }

        // Fade Image 초기 색상 설정
        fadeImageInitialColor = fadeImage.color;
    }

    public void OnStartButtonClick()
    {
        if (!isRunning)
        {
            isRunning = true;
            particleSystem.Play(); // 파티클 이펙트 실행
        }
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / duration;

            // 오브젝트 크기 점점 줄이기
            emotionObject.transform.localScale = Vector3.Lerp(initialScale, Vector3.zero, t);

            // 오브젝트 색상 점점 흐리게 (알파값 감소)
            Renderer renderer = emotionObject.GetComponent<Renderer>();
            if (renderer != null)
            {
                Color currentColor = Color.Lerp(initialColor, new Color(0, 0, 0, 0), t);
                renderer.material.color = currentColor;
            }

            // 화면 알파값 점점 증가
            fadeImage.color = new Color(
                fadeImageInitialColor.r,
                fadeImageInitialColor.g,
                fadeImageInitialColor.b,
                Mathf.Lerp(0f, 1f, t) // 알파값 점점 증가
            );

            // 10초 후 완료
            if (elapsedTime >= duration)
            {
                isRunning = false;
                particleSystem.Stop();
            }
        }
    }
}
