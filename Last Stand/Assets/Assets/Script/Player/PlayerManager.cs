using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private PlayerData pd;
    public Slider slider;
    public TextMeshProUGUI levelText;

    private void Start()
    {
        slider.maxValue = pd.expNeeded;
        slider.value = pd.exp;
    }

    private void Update()
    {
        levelText.text = pd.level.ToString();
        levelUpChecker();
    }

    private void levelUpChecker()
    {
        slider.value = pd.exp;
        if (pd.exp >= pd.expNeeded)
        {
            pd.exp = 0;
            pd.level++;
        }
    }
}
