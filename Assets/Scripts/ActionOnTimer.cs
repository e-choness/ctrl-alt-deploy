using System;
using UnityEngine;

public class ActionOnTimer : MonoBehaviour
{
    // The callback is defined directly through lambda expression in TestingTesting script
    private Action _timerCallback;
    private float _timer;

    public void SetTimer(float timer, Action timerCallback)
    {
        this._timer = timer;
        this._timerCallback = timerCallback;
    }

    // Update is called once per frame
    private void Update()
    {
        if (_timer > 0f)
        {
            _timer -= Time.deltaTime;

            if (IsTimerComplete())
            {
                _timerCallback();
            }
        }
    }

    public bool IsTimerComplete()
    {
        return _timer <= 0f;
    }
}
