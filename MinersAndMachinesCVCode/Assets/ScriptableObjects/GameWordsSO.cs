using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "GameWordsSO", menuName = "ScriptableObjects/GameWordsSO")]
public class GameWordsSO : ScriptableObject
{
    public Language.LanguageType languageType;

    public List<string> mainMenuWords;
    public string creditsWord;

    [TextArea(5, 10)]
    public List<string> newsletterWords;

    public List<string> optionsWords;
    public string privacyPolicyButtonWord;

    public List<string> languagesWords;

    [TextArea(5, 10)]
    public string privacyPolicyTextWords;

    public List<string> menuMapWords;
    public List<string> rankingLandsListWords;
    public List<string> rankingLandsTitleWords;
    public string yourPositionWords;
    public string tutorialAskWords;
    public string bankCreditWordsOne;
    public string bankCreditWordsTwo;

    public string buyWord;
    public string selectUpgradeWords;
    public List<string> upgradeDestriptionsWords;

    public string backWord;

    public List<TMP_FontAsset> fontsList;

    // ---GAME---

    public List<string> pausePanelWords;
    public string notEnoughMoneyWord;
    public List<string> tutorialWords;
    public string clickToContinueWord;
}
