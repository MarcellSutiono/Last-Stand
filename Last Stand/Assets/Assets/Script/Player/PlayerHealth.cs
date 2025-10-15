using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerData pd;
    [SerializeField] private Image healthbar;
    [SerializeField] private Image border;
    public Vector3 Offset;
    public float Timer;
    public float healthTimer;

    private void Start()
    {
        Timer = 0f;
        healthTimer = 1f;
    }
    public void TakeDamage(int damage)
    {
        Timer = 3f;
        pd.health -= damage;
        healthbar.fillAmount = pd.health / 100;
    }

    private void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
            return;
        }
        healthTimer += Time.deltaTime;

        if (healthTimer >= 1f)
        {
            healthTimer = 0f;
            if(pd.health < pd.maxHealth)
            {
                pd.health += 2.5f;
                if (pd.health > pd.maxHealth) pd.health = pd.maxHealth;
                healthbar.fillAmount = pd.health / 100f;
            }
        }

        Debug.Log(Timer);
        if (pd.health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        healthbar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
