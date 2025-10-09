using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;

    public static ScoreManager Instance;

    private void Awake()
    {
        if(Instance == null)
            Instance = this;
    }
}
