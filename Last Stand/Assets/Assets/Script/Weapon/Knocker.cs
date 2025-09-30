using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Knocker : MonoBehaviour
{
    //------------- PLAYER --------------
    [SerializeField] private PlayerData pd;

    //------------- BUTTONS -------------
    [SerializeField] private GameObject interactButtonUI;
    [SerializeField] private Button interactButton;
    [SerializeField] private TextMeshProUGUI interactText;

    //------------- KNOCKER -------------
    [SerializeField] private float knockCooldown = 2f;
    [SerializeField] private GameObject tungTungSahurParent;
    [SerializeField] private GameObject cappuccinoParent;
    private Animator anim;
    private float knockTimer = 0f;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && (!pd.holdShooter && !pd.holdStunner))
        {
            interactText.text = "Took Knocker";
            interactButtonUI.SetActive(true);

            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(() =>
            {
                pd.holdKnocker = true;
                this.gameObject.SetActive(false);
            });
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        interactButtonUI.SetActive(false);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        knockTimer += Time.deltaTime;
    }

    public void knock()
    {
        if (knockTimer >= knockCooldown && !pd.holdStunner)
        {
            anim.SetTrigger("Knock");
            knockTimer = 0f;

            for (int i = 0; i < tungTungSahurParent.transform.childCount; i++)
            {
                GameObject tts = tungTungSahurParent.transform.GetChild(i).gameObject;
                TungTungSahur ttsScript = tts.GetComponent<TungTungSahur>();
                if (ttsScript.isActiveAndEnabled)
                {
                    ttsScript.knockTungTungSahur(5f);
                }
            }

            for (int i = 0; i < cappuccinoParent.transform.childCount; i++)
            {
                GameObject tts = cappuccinoParent.transform.GetChild(i).gameObject;
                CappuccinoAssasino caScript = tts.GetComponent<CappuccinoAssasino>();
                if (caScript.isActiveAndEnabled)
                {
                    caScript.knockCappuccino(5f);
                }
            }
        }
    }
}
