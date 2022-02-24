using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterUser : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public InputField PasswordConfirm;
    public Button SubmitButton;

    void Start()
    {
        SubmitButton.onClick.AddListener(() => {
            //StartCoroutine(Main.Instance.Web.RegisterUser(UsernameInput.text, PasswordInput.text));
            OnSubmitClicked();
        });
    }

    public void OnSubmitClicked()
    {
        if (PasswordInput.text != PasswordConfirm.text)
        {
            Debug.Log("creation failed - passwords do not match");
        }
        else
        {
            StartCoroutine(Main.Instance.Web.RegisterUser(UsernameInput.text, PasswordInput.text));
        }
    }

}
