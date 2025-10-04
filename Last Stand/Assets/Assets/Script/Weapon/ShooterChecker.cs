using UnityEngine;

public class ShooterChecker : MonoBehaviour
{
    public ShooterData sd;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Ballerina") || col.gameObject.CompareTag("TungTung"))
        {
            sd.enemyInsight = true;
        }
    }

}
