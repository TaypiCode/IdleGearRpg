using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float _time = 0;
    private bool _isWorking;
    private void Start()
    {
        StartCoroutine(UpdateTimer());
    }
    private IEnumerator UpdateTimer()
    {
        while (true)
        {
            if (_time > 0)
            {
                _isWorking = true;
                _time -= Time.deltaTime;
            }
            else
            {
                _isWorking = false;
            }
            yield return new WaitForEndOfFrame();
        }
    }
    public void SetTimer(float time)
    {
        _time = time;
    }
    public float GetTime()
    {
        return _time;
    }
    public bool IsWorking()
    {
        return _isWorking;
    }
}
