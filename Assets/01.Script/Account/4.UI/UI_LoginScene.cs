using System;
using System.Security.Cryptography;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public class UI_AccountInputFields
{
    public TextMeshProUGUI ResultText; // 결과 텍스트
    public TMP_InputField EmailInputField;
    public TMP_InputField NickNameInputField;
    public TMP_InputField PWInputField;
    public TMP_InputField PWConfirmInputField;
    public Button ConfirmButton; // 로그인 or 회원가입 버튼
}

public class UI_LoginScene : MonoBehaviour
{
    private const string PREFIX = "ID_";

    [Header("패널")]
    [SerializeField]
    private GameObject _loginPanel;

    [SerializeField]
    private GameObject _registerPanel;

    [Header("로그인")]
    [SerializeField]
    private UI_AccountInputFields _loginInputField;

    [Header("회원가입")]
    [SerializeField]
    private UI_AccountInputFields _registerInputField;

    [Header("버튼")]
    [SerializeField]
    private Button _goToLoginButton;

    [SerializeField]
    private Button _goToRegisterButton;

    private void Start()
    {
        // PlayerPrefs.DeleteAll();

        GoToLogin();

        _goToLoginButton.onClick.AddListener(OnClickLoginButton);
        _goToRegisterButton.onClick.AddListener(OnClickRegisterButton);

        _registerInputField.ConfirmButton.onClick.AddListener(Register);
        _loginInputField.ConfirmButton.onClick.AddListener(Login);

        _loginInputField.EmailInputField.onValueChanged.AddListener((_) => LoginCheck());
        _loginInputField.PWInputField.onValueChanged.AddListener((_) => LoginCheck());
    }

    private void OnClickLoginButton()
    {
        GoToLogin();
    }

    private void OnClickRegisterButton()
    {
        GoToRegister();
    }

    private void GoToLogin(string tempID = null)
    {
        _loginPanel.SetActive(true);
        _registerPanel.SetActive(false);

        _loginInputField.ResultText.text = string.Empty;
        _loginInputField.EmailInputField.text = string.IsNullOrEmpty(tempID) ? string.Empty : tempID;
        _loginInputField.PWInputField.text = string.Empty;
    }

    private void GoToRegister()
    {
        _loginPanel.SetActive(false);
        _registerPanel.SetActive(true);

        _registerInputField.ResultText.text = string.Empty;
    }

    // 회원가입
    private void Register()
    {
        // 이메일 검사
        string email = _registerInputField.EmailInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            _registerInputField.ResultText.text = emailSpecification.ErrorMessage;
            return;
        }

        string nickName = _registerInputField.NickNameInputField.text;
        var nickNameSpecification = new AccountNickNameSpecification();
        if (!nickNameSpecification.IsSatisfiedBy(nickName))
        {
            _registerInputField.ResultText.text = nickNameSpecification.ErrorMessage;
            return;
        }

        // 비밀번호 검사
        string pw = _registerInputField.PWInputField.text;
        var pwSpecification = new AccountPasswordSpecification();
        if (!pwSpecification.IsSatisfiedBy(pw))
        {
            _registerInputField.ResultText.text = pwSpecification.ErrorMessage;
            return;
        }

        // 2차 비밀번호 검사
        string pwConfirm = _registerInputField.PWConfirmInputField.text;
        if (!pwSpecification.IsSatisfiedBy(pwConfirm))
        {
            _registerInputField.ResultText.text = pwSpecification.ErrorMessage;
            return;
        }

        if (!string.Equals(pw, pwConfirm))
        {
            _registerInputField.ResultText.text = "비밀번호 확인 결과가 다릅니다.";
            return;
        }

        var result = AccountManager.instance.TryRegister(email, nickName, pw);
        if (result.IsSuccess)
        {
            GoToLogin(email);
        }
        else
        {
            _registerInputField.ResultText.text = result.Message;
        }
    }

    private void Login()
    {
        string email = _loginInputField.EmailInputField.text;
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            _loginInputField.ResultText.text = emailSpecification.ErrorMessage;
            return;
        }

        string pw = _loginInputField.PWInputField.text;
        var pwSpecification = new AccountPasswordSpecification();
        if (!pwSpecification.IsSatisfiedBy(pw))
        {
            _loginInputField.ResultText.text = pwSpecification.ErrorMessage;
            return;
        }

        if (AccountManager.instance.TryLogin(email, pw))
        {
            SceneManager.LoadScene(1);
        }
    }

    private void LoginCheck()
    {
        string id = _loginInputField.EmailInputField.text;
        string pw = _loginInputField.PWInputField.text;

        _loginInputField.ConfirmButton.enabled = !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(pw);
    }
}