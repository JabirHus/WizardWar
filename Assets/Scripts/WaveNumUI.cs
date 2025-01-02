using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class WaveNumUI : MonoBehaviour
{
    public WaveManager SpawnWaveManager;
    public TextMeshProUGUI WaveNumText;

    void Start()
    {
        if (SpawnWaveManager != null)
        {
            SpawnWaveManager.OnWaveNumChanged += UpdateWaveText; // link to event
            UpdateWaveText(); // Initial update of the wave number display
        }
        else
        {
            Debug.LogError("SpawnWaveManger is not assigned in WaveNumUI!");
        }
    }

  
    void OnDestroy()
    {
        if (SpawnWaveManager != null)
        {
           SpawnWaveManager.OnWaveNumChanged -= UpdateWaveText; // Remove link
        }
    }


    void UpdateWaveText()
    {
        if (WaveNumText != null)
        {
            WaveNumText.text = "WAVE " + SpawnWaveManager.GetCurrentWave()  + " / " + SpawnWaveManager.GetMaxWave();
        }
        else
        {
            Debug.LogError("WaveNumText is not assigned in WaveNumUI!");
        }
    }
}

