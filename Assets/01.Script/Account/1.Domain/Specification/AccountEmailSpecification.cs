using System.Text.RegularExpressions;

public class AccountEmailSpecification : ISpecification<string>
{
    private string _errorMessage;
    
    // 이메일 정규표현식 (간단한 RFC5322 기반)
    private static readonly Regex EmailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.Compiled);
    
    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            _errorMessage = "이메일은 비어있을 수 없습니다.";
            return false;
        }

        if (!EmailRegex.IsMatch(value))
        {
            _errorMessage = "올바른 이메일 형식이 아닙니다.";
            return false;
        }

        return true;
    }

    public string ErrorMessage => _errorMessage;
}