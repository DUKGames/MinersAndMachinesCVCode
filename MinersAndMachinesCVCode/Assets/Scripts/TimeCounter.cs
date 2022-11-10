using System.Collections;
using UnityEngine;
using System;

public class TimeCounter : MonoBehaviour
{
    private Action actionAfterEndCount;
    private Coroutine coroutine;
    private int numberEndCount;
    private int frameTime;
    private bool count;
    private int actualTime;

    public event EventHandler<TimerEventArgs> OnTimeChanged;

    public class TimerEventArgs : EventArgs
    {
        public int actualTime;
    }

    public void SetTimer(int frameTime, Action actionAfterCount, int numberEndCount)
    {
        count = true;
        actionAfterEndCount = actionAfterCount;
        this.numberEndCount = numberEndCount;
        this.frameTime = frameTime;
        coroutine = StartCoroutine(CountCoroutine());
    }

    public void PauseTimer()
    {
        if(coroutine != null) StopCoroutine(coroutine);
        count = false;
    }

    public void ResumeTimer()
    {
        count = true;
        coroutine = StartCoroutine(CountCoroutine());
    }

    public int GetActualTime()
    {
        return actualTime;
    }

    private IEnumerator CountCoroutine()
    {
        while (count)
        {
            yield return new WaitForSeconds(frameTime);
            actualTime++;
            OnTimeChanged?.Invoke(this, new TimerEventArgs { actualTime = actualTime });

            if (numberEndCount != -1)
            if (numberEndCount > 0)
            {
                if(numberEndCount <= actualTime)
                {
                    count = false;
                    if (actionAfterEndCount != null) actionAfterEndCount();
                    Destroy(gameObject);
                }
            }
        }
    }
}

public static class TimeCounterCreate
{
    public static TimeCounter CreateTimer(int frameTime, Action actionAfterCount = null, int numberEndCount = 0)
    {
        TimeCounter timer = new GameObject("timer", typeof(TimeCounter)).GetComponent<TimeCounter>();
        timer.SetTimer(frameTime, actionAfterCount, numberEndCount);
        return timer;
    }
}
