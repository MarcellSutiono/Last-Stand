using UnityEngine;

public class UpgradePanel : MonoBehaviour
{
    public GameObject upgradePanelUI;

    public void Start()
    {
        upgradePanelUI.SetActive(false);
    }

    public void OnUpgradePanelOpen()
    {
        upgradePanelUI.SetActive(true);
        Time.timeScale = 0f;
    }
}
