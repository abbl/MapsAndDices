using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkedTimer : NetworkBehaviour {
    private float elapsedTime;
    private float endTime;
    private bool isWorking;
    private bool isPaused;
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
            if (!isPaused)
            {
                elapsedTime += Time.deltaTime;
                if (elapsedTime >= endTime)
                {
                    done = true;
                    isWorking = false;
                }
            }
        }
    }

    public void StartTimer(float endTime)
    {
        RestartTimer();
        this.endTime = endTime;
        isWorking = true;
    }

    public void PauseTimer()
    {
        isPaused = true;
    }

    public void ResumeTimer()
    {
        isPaused = false;
    }

    private void RestartTimer()
    {
        isWorking = false;
        isPaused = false;
        done = false;
        elapsedTime = 0f;
        endTime = 0f;
    }

    public bool IsCountingDone()
    {
        return done;
    }

    public int GetTimeLeft()
    {
        return (int)(endTime - elapsedTime) + 1;
    }
}
