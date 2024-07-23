using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject GameOverPanal;
    [SerializeField] Button RestartButton;

    [SerializeField] GameObject Wavepanal;
    [SerializeField] Button Continue;

    [SerializeField] Button Gun;

    private bool gameRunning;

   public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        RestartButton.onClick.AddListener(RestartGame);
        Continue.onClick.AddListener(WaveContinue);
        Gun.onClick.AddListener(AddGun);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }
    public bool IsGameRunning()
    {
        return gameRunning;
    }

    public void GameOver()
    {
        gameRunning = false;
        GameOverPanal.SetActive(true);
    }

    void WaveContinue()
    {
        WaveManager.Instance.StartNewWave();
    }

    public void WavePanal()
    {
        Wavepanal.SetActive(true);
    }
    public void WavePanaloff()
    {
        Wavepanal.SetActive(false);
    }
    public void AddGun()
    {
        GunManager.Instance.AddGun();

    }
}
