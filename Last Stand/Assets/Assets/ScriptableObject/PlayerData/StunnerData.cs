using UnityEngine;

[CreateAssetMenu(fileName = "StunnerData", menuName = "Scriptable Objects/StunnerData")]
public class StunnerData : ScriptableObject
{
    public float health = 8f;
    public float stunCooldown = 2f;
    public int level = 1;
}
