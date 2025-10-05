using UnityEngine;

[CreateAssetMenu(fileName = "ShooterData", menuName = "Scriptable Objects/ShooterData")]
public class ShooterData : ScriptableObject
{
    public float health = 10f;
    public float shootDelay = 2.4f;
    public int level = 1;
    public bool enemyInsight = false;
}
