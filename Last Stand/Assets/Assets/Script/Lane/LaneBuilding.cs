using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaneBuilding : MonoBehaviour
{
    //------------- WEAPONS -------------
    [SerializeField] private GameObject shooter;
    [SerializeField] private GameObject stunner;

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
            interactText.text = "Place Shooter";
            interactButtonUI.SetActive(true);

            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(() => {
                pd.holdShooter = false;
                shooter.transform.position = player.transform.position + new Vector3(1f, 0f, 0f);
                shooter.gameObject.SetActive(true);
            });
        }
        else if (col.CompareTag("Player") && pd.holdStunner)
        {
            interactText.text = "Place Stunner";
            interactButtonUI.SetActive(true);

            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(() => {
                pd.holdStunner = false;
                stunner.transform.position = player.transform.position + new Vector3(1f, 0f, 0f);
                stunner.gameObject.SetActive(true);
                
                Stunner st = stunner.GetComponent<Stunner>();
                st.stun();
            });
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        interactButtonUI.SetActive(false);
    }
}
