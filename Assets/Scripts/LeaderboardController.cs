using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    public Text name1;
    public Text score1;
    public Text name2;
    public Text score2;
    public Text name3;
    public Text score3;

    string[] names = new string[3];
    int[] scores;

    private void OnEnable()
    {
        StartCoroutine(WaitAndGoNextStage());
    }

    IEnumerator WaitAndGoNextStage()
    {
        Debug.Log("activating leaderboard");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        yield return new WaitForSeconds(3.0f);
        player.SendMessage("GoToNextStage");
    }

    void InitializeScores()
    {
        scores = new int[3];

        for (int i = 0; i < 3; i++)
        {
            scores[i] = -1;
        }
    }

    public bool IsHighScore(int score)
    {
        if (scores == null)
        {
            InitializeScores();
            return true;
        }

        for (int i = 0; i < scores.Length; i++)
        {
            if (score > scores[i])
            {
                return true;
            }
        }

        return false;
    }

    public void AddScore(string name, int score)
    {
        int newScorePos = -1;

        for (int i = 0; i < scores.Length; i++)
        {
            if (score > scores[i])
            {
                newScorePos = i;
                break;
            }
        }

        if (newScorePos == -1)
            return;

        for (int i = scores.Length - 1; i > newScorePos; i--)
        {
            names[i] = names[i - 1];
            scores[i] = scores[i - 1];
        }

        names[newScorePos] = name;
        scores[newScorePos] = score;

        if (scores[0] != -1)
        {
            name1.text = names[0];
            score1.text = scores[0].ToString();
        }

        if (scores[1] != -1)
        {
            name2.text = names[1];
            score2.text = scores[1].ToString();
        }

        if (scores[2] != -1)
        {
            name3.text = names[2];
            score3.text = scores[2].ToString();
        }
    }
}
