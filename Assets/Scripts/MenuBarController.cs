using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBarController : MonoBehaviour
{
    public void OnMenuButtonClick(string buttonName)
    {
        switch (buttonName)
        {
            case "MenuEmotionalButton":
                LoadScene("ChooseYourEmotion");
                break;

            case "MenuUniverseButton":
                LoadScene("MyUniverse");
                break;

            case "MenuSettingButton":
                LoadScene("MySettings");
                break;

            default:
                Debug.LogWarning($"'{buttonName}'는 유효하지 않은 버튼 이름입니다.");
                break;
        }
    }

    // 씬 로드 메서드
    private void LoadScene(string sceneName)
    {
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError($"'{sceneName}' 씬을 찾을 수 없습니다. 씬이 빌드 설정에 추가되었는지 확인하세요.");
        }
    }
}
