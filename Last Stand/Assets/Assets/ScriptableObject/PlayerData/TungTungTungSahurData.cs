using UnityEngine;

[CreateAssetMenu(fileName = "TungTungTungSahurData", menuName = "Scriptable Objects/TungTungTungSahurData")]
public class TungTungTungSahurData : ScriptableObject
{
    // BASIC STATS
    public float speed = 4f;
    public float attackCooldown = 3f;

    // STUN
    public float stunDuration;
}
