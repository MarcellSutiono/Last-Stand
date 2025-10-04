using UnityEngine;

[CreateAssetMenu(fileName = "BallerinaCappucinaData", menuName = "Scriptable Objects/BallerinaCappucinaData")]
public class BallerinaCappucinaData : ScriptableObject
{
    // BASIC STATS
    public float speed = 7f;
    public float attackCooldown = 3f;

    // STUN
    public float stunDuration;
}
