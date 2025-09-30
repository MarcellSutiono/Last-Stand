using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stunner : MonoBehaviour
{
    //------------- PLAYER --------------
    [SerializeField] private PlayerData pd;

    //------------- BUTTONS -------------
    [SerializeField] private GameObject interactButtonUI;
    [SerializeField] private Button interactButton;
    [SerializeField] private TextMeshProUGUI interactText;

    //------------- Stunner -------------
    [SerializeField] private GameObject tungTungSahurParent;
    [SerializeField] private GameObject cappucinoParent;
    [SerializeField] private StunnerData std;
    private float stunTimer = 0f;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && (!pd.holdShooter && !pd.holdKnocker))
        {
            interactText.text = "Took Stunner";
            interactButtonUI.SetActive(true);

            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(() =>
            {
                pd.holdStunner = true;
                this.gameObject.SetActive(false);
            });
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        interactButtonUI.SetActive(false);
    }

    private void Update()
    {
        stunTimer += Time.deltaTime;
    }

    public void stun()
    {
        if(stunTimer >= std.stunCooldown && !pd.holdStunner)
        {
            stunTimer = 0f;

            for (int i = 0; i < tungTungSahurParent.transform.childCount; i++)
            {
                GameObject tts = tungTungSahurParent.transform.GetChild(i).gameObject;
                TungTungSahur ttsScript = tts.GetComponent<TungTungSahur>();
                if (ttsScript.isActiveAndEnabled)
                {
                    ttsScript.stunTungTungSahur(2f);
                }
            }

            for (int i = 0; i < cappucinoParent.transform.childCount; i++)
            {
                GameObject ca = cappucinoParent.transform.GetChild(i).gameObject;
                CappuccinoAssasino caScript = ca.GetComponent<CappuccinoAssasino>();
                if (caScript.isActiveAndEnabled)
                {
                    caScript.stunCappuccino(2f);
                }
            }
        }
    }

}
