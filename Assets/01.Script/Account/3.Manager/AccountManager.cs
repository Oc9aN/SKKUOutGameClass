using UnityEngine;

public class AccountManager : MonoBehaviour
{
    private const string SALT = "1004";
    
    public static AccountManager instance;

    public AccountDto CurrentAccount => _myAccount.ToDto();

    private Account _myAccount;
    
    private AccountRepository _accountRepository;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        Init();
    }

    private void Init()
    {
        // 초기화
        _accountRepository = new AccountRepository();
    }

    public Result TryRegister(string email, string nickName, string password)
    {
        // 중복 이메일 검사
        AccountDto savedAccount = _accountRepository.Find(email);
        if (savedAccount != null)
        {
            return new Result(false, "이메일이 중복되었습니다.");
        }
        
        string encryptionPassword = CryptoUtil.Encryption(password, SALT);
        Account account = new Account(email, nickName, encryptionPassword);
        
        // 레포 저장
        _accountRepository.TrySave(account.ToDto());
        Debug.Log("Account successfully registered");
        
        return new Result(true);
    }

    public bool TryLogin(string email, string password)
    {
        AccountDto saveData = _accountRepository.Find(email);
        if (saveData == null)
        {
            return false;
        }
        
        if (CryptoUtil.Verify(password, saveData.Password, SALT))
        {
            // 비밀번호가 같으므로 
            _myAccount = new Account(saveData.Email, saveData.Nickname, saveData.Password);
            Debug.Log("Account successfully logged in");
            return true;
        }
        return false;
    }
}