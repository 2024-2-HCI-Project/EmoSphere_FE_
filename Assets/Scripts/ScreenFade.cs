using UnityEngine;
using UnityEngine.UI;

public class ScreenFade : MonoBehaviour
{
    public Image fadeImage; // UI 이미지 연결
    private float fadeSpeed = 1f / 50f; // 40초 동안 알파값 0 → 1
    private float alpha = 0f; // 초기 알파 값

    void Update()
    {
        if (alpha < 1)
        {
            alpha += Time.deltaTime * fadeSpeed; // 알파 값 증가
            alpha = Mathf.Clamp01(alpha); // 알파 값이 1을 초과하지 않도록 제한
            Color color = fadeImage.color;
            color.a = alpha;
            fadeImage.color = color;
        }
    }
}
