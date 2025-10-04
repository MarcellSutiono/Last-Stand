using UnityEngine;

public class BallerinaCappucinaAttack : MonoBehaviour
{
    [SerializeField] private GameObject cappuccino;
    private GameObject currWeaponCollided;
    private bool isAttacking = false;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Shooter") || col.CompareTag("Knocker") || col.CompareTag("Stunner"))
        {
            isAttacking = true;
            currWeaponCollided = col.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.CompareTag("Shooter") || col.CompareTag("Knocker") || col.CompareTag("Stunner"))
        {
            isAttacking = false;
            currWeaponCollided = null;
        }
    }

    private void Update()
    {
        attack();
    }

    private void attack()
    {
        BallerinaCappuccina caScript = cappuccino.GetComponent<BallerinaCappuccina>();

        if (currWeaponCollided && isAttacking)
        {
            caScript.isAttacking = true;
            caScript.attack(currWeaponCollided);
        }
        else
        {
            caScript.isAttacking = false;
        }
    }
}
