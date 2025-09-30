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
}
