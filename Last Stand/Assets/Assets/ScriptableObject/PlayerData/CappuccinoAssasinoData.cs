using UnityEngine;

[CreateAssetMenu(fileName = "CappuccinoAssasino", menuName = "Scriptable Objects/CappuccinoAssasino")]
public class CappuccinoAssasinoData : ScriptableObject
{
    // BASIC STATS
    public float speed = 7f;
    public float attackCooldown = 3f;

    // STUN
    public float stunDuration;
}
