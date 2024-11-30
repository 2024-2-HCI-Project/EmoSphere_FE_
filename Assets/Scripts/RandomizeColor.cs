using UnityEngine;

public class RandomizeColor : MonoBehaviour
{
    public Color minColor = Color.white;  // 색상의 최소값 (흰색)
    public Color maxColor = Color.red;    // 색상의 최대값 (빨간색)

    void Start()
    {
        // Renderer 컴포넌트를 가져오기
        Renderer renderer = GetComponent<Renderer>();
        
        if (renderer != null)
        {
            // 랜덤한 색상을 생성 (R, G, B 값을 각각 랜덤하게 설정)
            Color randomColor = new Color(
                Random.Range(minColor.r, maxColor.r),  // 빨간색 값
                Random.Range(minColor.g, maxColor.g),  // 초록색 값
                Random.Range(minColor.b, maxColor.b)   // 파란색 값
            );

            // 생성된 랜덤 색상을 오브젝트의 Material에 적용
            renderer.material.color = randomColor;
        }
        else
        {
            Debug.LogError("Renderer가 이 오브젝트에 없습니다!");
        }
    }
}
