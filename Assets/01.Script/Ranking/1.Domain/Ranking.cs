using System;

public class Ranking
{
    public int Rank { get; private set; }
    public readonly string Email;
    public readonly string Nickname;
    public int Score { get; private set; }
    public float Time { get; private set; }

    public Ranking(string email, string nickname, int score, float time)
    {
        // email과 nickname의 명세에 대해서 다시 검사
        var emailSpecification = new AccountEmailSpecification();
        if (!emailSpecification.IsSatisfiedBy(email))
        {
            throw new Exception(emailSpecification.ErrorMessage);
        }

        var nickNameSpecification = new AccountNickNameSpecification();
        if (!nickNameSpecification.IsSatisfiedBy(nickname))
        {
            throw new Exception(nickNameSpecification.ErrorMessage);
        }

        if (score < 0)
        {
            throw new Exception("올바르지 못한 점수입니다.");
        }
        
        if (time < 0)
        {
            throw new Exception("올바르지 못한 시간입니다.");
        }
        
        Email = email;
        Nickname = nickname;
        Score = score;
        Time = time;
    }

    public void SetRank(int rank)
    {
        if (rank <= 0)
        {
            throw new Exception("올바르지 못한 랭킹입니다.");
        }
        
        Rank = rank;
    }

    public void AddScore(int score)
    {
        if (score <= 0)
        {
            throw new Exception("올바르지 못한 점수입니다.");
        }
        
        Score += score;
    }

    public void SetTime(float time)
    {
        if (time < 0)
        {
            throw new Exception("올바르지 못한 시간입니다.");
        }
        
        Time = time;
    }

    public RankingDTO ToDTO()
    {
        return new RankingDTO(this);
    }
}