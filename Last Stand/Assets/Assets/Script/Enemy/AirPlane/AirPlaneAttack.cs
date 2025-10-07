using UnityEngine;

public class AirPlaneAttack : MonoBehaviour
{
    [SerializeField] private GameObject airPlane;
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
        AirPlane airScript = airPlane.GetComponent<AirPlane>();

        if (currWeaponCollided && isAttacking)
        {
            airScript.isAttacking = true;
            airScript.attack(currWeaponCollided);
        }
        else
        {
            airScript.isAttacking = false;
        }
    }
}
