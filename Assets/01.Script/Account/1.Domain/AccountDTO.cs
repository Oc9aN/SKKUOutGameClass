public class AccountDto
{
    public readonly string Email;
    public readonly string Nickname;
    public readonly string Password;

    public AccountDto(string email, string nickname, string password)
    {
        Email = email;
        Nickname = nickname;
        Password = password;
    }
}