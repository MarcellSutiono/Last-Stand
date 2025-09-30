using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaneBuilding : MonoBehaviour
{
    //------------- WEAPONS -------------
    [SerializeField] private GameObject shooter;
    [SerializeField] private GameObject stunner;
    [SerializeField] private GameObject knocker;

    //------------- PLAYER --------------
    [SerializeField] private PlayerData pd;
    [SerializeField] private GameObject player;

    //------------- BUTTONS -------------
    [SerializeField] private GameObject interactButtonUI;
    [SerializeField] private Button interactButton;
    [SerializeField] private TextMeshProUGUI interactText;

    //------------- OTHERS -------------
    private BoxCollider2D laneCollider;
    private float centerY;

    private void Start()
    {
        laneCollider = gameObject.GetComponent<BoxCollider2D>();
        centerY = laneCollider.bounds.center.y;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && pd.holdShooter)
        {
            interactText.text = "Place Shooter";
            interactButtonUI.SetActive(true);

            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(() => {
                pd.holdShooter = false;

                float playerX = player.transform.position.x;
                shooter.transform.position = new Vector3(playerX + 1f, centerY, 0f);

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

                float playerX = player.transform.position.x;
                stunner.transform.position = new Vector3(playerX + 1f, centerY, 0f);

                stunner.gameObject.SetActive(true);
                
                Stunner st = stunner.GetComponent<Stunner>();
                st.stun();
            });
        }
        else if (col.CompareTag("Player") && pd.holdKnocker)
        {
            interactText.text = "Place Knocker";
            interactButtonUI.SetActive(true);

            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(() => {
                pd.holdKnocker = false;

                float playerX = player.transform.position.x;
                knocker.transform.position = new Vector3(playerX + 1f, centerY, 0f);

                knocker.gameObject.SetActive(true);

                Knocker kn = knocker.GetComponent<Knocker>();
                kn.knock();
            });
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        interactButtonUI.SetActive(false);
    }
}
