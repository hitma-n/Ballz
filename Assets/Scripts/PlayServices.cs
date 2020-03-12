using GooglePlayGames;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayServices : MonoBehaviour {

    void Awake()
    {
        //PlayerPrefs.DeleteAll();
        DontDestroyOnLoad(transform.gameObject);
    }

    // Use this for initialization
    void Start()
    {
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;

        // Activate the Google Play Games platform
        PlayGamesPlatform.Activate();

        LogIn();


    }

    public static void OnShowLeaderBoard()
    {
        //        Social.ShowLeaderboardUI (); // Show all leaderboard
        //((PlayGamesPlatform)Social.Active).ShowLeaderboardUI(leaderboard);
        // Show current (Active) leaderboard

        if (Social.localUser.authenticated)
        {
            OnAddScoreToLeaderBorad(PlayerPrefs.GetInt("highScore"));
            Social.ShowLeaderboardUI();
        }

    }

    public static void OnAddScoreToLeaderBorad(int score)
    {
        if (Social.localUser.authenticated)
        {

            Social.ReportScore(score, GPGSIds.leaderboard_high_score, (bool success) =>
            {
                if (success)
                {
                    Debug.Log("Update Score Success");

                }
                else
                {
                    Debug.Log("Update Score Fail");
                }
            });


        }
    }

    public void LogIn()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login Sucess");
            }
            else
            {
                Debug.Log("Login failed");
            }
        });
    }

}
