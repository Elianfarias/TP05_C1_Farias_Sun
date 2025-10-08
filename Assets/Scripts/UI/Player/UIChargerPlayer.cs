using UnityEngine;
using UnityEngine.UI;

public class UIChargerPlayer : MonoBehaviour
{
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private GameObject[] fireballsCharger;

    private void Awake()
    {
        playerAttack.onChargerUpdate += OnChargerUpdate;
    }

    private void OnDestroy()
    {
        playerAttack.onChargerUpdate -= OnChargerUpdate;
    }

    public void OnChargerUpdate(int current)
    {
        for (int i = fireballsCharger.Length; i > current; i--)
        {
            fireballsCharger[i].SetActive(false);
        }
    }
}