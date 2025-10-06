using UnityEngine;

public enum GameState
{
    MAIN_MENU,
    PLAYING,
    PAUSED,
    GAME_OVER
}

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private AudioClip clipGameOver;
    public static GameStateManager Instance { get; private set; }
    public GameState CurrentGameState { get; private set; } = GameState.PLAYING;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        SetGameState(CurrentGameState);
    }

    public void SetGameState(GameState newState)
    {
        CurrentGameState = newState;

        switch (newState)
        {
            case GameState.PLAYING:
                AudioController.Instance.PlayBackgroundMusic();
                break;
            case GameState.PAUSED:
                break;
            case GameState.GAME_OVER:
                var recordMeters = PlayerPrefs.GetInt("PlayerScore");
                var score = ScoreManager.Instance.metersTraveled;

                if (score > recordMeters)
                    PlayerPrefs.SetInt("PlayerScore", score);

                AudioController.Instance.StopBackgroundMusic();
                AudioController.Instance.PlaySoundEffect(clipGameOver);
                HUDManager.Instance.ShowPanelPlayerLose();
                Time.timeScale = 0;
                break; 

        }
    }
}
