using UnityEngine;

[CreateAssetMenu(fileName = "ShooterData", menuName = "Scriptable Objects/ShooterData")]
public class ShooterData : ScriptableObject
{
    public float maxHealth = 50f;
    public float health = 50f;
    public float shootDelay = 2.4f;
    public int level = 1;
    public bool enemyInsight = false;
    public float damage = 1f;
    public bool hasDoubleProjectile = false;
    public bool hasSplashLane = false;
}
