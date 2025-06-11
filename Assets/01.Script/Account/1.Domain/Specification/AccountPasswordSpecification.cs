public class AccountPasswordSpecification : ISpecification<string>
{
    private string _errorMessage;
    
    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            _errorMessage = "비밀번호는 비어있을 수 없습니다.";
            return false;
        }

        if (value.Length < 6 || value.Length > 12)
        {
            _errorMessage = "비밀번호는 6자 이상 12자 이하이어야 합니다.";
            return false;
        }

        return true;
    }

    public string ErrorMessage => _errorMessage;
}