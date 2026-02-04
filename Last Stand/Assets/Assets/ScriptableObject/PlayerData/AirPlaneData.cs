using UnityEngine;

[CreateAssetMenu(fileName = "AirPlaneData", menuName = "Scriptable Objects/AirPlaneData")]
public class AirPlaneData : ScriptableObject
{
    // BASIC STATS
    public float speed = 7f;
    public float attackCooldown = 3f;

    // STUN
    public float stunDuration;
}
