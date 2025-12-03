using UnityEngine;

[CreateAssetMenu(fileName = "KnockerData", menuName = "Scriptable Objects/KnockerData")]
public class KnockerData : ScriptableObject
{
    public float maxHealth = 25f;
    public float health = 25f;
    public int level = 1;
}
