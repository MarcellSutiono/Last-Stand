using UnityEngine;

[CreateAssetMenu(fileName = "KnockerData", menuName = "Scriptable Objects/KnockerData")]
public class KnockerData : ScriptableObject
{
    public float maxHealth = 25f;
    public float health = 25f;
    public int level = 1;
    public float damage = 0;
    public float knockCooldown = 2f;
    public float moveSpeed = 5f;
    public float pushDistance = 3f;
    public float pushDuration = 0.3f;
}
