using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class UpgradePanel : MonoBehaviour
{
    public GameObject upgradePanelUI;
    public Button[] upgradeButtons;
    public TextMeshProUGUI[] upgradeDescTexts;
    public ShooterData sd;
    public KnockerData kd;
    private List<int> shooterUpgrades = new List<int> { 1, 2, 3, 4, 5 };
    public AudioManager am;

    public void Start()
    {
        upgradePanelUI.SetActive(false);
    }

    public void shooterUpgrade()
    {
        upgradePanelUI.SetActive(true);
    
        List<int> availableUpgrades = new List<int>();
    
        for (int i = 0; i < upgradeButtons.Length; i++)
        {
            int upgradeId = GetRandomShooterUpgrade();
        
            while (availableUpgrades.Contains(upgradeId) || 
                (upgradeId == 1 && sd.hasDoubleProjectile) || 
                (upgradeId == 2 && sd.hasSplashLane))
            {
                upgradeId = GetRandomShooterUpgrade();
            }
        
            availableUpgrades.Add(upgradeId);
            upgradeDescTexts[i].text = shooterGetUpgradeDescription(upgradeId);
        
            int id = upgradeId;
            upgradeButtons[i].onClick.RemoveAllListeners();
            upgradeButtons[i].onClick.AddListener(() => shooterApplyUpgrade(id));
        }
    
        Time.timeScale = 0f;
    }

    private int GetRandomShooterUpgrade()
    {
        float rand = Random.value; // 0.0 to 1.0
    
        if (rand < 0.05f) return 1; // 5% chance
        else if (rand < 0.10f) return 2; // 5% chance
        else if (rand < 0.40f) return 3; // 30% chance
        else if (rand < 0.70f) return 4; // 30% chance
        else return 5; // 30% chance
    }

    private string shooterGetUpgradeDescription(int upgradeId)
    {
        switch (upgradeId)
        {
            case 1:
                return "Double Projectile";
            case 2:
                return "Splash Lane";
            case 3:
                float currentAttackSpeed = sd.shootDelay;
                float newAttackSpeed = currentAttackSpeed * 0.9f; // 10% faster
                return $"Attack Speed: {currentAttackSpeed:F1}s -> {newAttackSpeed:F1}s";
            case 4:
                float currentDamage = sd.damage;
                float newDamage = currentDamage * 1.2f; // 20% more
                return $"Damage: {currentDamage:F1} -> {newDamage:F1}";
            case 5:
                float currentHealth = sd.maxHealth;
                float newHealth = currentHealth * 1.5f; // 50% more
                return $"Health: {currentHealth:F1} -> {newHealth:F1}";
            default:
                return "";
        }
    }

    private void shooterApplyUpgrade(int upgradeId)
    {
        switch (upgradeId)
        {
            case 1:
                sd.hasDoubleProjectile = true;
                break;
            case 2:
                sd.hasSplashLane = true;
                break;
            case 3:
                sd.shootDelay *= 0.9f;
                break;
            case 4:
                sd.damage *= 1.2f;
                break;
            case 5:
                sd.maxHealth *= 1.5f;
                sd.health = sd.maxHealth;
                break;
        }
        am.playSFX(am.levelUpTowerSFX);
        sd.level++;
        upgradePanelUI.SetActive(false);
        Time.timeScale = 1f;
    }

    private List<int> knockerUpgrades = new List<int> { 1, 2, 3, 4, 5 };

    public void knockerUpgrade()
    {
        upgradePanelUI.SetActive(true);
    
        List<int> availableUpgrades = new List<int>();
    
        for (int i = 0; i < upgradeButtons.Length; i++)
        {   
            int upgradeId = GetRandomKnockerUpgrade();
        
            while (availableUpgrades.Contains(upgradeId))
            {
                upgradeId = GetRandomKnockerUpgrade();
            }
        
            availableUpgrades.Add(upgradeId);
            upgradeDescTexts[i].text = knockerGetUpgradeDescription(upgradeId);
        
            int id = upgradeId;
            upgradeButtons[i].onClick.RemoveAllListeners();
            upgradeButtons[i].onClick.AddListener(() => knockerApplyUpgrade(id));
        }
    
        Time.timeScale = 0f;
    }

    private int GetRandomKnockerUpgrade()
    {
        float rand = Random.value;
    
        if (rand < 0.20f) return 1;
        else if (rand < 0.40f) return 2;
        else if (rand < 0.60f) return 3;
        else if (rand < 0.80f) return 4;
        else return 5;
    }

    private string knockerGetUpgradeDescription(int upgradeId)
    {
        switch (upgradeId)
        {
            case 1:
                float currentDamage = kd.damage;
                float newDamage = currentDamage + 1f;
                return $"Damage: {currentDamage:F1} -> {newDamage:F1}";;
            case 2:
                float currentKnockDistance = kd.pushDistance;
                float newKnockDistance = currentKnockDistance + 0.7f;
                return $"Push Distance: {currentKnockDistance:F1} -> {newKnockDistance:F1}";
            case 3:
                float currentKnockCd = kd.knockCooldown;
                float newKnockCd = currentKnockCd * 0.95f;
                return $"Cooldown: {currentKnockCd:F1}s -> {newKnockCd:F1}s";
            case 4:
                float currentHeatlh = kd.maxHealth;
                float newHealth = currentHeatlh * 1.2f; // 20% more
                return $"Max Health: {currentHeatlh:F1} -> {newHealth:F1}";
            case 5:
                float currentMoveSpeed = kd.moveSpeed;
                float newMoveSpeed = currentMoveSpeed * 1.1f; // 10% more
                return $"Wave Speed: {currentMoveSpeed:F1} -> {newMoveSpeed:F1}";
            default:
                return "";
        }
    }

    private void knockerApplyUpgrade(int upgradeId)
    {
        switch (upgradeId)
        {
            case 1:
                kd.damage ++;
                break;
            case 2:
                kd.pushDistance += 0.7f;
                break;
            case 3:
                kd.knockCooldown *= 0.95f;
                break;
            case 4:
                kd.maxHealth *= 1.2f;
                kd.health = kd.maxHealth;
                break;
            case 5:
                kd.moveSpeed *= 1.1f;
                break;
        }
        am.playSFX(am.levelUpTowerSFX);
        kd.level++;
        upgradePanelUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public StunnerData stunnerd;
private List<int> stunnerUpgrades = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };

public void stunnerUpgrade()
{
    upgradePanelUI.SetActive(true);
    
    List<int> availableUpgrades = new List<int>();
    
    for (int i = 0; i < upgradeButtons.Length; i++)
    {
        int upgradeId = GetRandomStunnerUpgrade();
        
        // Re-roll if duplicate or already owned
        while (availableUpgrades.Contains(upgradeId) || 
               (upgradeId == 2 && stunnerd.canKnockback))
        {
            upgradeId = GetRandomStunnerUpgrade();
        }
        
        availableUpgrades.Add(upgradeId);
        upgradeDescTexts[i].text = stunnerGetUpgradeDescription(upgradeId);
        
        int id = upgradeId;
        upgradeButtons[i].onClick.RemoveAllListeners();
        upgradeButtons[i].onClick.AddListener(() => stunnerApplyUpgrade(id));
    }
    
    Time.timeScale = 0f;
}

private int GetRandomStunnerUpgrade()
{
    float rand = Random.value;
    
    if (rand < 0.05f) return 1; // 5% - Damage
    else if (rand < 0.10f) return 2; // 5% - Knockback
    else if (rand < 0.25f) return 3; // 15% - Cooldown 2%
    else if (rand < 0.40f) return 4; // 15% - Cooldown 3%
    else if (rand < 0.55f) return 5; // 15% - Cooldown 4%
    else if (rand < 0.70f) return 6; // 15% - Cooldown 5%
    else if (rand < 0.85f) return 7; // 15% - Health
    else return 8; // 15% - Stun Time
}

private string stunnerGetUpgradeDescription(int upgradeId)
{
    switch (upgradeId)
    {
        case 1:
            float currentDamage = stunnerd.damage;
            float newDamage = currentDamage + 3f;
            return $"Damage: {currentDamage:F1} -> {newDamage:F1}";
        case 2:
            return "Brainrot takes 10% more damage when stunned";
        case 3:
            float currentCd3 = stunnerd.stunCooldown;
            float newCd3 = currentCd3 * 0.98f;
            return $"Cooldown: {currentCd3:F1}s -> {newCd3:F1}s (-2%)";
        case 4:
            float currentCd4 = stunnerd.stunCooldown;
            float newCd4 = currentCd4 * 0.97f;
            return $"Cooldown: {currentCd4:F1}s -> {newCd4:F1}s (-3%)";
        case 5:
            float currentCd5 = stunnerd.stunCooldown;
            float newCd5 = currentCd5 * 0.96f;
            return $"Cooldown: {currentCd5:F1}s -> {newCd5:F1}s (-4%)";
        case 6:
            float currentCd6 = stunnerd.stunCooldown;
            float newCd6 = currentCd6 * 0.95f;
            return $"Cooldown: {currentCd6:F1}s -> {newCd6:F1}s (-5%)";
        case 7:
            float currentHealth = stunnerd.maxHealth;
            float newHealth = currentHealth * 1.2f;
            return $"Health: {currentHealth:F1} -> {newHealth:F1}";
        case 8:
            float currentStunTime = stunnerd.stunTime;
            float newStunTime = currentStunTime * 1.1f;
            return $"Stun Time: {currentStunTime:F1}s -> {newStunTime:F1}s";
        default:
            return "";
    }
}

    private void stunnerApplyUpgrade(int upgradeId)
    {
        switch (upgradeId)
        {
            case 1:
                stunnerd.damage += 2f;
                break;
            case 2:
                stunnerd.canKnockback = true;
                break;
            case 3:
                stunnerd.stunCooldown *= 0.98f;
                break;
            case 4:
                stunnerd.stunCooldown *= 0.97f;
                break;
            case 5:
                stunnerd.stunCooldown *= 0.96f;
                break;
            case 6:
                stunnerd.stunCooldown *= 0.95f;
            break;
        case 7:
            stunnerd.maxHealth *= 1.2f;
            stunnerd.health = stunnerd.maxHealth;
            break;
        case 8:
            stunnerd.stunTime *= 1.1f;
            break;
        }
        am.playSFX(am.levelUpTowerSFX);
        stunnerd.level++;
        upgradePanelUI.SetActive(false);
        Time.timeScale = 1f;
    }
}
