using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private int metersPerSpeedIncrease = 50;

    public static ScoreManager Instance;
    public int metersTraveled = 0;

    private int nextSpeedMilestone = 50;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }

    private void Start()
    {
        StartCoroutine(nameof(IncreaseMetersTraveled));
    }

    private void Update()
    {
        scoreText.text = metersTraveled.ToString() + " m";
    }

    private IEnumerator IncreaseMetersTraveled()
    {
        while(GameStateManager.Instance.CurrentGameState == GameState.PLAYING)
        {
            if (metersTraveled >= nextSpeedMilestone)
            {
                OnSpeedIncrease();
                nextSpeedMilestone += metersPerSpeedIncrease;
            }

            metersTraveled++;

            yield return new WaitForSeconds(0.5f);
        }
        
    }

    public void IncreaseMeters(int meters)
    {
        metersTraveled += meters;
    }

    private void OnSpeedIncrease()
    {
        //EnemySpawner.Instance.IncreaseSpeed();
        //BiomeParallaxOrchestrator.Instance.worldSpeed += 2f;
    }
}
