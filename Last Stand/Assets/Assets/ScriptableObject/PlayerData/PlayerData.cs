using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "Scriptable Objects/PlayerData")]
public class PlayerData : ScriptableObject
{
    public float playerSpeed = 15f;
    public bool holdShooter = false;
    public bool holdStunner = false;
    public bool holdKnocker = false;

    public int exp = 0;
    public int level = 1;
    public int expNeeded = 10;
    public int resource = 0;
    public float health = 100;
    public int maxHealth = 100;
    public float attackCooldown = 0.6f; // level 2 : 0.3
    public int damageTaken = 10; // level 3 : 5 
}
