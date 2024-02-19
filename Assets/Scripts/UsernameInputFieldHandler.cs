using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UsernameInputFieldHandler : MonoBehaviour
{
    TMP_InputField inputField;

    [SerializeField] TextMeshProUGUI placeholder;
    [SerializeField] TextMeshProUGUI inputFieldUGUI;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();

        inputField.onEndEdit.AddListener(GameManager.Instance.UpdateUsername);

        placeholder.text = "usuario";
        inputField.text = GameManager.Instance.username;
    }

    private void OnDestroy()
    {
        inputField.onEndEdit.RemoveListener(GameManager.Instance.UpdateUsername);
    }
}
