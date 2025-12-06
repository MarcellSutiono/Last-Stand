using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Stunner : MonoBehaviour
{
    //------------- PLAYER --------------
    [SerializeField] private PlayerData pd;

    //------------- BUTTONS -------------
    [SerializeField] private GameObject interactButtonUI;
    [SerializeField] private GameObject upgradeButtonUI;
    [SerializeField] private Button interactButton;
    [SerializeField] private Button upgradeButton;


    //------------- Stunner -------------
    private bool isBroken = false;
    [SerializeField] private GameObject tungTungSahurParent;
    [SerializeField] private GameObject cappucinoParent;
    [SerializeField] private GameObject airplaneParent;
    [SerializeField] private StunnerData std;
    [SerializeField] private Image flashImage;
    [SerializeField] private float flashSpeed = 0.5f;
    private Animator anim;
    public AudioManager am;
    private float stunTimer = 0f;
    public TextMeshProUGUI levelIndicator;
    private int upgradeCost = 5;
    public GameObject upgradeText;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && (!pd.holdShooter && !pd.holdKnocker))
        {
            interactButtonUI.SetActive(true);

            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(() =>
            {
                am.playSFX(am.pickTowerSFX);
                pd.holdStunner = true;
                this.gameObject.SetActive(false);
            });

            if(pd.resource >= upgradeCost)
            {
                upgradeButtonUI.SetActive(true);
                upgradeButton.onClick.RemoveAllListeners();
                upgradeButton.onClick.AddListener(() =>
                {
                    pd.resource -= upgradeCost;
                    upgradeCost += 5;
                    FindAnyObjectByType<UpgradePanel>().stunnerUpgrade();
                });
            }
            else
            {
                upgradeButtonUI.SetActive(false);
            }
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
        if (pd.resource >= upgradeCost) upgradeText.SetActive(true);
        else upgradeText.SetActive(false);
        stunTimer += Time.deltaTime;
        levelIndicator.text = std.level.ToString();
    }

    public void stun()
    {
        if(stunTimer >= std.stunCooldown && !pd.holdStunner)
        {
            am.playSFX(am.flashSFX);
            flash();
            anim.SetTrigger("Stun");
            stunTimer = 0f;

            float duration = std.stunTime;
            if (std.level == 1)
            {
                duration = 2;
            }
            else if(std.level == 2)
            {
                duration = 3;
            }
            else if (std.level == 3)
            {
                duration = 4;
            }

            for (int i = 0; i < tungTungSahurParent.transform.childCount; i++)
            {
                GameObject tts = tungTungSahurParent.transform.GetChild(i).gameObject;
                TungTungSahur ttsScript = tts.GetComponent<TungTungSahur>();
                if (ttsScript.isActiveAndEnabled)
                {
                    ttsScript.stunTungTungSahur(duration);
                }
            }

            for (int i = 0; i < cappucinoParent.transform.childCount; i++)
            {
                GameObject ca = cappucinoParent.transform.GetChild(i).gameObject;
                BallerinaCappuccina caScript = ca.GetComponent<BallerinaCappuccina>();
                if (caScript.isActiveAndEnabled)
                {
                    caScript.stunCappuccino(duration);
                }
            }

            for (int i = 0; i < airplaneParent.transform.childCount; i++)
            {
                GameObject ap = airplaneParent.transform.GetChild(i).gameObject;
                AirPlane apScript = ap.GetComponent<AirPlane>();
                if (apScript.isActiveAndEnabled)
                {
                    apScript.stunAirplane(duration);
                }
            }
        }
    }

    private void flash()
    {
        StartCoroutine(FlashCoroutine());
    }

    private IEnumerator FlashCoroutine()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / flashSpeed)
        {
            flashImage.color = new Color(1, 1, 1, Mathf.Lerp(0, 1, t));
            yield return null;
        }

        for (float t = 0; t < 1; t += Time.deltaTime / flashSpeed)
        {
            flashImage.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, t));
            yield return null;
        }

        flashImage.color = new Color(1, 1, 1, 0);
    }
}
