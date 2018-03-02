using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour {
    private float elapsedTime;
    private float endTime;
    private bool isWorking;
    private bool done;

	// Use this for initialization
	void Start () {
        RestartTimer();
	}
	
	// Update is called once per frame
	void Update () {
        HandleTimer();
	}

    private void HandleTimer()
    {
        if (isWorking)
        {
            elapsedTime += Time.deltaTime;
            if(elapsedTime >= endTime)
            {
                done = true;
                isWorking = false;
            }
        }
    }

    public void StartTimer(float endTime)
    {
        RestartTimer();
        this.endTime = endTime;
        isWorking = true;
    }

    private void RestartTimer()
    {
        isWorking = false;
        done = false;
        elapsedTime = 0f;
        endTime = 0f;
    }

    public bool isCountingDone()
    {
        return done;
    }

    public float GetTimeLeft()
    {
        return endTime - elapsedTime;
    }
}
