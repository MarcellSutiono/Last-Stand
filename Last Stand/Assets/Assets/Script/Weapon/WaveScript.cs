using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class WaveScript : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float pushDistance = 5f;
    public float pushDuration = 0.3f;
    public LayerMask enemyLayer; // Set this in inspector
    public KnockerData kd;
    
    private HashSet<GameObject> hitEnemies = new HashSet<GameObject>();

    void Start()
    {
        moveSpeed = kd.moveSpeed;
        pushDistance = kd.pushDistance;
        pushDuration = kd.pushDuration;
    }

    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if it's an enemy layer
        if (((1 << collision.gameObject.layer) & enemyLayer) != 0)
        {
            if (!hitEnemies.Contains(collision.gameObject))
            {
                hitEnemies.Add(collision.gameObject);
                StartCoroutine(PushEnemy(collision.gameObject));
            }
        }
        if (collision.gameObject.CompareTag("TungTung"))
        {
            collision.gameObject.GetComponent<TungTungSahur>().health -= kd.damage;
        }
        else if(collision.gameObject.CompareTag("Ballerina"))
        {
            collision.gameObject.GetComponent<BallerinaCappuccina>().health -= kd.damage;
        }
        else if (collision.gameObject.CompareTag("Airplane"))
        {
            if(collision.gameObject.GetComponent<AirPlane>().isStunned)
            {
                collision.gameObject.GetComponent<AirPlane>().health -= kd.damage;
            }
        }
    }
    
    IEnumerator PushEnemy(GameObject enemy)
    {
        Vector3 startPos = enemy.transform.position;
        Vector3 endPos = startPos + Vector3.right * pushDistance;
        float elapsed = 0f;
        
        while (elapsed < pushDuration)
        {
            if (enemy != null)
            {
                enemy.transform.position = Vector3.Lerp(startPos, endPos, elapsed / pushDuration);
            }
            elapsed += Time.deltaTime;
            yield return null;
        }
    }
}