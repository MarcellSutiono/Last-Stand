using UnityEngine;

public class GameManager : MonoBehaviour
{
    public PlayerData playerData;
    public ShooterData shooterData;
    public StunnerData stunnerData;
    public KnockerData knockerData;

    void Awake()
    {
        ResetAllData();
    }

    public void ResetAllData()
    {
        // Reset PlayerData
        playerData.playerSpeed = 10f;
        playerData.holdShooter = false;
        playerData.holdStunner = false;
        playerData.holdKnocker = false;
        playerData.exp = 0;
        playerData.level = 1;
        playerData.expNeeded = 10;
        playerData.resource = 999;
        playerData.health = 100;
        playerData.maxHealth = 100;
        playerData.attackCooldown = 0.6f;
        playerData.damageTaken = 10;

        // Reset ShooterData
        shooterData.maxHealth = 50f;
        shooterData.health = 50f;
        shooterData.shootDelay = 2.4f;
        shooterData.level = 1;
        shooterData.enemyInsight = false;
        shooterData.damage = 1f;
        shooterData.hasDoubleProjectile = false;
        shooterData.hasSplashLane = false;

        // Reset StunnerData
        stunnerData.maxHealth = 40f;
        stunnerData.health = 40f;
        stunnerData.stunCooldown = 2f;
        stunnerData.level = 1;
        stunnerData.damage = 0;

        // Reset KnockerData
        knockerData.maxHealth = 25f;
        knockerData.health = 25f;
        knockerData.level = 1;

    }
}