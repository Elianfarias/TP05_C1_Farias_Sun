using UnityEngine;

public class FinishGate : MonoBehaviour
{
    [Header("PlayerWinHUD")]
    [SerializeField] private GameObject panelPlayerWin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            panelPlayerWin.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
