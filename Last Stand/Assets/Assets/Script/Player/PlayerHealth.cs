using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerData pd;
    [SerializeField] private Image healthbar;
    [SerializeField] private Image border;
    public Vector3 Offset;
    public void TakeDamage(int damage)
    {
        pd.health -= damage;
        healthbar.fillAmount = pd.health / 100;
    }

    private void Update()
    {
        if (pd.health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        healthbar.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position + Offset);
    }
}
