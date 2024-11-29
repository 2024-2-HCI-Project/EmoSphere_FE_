using UnityEngine;
using TMPro;

public class InputFieldActivator : MonoBehaviour
{
    public TMP_InputField inputField;

    public void ActivateInputField()
    {
        inputField.Select();
        inputField.ActivateInputField();
    }
}