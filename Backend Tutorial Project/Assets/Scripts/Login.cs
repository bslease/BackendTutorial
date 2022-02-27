using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Login : MonoBehaviour
{
    public InputField UsernameInput;
    public InputField PasswordInput;
    public Button LoginButton;
    public Button RegisterButton;
    public GameObject RegisterUI;

    void Start()
    {
        LoginButton.onClick.AddListener(() => {
            StartCoroutine(Main.Instance.Web.Login(UsernameInput.text, PasswordInput.text));
        });

        RegisterButton.onClick.AddListener(() => {
            //StartCoroutine(Main.Instance.Web.RegisterUser(UsernameInput.text, PasswordInput.text));
            RegisterUI.SetActive(true);
            this.gameObject.SetActive(false);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
