using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class LanguangeManager : MonoBehaviour
{
    public static LanguangeManager i;
    public Language.LanguageType ActualLanguage { 
        get
        { 
            return actualLanguage; 
        } 
        set 
        { 
            actualLanguage = value; 
            SetActualLanguageToCloud(); 
            OnLanguageChanged?.Invoke(this, EventArgs.Empty); 
        }}

    [SerializeField] private List<GameWordsSO> gameWordSOList;
    private Dictionary<Language.LanguageType, GameWordsSO> languageWords;
    private Language.LanguageType actualLanguage;

    public event EventHandler OnLanguageChanged;

    private void Awake()
    {
        if (LanguangeManager.i == null) 
        { 
            i = this; 
            InitLanguageWordsDictionary(); 
            CheckActualLanguage();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public GameWordsSO GetWords()
    {
        return languageWords[actualLanguage];
    }

    private void InitLanguageWordsDictionary()
    {
        languageWords = new Dictionary<Language.LanguageType, GameWordsSO>();
        foreach(var gameWordsSO in gameWordSOList)
        {
            languageWords.Add(gameWordsSO.languageType, gameWordsSO);
        }
    }

    private void CheckActualLanguage()
    {
        ActualLanguage = SteamCloudDataManager.i.SteamCloudPrefs.ActualLanguage;
    }

    private void SetActualLanguageToCloud()
    {
        SteamCloudDataManager.i.SteamCloudPrefs.ActualLanguage = actualLanguage;
    }
}
