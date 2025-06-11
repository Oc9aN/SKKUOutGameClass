using System;

public class Account
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    public Account(string email, string nickname, string password)
    {
        // 규칙은 캡슐화해서 분리.
        // 도메인과 UI는 이 규칙을 검사
        // 캡슐화된 규칙 : 명세(Specification) 구조
        
        // 이메일 검증
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        // 닉네임 검증
        var nickNameSpecification = new AccountNickNameSpecification();
        if (!nickNameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nickNameSpecification.ErrorMessage);
        }

        // 비밀번호 검증
        

        Email = email;
        Nickname = nickname;
        Password = password;
    }

    public AccountDto ToDto()
    {
        return new AccountDto(Email, Nickname, Password);
    }
}