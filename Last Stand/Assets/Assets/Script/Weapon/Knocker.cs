using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Knocker : MonoBehaviour
{
    //------------- PLAYER --------------
    [SerializeField] private PlayerData pd;

    //------------- BUTTONS -------------
    [SerializeField] private GameObject interactButtonUI;
    [SerializeField] private GameObject upgradeButtonUI;

    [SerializeField] private Button interactButton;
    [SerializeField] private Button upgradeButton;

    //------------- KNOCKER -------------
    private bool isBroken = false;
    [SerializeField] private float knockCooldown = 2f;
    [SerializeField] private GameObject tungTungSahurParent;
    [SerializeField] private GameObject cappuccinoParent;
    [SerializeField] private GameObject airplaneParent;
    [SerializeField] private KnockerData kd;
    [SerializeField] private GameObject wave;
    public AudioManager am;
    private Animator anim;
    private float knockTimer = 0f;
    [SerializeField] private int upgradeCost = 5;
    public GameObject upgradeText;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && (!pd.holdShooter && !pd.holdStunner))
        {
            interactButtonUI.SetActive(true);

            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(() =>
            {
                am.playSFX(am.pickTowerSFX);
                pd.holdKnocker = true;
                this.gameObject.SetActive(false);
            });
        }

        if(pd.resource >= upgradeCost)
            {
                upgradeButtonUI.SetActive(true);
                upgradeButton.onClick.RemoveAllListeners();
                upgradeButton.onClick.AddListener(() =>
                {
                    pd.resource -= upgradeCost;
                    upgradeCost += 5;
                    FindAnyObjectByType<UpgradePanel>().knockerUpgrade();
                });
            }
            else
            {
                upgradeButtonUI.SetActive(false);
            }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        interactButtonUI.SetActive(false);
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
        knockCooldown = kd.knockCooldown;
    }

    private void Update()
    {
        if (pd.resource >= upgradeCost) upgradeText.SetActive(true);
        else upgradeText.SetActive(false);
        knockTimer += Time.deltaTime;
    }

    public void knock()
    {
        if (knockTimer >= knockCooldown && !pd.holdStunner)
        {
            anim.SetTrigger("Knock");
            knockTimer = 0f;

            StartCoroutine(shootWave());

            /*for (int i = 0; i < tungTungSahurParent.transform.childCount; i++)
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
                BallerinaCappuccina caScript = tts.GetComponent<BallerinaCappuccina>();
                if (caScript.isActiveAndEnabled)
                {
                    caScript.knockCappuccino(5f);
                }
            }

            for (int i = 0; i < airplaneParent.transform.childCount; i++)
            {
                GameObject ap = airplaneParent.transform.GetChild(i).gameObject;
                AirPlane caSapScriptcript = ap.GetComponent<AirPlane>();
                if (caSapScriptcript.isActiveAndEnabled)
                {
                    caSapScriptcript.knockAirplane(5f);
                }
            }*/
        }
    }

    IEnumerator shootWave()
    {
        for (int i = 0; i < 5; i++)
        {
            GameObject waveGameObject = Instantiate(wave, transform.position + new Vector3(1f, 1.25f, 0), Quaternion.identity);
            StartCoroutine(ExpandWave(waveGameObject));
            
            yield return new WaitForSeconds(0.3f);
        }
    }

    IEnumerator ExpandWave(GameObject waveObj)
    {
        float duration = 0.8f;
        float elapsed = 0f;
        Vector3 startScale = Vector3.one * 0.5f;
        Vector3 endScale = Vector3.one * 5f;
        
        while (elapsed < duration)
        {
            waveObj.transform.localScale = Vector3.Lerp(startScale, endScale, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        
        Destroy(waveObj);
    }
}
