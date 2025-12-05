using UnityEngine;

[CreateAssetMenu(fileName = "StunnerData", menuName = "Scriptable Objects/StunnerData")]
public class StunnerData : ScriptableObject
{
    public float maxHealth = 40f;
    public float health = 40f;
    public float stunCooldown = 2f;
    public int level = 1;
    public float damage = 0;
    public float stunTime = 2f;
    public bool canKnockback = false;
}
