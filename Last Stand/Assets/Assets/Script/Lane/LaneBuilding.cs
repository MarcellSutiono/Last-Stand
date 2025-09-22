using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaneBuilding : MonoBehaviour
{
    //------------- WEAPONS -------------
    [SerializeField] private GameObject shooter;

    //------------- PLAYER --------------
    [SerializeField] private PlayerData pd;
    [SerializeField] private GameObject player;

    //------------- BUTTONS -------------
    [SerializeField] private GameObject interactButtonUI;
    [SerializeField] private Button interactButton;
    [SerializeField] private TextMeshProUGUI interactText;
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && pd.holdShooter)
        {
            interactText.text = "Place";
            interactButtonUI.SetActive(true);
            interactButton.onClick.AddListener(() => {
                pd.holdShooter = false;
                shooter.transform.position = player.transform.position + new Vector3(1f, 0f, 0f);
                shooter.gameObject.SetActive(true);
            });
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        interactButtonUI.SetActive(false);
    }
}
