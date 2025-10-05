using UnityEngine;

public class TungTungTungSahurAttack : MonoBehaviour
{
    [SerializeField] private GameObject tts;
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
        TungTungSahur ttsScript = tts.GetComponent<TungTungSahur>();

        if(currWeaponCollided && isAttacking)
        {
            ttsScript.isAttacking = true;
            ttsScript.attack(currWeaponCollided);
        }
        else
        {
            ttsScript.isAttacking = false;
        }
    }
}
