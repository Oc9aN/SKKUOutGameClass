using System;
using System.Text.RegularExpressions;

public class AccountNickNameSpecification : ISpecification<String>
{
    private string _errorMessage;
    
    // 닉네임: 한글 또는 영어로 구성, 2~7자
    private static readonly Regex NicknameRegex = new Regex(@"^[가-힣a-zA-Z0-9]{2,12}$", RegexOptions.Compiled);

    // 금지된 닉네임 (비속어 등)
    private static readonly string[] ForbiddenNicknames = { "바보", "멍청이", "운영자", "김홍일" };
    
    public bool IsSatisfiedBy(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            _errorMessage = "닉네임은 비어있을 수 없습니다.";
            return false;
        }

        if (!NicknameRegex.IsMatch(value))
        {
            _errorMessage = "닉네임은 2자 이상 12자 이하의 한글 또는 영문이어야 합니다.";
            return false;
        }
        
        foreach (var forbidden in ForbiddenNicknames)
        {
            if (value.Contains(forbidden))
            {
                _errorMessage = $"닉네임에 부적절한 단어가 포함되어 있습니다: {forbidden}";
                return false;
            }
        }
        
        return true;
    }

    public string ErrorMessage => _errorMessage;
}