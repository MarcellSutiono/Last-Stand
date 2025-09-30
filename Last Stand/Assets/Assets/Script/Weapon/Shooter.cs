using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    //------------- PLAYER --------------
    [SerializeField] private PlayerData pd;

    //------------- BUTTONS -------------
    [SerializeField] private GameObject interactButtonUI;
    [SerializeField] private Button interactButton;
    [SerializeField] private TextMeshProUGUI interactText;

    //------------- SHOOTER -------------
    [SerializeField] private ShooterData sd;
    [SerializeField] private GameObject bullet;
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
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        interactButtonUI.SetActive(false);
    }

    private void Update()
    {
        
        shooting();
    }

    private void shooting()
    {
        if (!pd.holdShooter)
        {
            shootTimer += Time.deltaTime;
            if (shootTimer >= sd.shootDelay)
            {
                shootTimer = 0;
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
    }
}
