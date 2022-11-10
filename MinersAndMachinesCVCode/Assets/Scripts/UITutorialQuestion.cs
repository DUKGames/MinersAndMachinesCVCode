using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITutorialQuestion : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private UIParcels uiParcels;
    [SerializeField] private GameObject questionPanel;

    private void OnEnable()
    {
        if (TutorialManager.i == null) 
            return;

        CheckNeedShowPanel();
    }

    public void SetQuestionPanelState(bool newState)
    {
        questionPanel.SetActive(newState);
    }

    public void SetPlayTutorial(bool newState)
    {
        TutorialManager.i.PlayTutorial = newState;
    }

    public bool IsTutorialQuestionEnable()
    {
        return questionPanel.activeInHierarchy;
    }

    private bool GetPlayTutorialState()
    {
        return TutorialManager.i.PlayTutorial;
    }

    private void CheckNeedShowPanel()
    {
        if (GetPlayTutorialState())
        {
            SetQuestionPanelState(true);
        }
    }
}
