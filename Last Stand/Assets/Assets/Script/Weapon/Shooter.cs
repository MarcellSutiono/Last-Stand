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

    [SerializeField] private TextMeshProUGUI interactText;

    //------------- SHOOTER -------------
    [SerializeField] private ShooterData sd;
    [SerializeField] private GameObject bullet;
    [SerializeField] private TextMeshProUGUI levelIndicator;
    private float shootTimer = 0;

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.CompareTag("Player") && (!pd.holdStunner && !pd.holdKnocker))
        {
            interactText.text = "Took Shooter";

            interactButtonUI.SetActive(true);

            interactButton.onClick.RemoveAllListeners();
            interactButton.onClick.AddListener(() =>
            {
                pd.holdShooter = true;
                this.gameObject.SetActive(false);
            });

            if(pd.resource > 0)
            {
                upgradeButtonUI.SetActive(true);
                upgradeButton.onClick.RemoveAllListeners();
                upgradeButton.onClick.AddListener(() =>
                {
                    sd.level++;
                    pd.resource--;
                });
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
        shooting();

        levelIndicator.text = sd.level.ToString();
    }

    private void shooting()
    {
        if (!pd.holdShooter)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= sd.shootDelay)
            {
                shootTimer = 0;

                GameObject bulletGameObject = Instantiate(bullet, transform.position, Quaternion.identity);

                if (sd.level == 1)
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
            }
        }
    }
}
