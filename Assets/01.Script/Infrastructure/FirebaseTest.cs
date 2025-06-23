using System;
using System.Collections.Generic;
using Firebase.Extensions;
using Firebase;
using Firebase.Auth;
using Firebase.Firestore;
using UnityEngine;

public class FirebaseTest : MonoBehaviour
{
    private FirebaseApp _app;
    private FirebaseAuth _auth;
    private FirebaseFirestore _db;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        // 파이어 베이스에 연결
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            var dependencyStatus = task.Result;
            if (dependencyStatus == DependencyStatus.Available)
            {
                Debug.Log("파이어베이스 연결에 성공했습니다.");
                _app = FirebaseApp.DefaultInstance;
                _auth = FirebaseAuth.DefaultInstance;
                _db = FirebaseFirestore.DefaultInstance;

                Login();
            }
            else
            {
                Debug.Log($"파이어베이스 연결에 실패했습니다. {dependencyStatus}");
            }
        });
    }

    private void Register()
    {
        string email = "asd@naver.com";
        string password = "123456";

        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"회원 가입에 실패했습니다. {task.Exception.Message}");
                return;
            }

            // Firebase user has been created.
            AuthResult result = task.Result;
            Debug.Log($"회원 가입에 성공했습니다. {result.User.DisplayName} ({result.User.UserId})");
        });
    }

    private void Login()
    {
        string email = "asd@naver.com";
        string password = "123456";

        _auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"로그인에 실패했습니다. {task.Exception.Message}");
                return;
            }

            AuthResult result = task.Result;
            Debug.Log($"로그인에 성공했습니다. {result.User.DisplayName} ({result.User.UserId})");

            NicknameChange();
        });
        
        // AddRanking();
        // GetMyRanking();
        GetRankings();
    }

    private void NicknameChange()
    {
        var user = _auth.CurrentUser;
        if (user == null)
        {
            return;
        }

        var profile = new UserProfile
        {
            DisplayName = "asd"
        };

        user.UpdateUserProfileAsync(profile).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                Debug.LogError($"닉네임 변경에 실패했습니다. {task.Exception.Message}");
                return;
            }

            Debug.Log("닉네임 변경에 성공했습니다.");
        });
    }

    private void GetProfile()
    {
        FirebaseUser user = _auth.CurrentUser;
        if (user == null)
        {
            return;
        }

        string nickname = user.DisplayName;
        string email = user.Email;

        Account account = new Account(email, nickname, "password");
    }

    private void AddRanking()
    {
        Ranking ranking = new Ranking("qwer@naver.com", "qwer", 1000, 60);
        
        Dictionary<string, object> rankingDict = new Dictionary<string, object>
        {
            { "Email", ranking.Email },
            { "Nickname", ranking.Nickname },
            { "Score", ranking.Score }
        };
        // 중복된 ID의 경우 업데이트, 없는 ID의 경우 생성을 한다.
        _db.Collection("rankings").Document(ranking.Email).SetAsync(rankingDict).ContinueWithOnMainThread(task =>
        {
            Debug.Log(String.Format("Added document with ID: {0}.", task.Id));
        });
    }

    private void GetMyRanking()
    {
        string email = "asd@naver.com"; // ID 역할

        DocumentReference docRef = _db.Collection("rankings").Document(email);
        docRef.GetSnapshotAsync().ContinueWithOnMainThread(task =>
        {
            DocumentSnapshot snapshot = task.Result;
            if (snapshot.Exists)
            {
                Debug.Log(String.Format("Document data for {0} document:", snapshot.Id));
                var rankingDict = snapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in rankingDict)
                {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }
            }
            else
            {
                Debug.Log(String.Format("Document {0} does not exist!", snapshot.Id));
            }
        });
    }

    private void GetRankings()
    {
        Query allRankingsQuery = _db.Collection("rankings");
        allRankingsQuery.GetSnapshotAsync().ContinueWithOnMainThread(task => {
            QuerySnapshot allRankingsQuerySnapshot = task.Result;
            Debug.Log("랭킹을 출력합니다.");
            foreach (DocumentSnapshot documentSnapshot in allRankingsQuerySnapshot.Documents) {
                Debug.Log(String.Format("Document data for {0} document:", documentSnapshot.Id));
                Dictionary<string, object> ranking = documentSnapshot.ToDictionary();
                foreach (KeyValuePair<string, object> pair in ranking) {
                    Debug.Log(String.Format("{0}: {1}", pair.Key, pair.Value));
                }

                // Newline to separate entries
                Debug.Log("-----------------------");
            };
        });
        // 이 데이터를 RankingData로 변환해서 사용
    }
}