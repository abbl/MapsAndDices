using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

[RequireComponent (typeof(DayCycleView))]
public class DayCycleController : TurnChangeListener {
    public GameObject[] cycles;
    public Light worldLight;
    public float lightChangeSpeed;

    [SyncVar]
    public int cycleLengthIndex;
    [SyncVar (hook = "OnCycleChange")]
    public int cycleIndex;

    [Server]
    public override void NextTurn()
    {
        IncrementCycleIndex();
    }

    [Server]
    private void IncrementCycleIndex()
    {
        if(++cycleLengthIndex == LocalDataManager.GetPlayersGameObjects().Length)
        {
            cycleLengthIndex = 0;

            if (cycleIndex + 1 > cycles.Length - 1)
            {
                cycleIndex = 0;
            }
            else
            {
                ++cycleIndex;
            }
        }
    }

    void OnCycleChange(int newCycleIndex)
    {
        PlayCycleSound(cycles[newCycleIndex]);
        StartCoroutine(LightChange(newCycleIndex));

        if (newCycleIndex == 2)
        {
            SwitchPlayersLight(true);
        }
        if (newCycleIndex == 0)
        {
            DisplayNewDayPopOut();
            SwitchPlayersLight(false);
        }
    }

    private void PlayCycleSound(GameObject cycle)
    {
        AudioClip cycleBeginClip = cycle.GetComponent<DayCycle>().cycleBeginSound;

        if (cycleBeginClip != null)
        {
            GetComponent<AudioSource>().clip = cycleBeginClip;
            GetComponent<AudioSource>().Play();
        }
    }

    private void DisplayNewDayPopOut()
    {
        GetComponent<DayCycleView>().DisplayNewDayPopOut();
    }

    private void SwitchPlayersLight(bool enabled)
    {
        foreach (GameObject checker in LocalDataManager.GetPlayersCheckers())
        {
            checker.GetComponentInChildren<Light>().enabled = enabled;
        }
    }
    
    private IEnumerator LightChange(int newCycleIndex)
    {
        float cycleLightIntensity = cycles[newCycleIndex].GetComponent<DayCycle>().lightIntensity;
        bool incrementOrDecrement = false;

        if (worldLight.intensity < cycleLightIntensity) // Checking if light value should be incremented.
            incrementOrDecrement = true;

        while (true)
        {
            if (incrementOrDecrement) // true for increment
            {
                worldLight.intensity += lightChangeSpeed * Time.deltaTime;

                if (worldLight.intensity >= cycleLightIntensity)
                    yield break;
            }
            else // false for decrement
            {
                worldLight.intensity -= lightChangeSpeed * Time.deltaTime;

                if (worldLight.intensity <= cycleLightIntensity)
                    yield break;
            }
            yield return null;
        }
    }
}