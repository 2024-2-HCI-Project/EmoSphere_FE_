using UnityEngine;
using TMPro;
public class KeyboardManager : MonoBehaviour
{
    public TMP_InputField inputField;

    void Start()
    {

        inputField.onSelect.AddListener(OpenKeyboard);
    }

    void OpenKeyboard(string text)
    {
        TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default);
    }
}

