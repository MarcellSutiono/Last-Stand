using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerData pd;
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

        levelUpChecker();
    }

    private void levelUpChecker()
    {
        slider.value = pd.exp;
        if (pd.exp >= pd.expNeeded)
        {
            pd.exp = 0;
            pd.level++;
            pd.expNeeded += 15 * pd.level;
        }
    }
}
