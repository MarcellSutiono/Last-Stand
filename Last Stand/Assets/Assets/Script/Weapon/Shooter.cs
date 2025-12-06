using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    //------------- PLAYER --------------
    [SerializeField] private PlayerData pd;

    //------------- BUTTONS -------------
    [SerializeField] private GameObject interactButtonUI;
    [SerializeField] private GameObject upgradeButtonUI;

    [SerializeField] private Button interactButton;
    [SerializeField] private Button upgradeButton;

    //------------- SHOOTER -------------
    [SerializeField] private ShooterData sd;
    [SerializeField] private GameObject bullet;
    [SerializeField] private TextMeshProUGUI levelIndicator;
    [SerializeField] private int upgradeCost = 5;
    private float shootTimer = 0;

    //------------- SHOOTER -------------
    public AudioManager am;
    public GameObject upgradeText;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && (!pd.holdStunner && !pd.holdKnocker))
        {
            interactButtonUI.SetActive(true);

            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(() =>
            {
                am.playSFX(am.pickTowerSFX);
                pd.holdShooter = true;
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
                    FindAnyObjectByType<UpgradePanel>().shooterUpgrade();
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
        upgradeButtonUI.SetActive(false);
    }

    private void Update()
    {
        if (pd.resource >= upgradeCost) upgradeText.SetActive(true);
        else upgradeText.SetActive(false);
        shooting();
        levelIndicator.text = sd.level.ToString();
    }

    private void shooting()
    {
        if (!pd.holdShooter && sd.enemyInsight)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= sd.shootDelay)
            {
                shootTimer = 0;

                am.playSFX(am.zapSFX);

                if (!sd.hasDoubleProjectile && !sd.hasSplashLane){
                    GameObject bulletGameObject = Instantiate(bullet, transform.position + new Vector3(1f, 1.25f, 0), Quaternion.identity);
                }else if (sd.hasDoubleProjectile && !sd.hasSplashLane){
                    StartCoroutine(ShootDoubleProjectile());
                }else if (!sd.hasDoubleProjectile && sd.hasSplashLane){
                    int bulletCount = 3;
                    float spreadAngle = 15f; // degrees between each bullet

                    for (int i = 0; i < bulletCount; i++)
                    {
                        float angleOffset = (i - (bulletCount - 1) / 2f) * spreadAngle;
    
                        Quaternion rotation = Quaternion.Euler(0, 0, angleOffset);
    
                        GameObject bulletGameObject = Instantiate(bullet, transform.position + new Vector3(1f, 1.25f, 0), rotation);
                    }
                }else if (sd.hasDoubleProjectile && sd.hasSplashLane){
                    StartCoroutine(ShootDoubleSplashLane());
                }
                /*if (sd.level == 1)
                {
                    bullet.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                }
                else if (sd.level >= 2)
                {
                    Color customColor;
                    if (ColorUtility.TryParseHtmlString("#FF3939", out customColor))
                    {
                        bullet.gameObject.GetComponent<SpriteRenderer>().color = customColor;
                    }
                }
                */
            }
        }
    }

    IEnumerator ShootDoubleProjectile()
    {
        GameObject bulletGameObject1 = Instantiate(bullet, transform.position + new Vector3(1f, 1.5f, 0), Quaternion.identity);
        yield return new WaitForSeconds(0.3f);
        GameObject bulletGameObject2 = Instantiate(bullet, transform.position + new Vector3(1f, 1.5f, 0), Quaternion.identity);
    }

    IEnumerator ShootDoubleSplashLane()
    {
        int bulletCount = 3;
        float spreadAngle = 15f; // degrees between each bullet

        for (int i = 0; i < bulletCount; i++)
        {
            float angleOffset = (i - (bulletCount - 1) / 2f) * spreadAngle;

            Quaternion rotation = Quaternion.Euler(0, 0, angleOffset);

            GameObject bulletGameObject1 = Instantiate(bullet, transform.position + new Vector3(1f, 1.5f, 0), rotation);
        }

        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i < bulletCount; i++)
        {
            float angleOffset = (i - (bulletCount - 1) / 2f) * spreadAngle;

            Quaternion rotation = Quaternion.Euler(0, 0, angleOffset);

            GameObject bulletGameObject2 = Instantiate(bullet, transform.position + new Vector3(1f, 1.5f, 0), rotation);
        }
    }
}
