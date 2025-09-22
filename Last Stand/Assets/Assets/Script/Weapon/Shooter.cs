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
    [SerializeField] private GameObject bullet;
    [SerializeField] private float shootDelay = 1.4f;
    private float shootTimer = 0;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            interactText.text = "Took";
            interactButtonUI.SetActive(true);
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
            if (shootTimer >= shootDelay)
            {
                shootTimer = 0;
                Instantiate(bullet, transform.position, Quaternion.identity);
            }
        }
    }
}
