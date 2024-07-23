using System.Collections;
using TMPro;
using UnityEngine;
public class WaveManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI TimeText;
    [SerializeField] TextMeshProUGUI WaveText;

    public static WaveManager Instance;

    bool WaveRunning = true;
    int CurrentWave = 0;
    int currentWaveTime;

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }
    private void Start()
    {
        StartNewWave();
        TimeText.text = "30";
        WaveText.text = "Wave: 1"; 
    }

    public bool waveRunning() => WaveRunning;
    public void StartNewWave()
    {
        StopAllCoroutines();
        TimeText.color = Color.white;
        CurrentWave++;
        WaveRunning = true;
        currentWaveTime = 30;
        WaveText.text = "Wave: " + CurrentWave;
        StartCoroutine(WaveTimer());
        GameManager.Instance.WavePanaloff();
    }
    IEnumerator WaveTimer()
    {
        while(WaveRunning)
        {
            yield return new WaitForSeconds(1f);
            currentWaveTime--;

            TimeText.text = currentWaveTime.ToString();

            if(currentWaveTime <= 0)
                WaveComplete();
        }
        yield return null;
    }
    void WaveComplete()
    {
        StopAllCoroutines();
        EnemyManager.Instance.DestroyAllEnemies();
        WaveRunning = false;
        currentWaveTime = 30;
        TimeText.text = currentWaveTime.ToString();
        TimeText.color = Color.red;
        GameManager.Instance.WavePanal();
    }
}