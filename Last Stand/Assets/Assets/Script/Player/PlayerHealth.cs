using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerData pd;
    public void TakeDamage(int damage)
    {
        pd.health -= damage;
    }

    private void Update()
    {
        if (pd.health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
