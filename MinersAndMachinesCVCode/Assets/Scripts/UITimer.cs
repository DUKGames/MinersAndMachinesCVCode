using UnityEngine;
using TMPro;

public class UITimer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TextMeshProUGUI text;

    private TimeCounter timer;
    private int startDays;

    public void SetTimer(TimeCounter timer)
    {
        this.timer = timer;
        this.timer.OnTimeChanged += Timer_OnTimeChanged;
    }

    public void Setup()
    {
        startDays = Levels.i.GetLevelMaxGameplayDayTime(LevelSetup.i.ActualLevel);
        ShowTime(timer.GetActualTime());
    }

    private void Timer_OnTimeChanged(object sender, TimeCounter.TimerEventArgs e)
    {
        ShowTime(e.actualTime);
    }

    private void ShowTime(int actualTime)
    {
        text.text = (startDays - actualTime).ToString();
    }
}
