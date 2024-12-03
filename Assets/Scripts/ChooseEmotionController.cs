using UnityEngine;
using UnityEngine.SceneManagement;

public class ChooseEmotionController : MonoBehaviour
{
    public void SelectEmotion(string emotionType)
    {
        // 선택한 감정을 EmotionDataManager에 저장
        EmotionDataManager.Instance.SetEmotion(emotionType);

        // 다음 씬으로 전환
        SceneManager.LoadScene("YourNextSceneName"); // 다음 씬 이름 입력
    }
}
