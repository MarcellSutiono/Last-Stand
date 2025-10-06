using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private PlayerData pd;
    public void TakeDamage(int damage)
    {
        pd.health -= damage;

        if (pd.health <= 0)
        {
        }
    }
}
