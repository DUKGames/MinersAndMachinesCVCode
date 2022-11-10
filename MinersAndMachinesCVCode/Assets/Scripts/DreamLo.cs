using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class DreamLo : MonoBehaviour
{
    private const string webURL = "http://dreamlo.com/lb/";

    [Header("Variables")]
    [SerializeField] private List<LandRankingKeys> landRankingKeysList;

    public event EventHandler<Checkname> OnCheckedName;
    public event EventHandler<Checkname> OnAddedScore;
    public event EventHandler<Checkname> OnDeletedScore;
    public event EventHandler<Checkname> OnDownloadedScores;
    public event EventHandler<Checkname> OnDownloadedScore;

    public class Checkname : EventArgs
    {
        public bool isFree;
        public string checkedName;
    }

    public void AddScore(string nickname, int landType, int score)
    {
        StartCoroutine(AddScoreToDatabase(nickname, landRankingKeysList[landType].privateKey, score));
    }

    public void CheckNick(string nick, int landType)
    {
        StartCoroutine(CheckNickname(nick, landRankingKeysList[landType].publicKey));
    }

    public void DeleteNick(string nick, int landType, string newNickname)
    {
        StartCoroutine(DeleteNickname(nick, landRankingKeysList[landType].privateKey, newNickname));
    }

    public void DownloadScores(int landType)
    {
        StartCoroutine(DownloadLaderboard(landRankingKeysList[landType].publicKey));
    }

    public void DownloadScore(string nick, int landType)
    {
        StartCoroutine(DownloadYourScore(nick, landRankingKeysList[landType].publicKey));
    }

    // Coroutine to add score to dreamlo database
    private IEnumerator AddScoreToDatabase(string nickname, string privateKey, int score)
    {
        UnityWebRequest www = new UnityWebRequest(webURL + privateKey + "/add/" + UnityWebRequest.EscapeURL(nickname) + "/" + score);
        yield return www.SendWebRequest();

        if (string.IsNullOrEmpty(www.error))
        {
            if (OnAddedScore != null) OnAddedScore(this, new Checkname {isFree = true, checkedName = nickname});
        }
        else
        {
            if (OnAddedScore != null) OnAddedScore(this, new Checkname { isFree = false, checkedName = nickname });
        }
    }

    // Coroutine to download full leaderboard in one string of characters
    private IEnumerator DownloadLaderboard(string publicKey)
    {
        UnityWebRequest www = UnityWebRequest.Get(webURL + publicKey + "/pipe/50");
        yield return www.SendWebRequest();

        if(string.IsNullOrEmpty(www.error))
        {
            if(OnDownloadedScores != null) OnDownloadedScores(this, new Checkname { isFree = true, checkedName = www.downloadHandler.text });
        }
        else
        {
            if(OnDownloadedScores != null) OnDownloadedScores(this, new Checkname { isFree = false, checkedName = www.downloadHandler.text });
        }
    }

    // Coroutine to download your score
    private IEnumerator DownloadYourScore(string nickname, string publicKey)
    {
        UnityWebRequest www = UnityWebRequest.Get(webURL + publicKey + "/pipe-get/" + UnityWebRequest.EscapeURL(nickname));
        yield return www.SendWebRequest();

        if (string.IsNullOrEmpty(www.error))
        {
            if (OnDownloadedScore != null) OnDownloadedScore(this, new Checkname { isFree = true, checkedName = www.downloadHandler.text});
        }
        else
        {
            if (OnDownloadedScore != null) OnDownloadedScore(this, new Checkname { isFree = false, checkedName = www.downloadHandler.text});
        }
    }

    // Coroutine to check whether nickname is already in use or is free
    private IEnumerator CheckNickname(string nickname, string publicKey)
    {
        UnityWebRequest www = UnityWebRequest.Get(webURL + publicKey + "/pipe-get/" + UnityWebRequest.EscapeURL(nickname));
        yield return www.SendWebRequest();

        if(string.IsNullOrEmpty(www.error))
        {
            bool isFree;
            if(www.downloadHandler.text.Length >= 1)
            {
                isFree = false;
            }
            else
            {
                isFree = true;
            }
            if (OnCheckedName != null) OnCheckedName(this, new Checkname { isFree = isFree });
        }
        else
        {
            if (OnCheckedName != null) OnCheckedName(this, new Checkname { isFree = false });
        }
    }

    // Coroutine to delete your score from leaderboard
    private IEnumerator DeleteNickname(string nickname, string privateKey, string newNickname)
    {
        UnityWebRequest www = new UnityWebRequest(webURL + privateKey + "/delete/" + UnityWebRequest.EscapeURL(nickname));
        yield return www.SendWebRequest();

        if(string.IsNullOrEmpty(www.error))
        {
            if (OnDeletedScore != null) OnDeletedScore(this, new Checkname { isFree = true, checkedName = newNickname});
        }
        else
        {
            if (OnDeletedScore != null) OnDeletedScore(this, new Checkname { isFree = false, checkedName = newNickname});
        }
    }
}

[System.Serializable]
public class LandRankingKeys
{
    public string privateKey;
    public string publicKey;
}
