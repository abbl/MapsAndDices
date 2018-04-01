using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NetworkedTimer : NetworkBehaviour {
    [SyncVar]
    public float elapsedTime;
    [SyncVar]
    public float endTime;
    public bool isWorking;
    public bool isPaused;
    public bool done;

	void Update () {
        if (!isServer)
            return;

        HandleTimer();
	}

    [Server]
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

    [Server]
    public void StartTimer(float endTime)
    {
        RestartTimer();
        this.endTime = endTime;
        isWorking = true;
    }

    [Server]
    public void PauseTimer()
    {
        isPaused = true;
    }

    [Server]
    public void ResumeTimer()
    {
        isPaused = false;
    }

    [Server]
    private void RestartTimer()
    {
        isWorking = false;
        isPaused = false;
        done = false;
        elapsedTime = 0f;
        endTime = 0f;
    }

    [Server]
    public bool IsCountingDone()
    {
        return done;
    }

    public int GetTimeLeft()
    {
        return (int)(endTime - elapsedTime);
    }
}
