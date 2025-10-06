using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public static HUDManager Instance { get; private set; }

    [Header("PlayersHUD")]
    [SerializeField] private PlayerDataSO playerSettings;
    [SerializeField] private TMP_Text textPlayerName;
    [Header("PlayerLoseHUD")]
    public GameObject panelPlayerLose;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text recordText;
    [SerializeField] private Button btnReset;
    [SerializeField] private Button btnBackToMenu;

    private void Awake()
    {
        Instance = this;
        btnBackToMenu.onClick.AddListener(BackToMenu);
        btnReset.onClick.AddListener(ResetGame);
    }

    private void Update()
    {
        textPlayerName.text = playerSettings.playerName;
    }

    private void OnDestroy()
    {
        btnBackToMenu.onClick.RemoveAllListeners();
        btnReset.onClick.RemoveAllListeners();
    }

    public void ShowPanelPlayerLose()
    {
        scoreText.text = ScoreManager.Instance.metersTraveled.ToString() + " m";
        recordText.text = PlayerPrefs.GetInt("PlayerScore").ToString() + " m";
        panelPlayerLose.SetActive(true);
    }

    private void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }

    private void ResetGame()
    {
        Time.timeScale = 1;
        GameStateManager.Instance.SetGameState(GameState.PLAYING);
        SceneManager.LoadScene("InGame");
    }
}
