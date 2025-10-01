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

    //------------- SHADOWS -------------
    [SerializeField] private GameObject shooterShadow;
    [SerializeField] private GameObject stunnerShadow;
    [SerializeField] private GameObject knockerShadow;
    private GameObject currentShadow;

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

            currentShadow = shooterShadow;
            showShadow(currentShadow);

            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(() => {
                currentShadow = null;
                pd.holdShooter = false;
                placeObject(shooter);
            });
        }
        else if (col.CompareTag("Player") && pd.holdStunner)
        {
            interactText.text = "Place Stunner";
            interactButtonUI.SetActive(true);

            currentShadow = stunnerShadow;
            showShadow(currentShadow);

            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(() => {
                currentShadow = null;
                pd.holdStunner = false;
                placeObject(stunner);
                Stunner st = stunner.GetComponent<Stunner>();
                st.stun();
            });
        }
        else if (col.CompareTag("Player") && pd.holdKnocker)
        {
            interactText.text = "Place Knocker";
            interactButtonUI.SetActive(true);

            currentShadow = knockerShadow;
            showShadow(currentShadow);

            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(() => {
                currentShadow = null;
                pd.holdKnocker = false;
                placeObject(knocker);
                Knocker kn = knocker.GetComponent<Knocker>();
                kn.knock();
            });
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        interactButtonUI.SetActive(false);
    }

    private void showShadow(GameObject shadow)
    {
        if (currentShadow != shadow)
        {
            currentShadow = shadow;
            currentShadow.SetActive(true);
        }

        float playerX = player.transform.position.x;
        shadow.transform.position = new Vector3(playerX + 1f, centerY, 0f);
    }

    private void placeObject(GameObject obj)
    {
        float playerX = player.transform.position.x;
        obj.transform.position = new Vector3(playerX + 1f, centerY, 0f);
        obj.SetActive(true);
    }
}
