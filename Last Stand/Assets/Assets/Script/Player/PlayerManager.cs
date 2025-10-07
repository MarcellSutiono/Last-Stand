using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerData pd;
    public TextMeshProUGUI resourceCount;
    public Slider slider;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI expText;

    private void Start()
    {
        slider.maxValue = pd.expNeeded;
        slider.value = pd.exp;
    }

    private void Update()
    {
        levelText.text = pd.level.ToString();
        expText.text = $"{pd.exp} / {pd.expNeeded}";

        resourceCount.text = pd.resource.ToString();

        levelUpChecker();
    }

    private void levelUpChecker()
    {
        slider.value = pd.exp;
        if (pd.exp >= pd.expNeeded)
        {
            pd.exp = 0;
            pd.maxHealth += 5;
            pd.health = pd.maxHealth;
            if (pd.attackCooldown - 0.3f > 0) pd.attackCooldown -= 0.3f;
            pd.level++;
            if (pd.level == 3) pd.damageTaken -= 5;
            pd.resource++;
            pd.expNeeded += 15 * pd.level;
        }
    }
}
