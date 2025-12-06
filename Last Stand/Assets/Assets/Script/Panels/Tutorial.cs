using UnityEngine;

public class Tutorial : MonoBehaviour
{
    private int currIdx = 0;
    public GameObject[] tutorials;
    public GameObject tutorialPanel;
    public GameObject left;
    public GameObject right;

    void Start()
    {
        openTutorial();
        UpdateButtons();
        activeTutorChecker();
    }

    void Update()
    {
        
    }
    private void UpdateButtons()
    {
        left.SetActive(currIdx > 0);
        right.SetActive(currIdx < tutorials.Length - 1);
    }

    private void activeTutorChecker()
    {
        for (int i = 0; i < tutorials.Length; i++)
        {
            tutorials[i].SetActive(i == currIdx);
        }
    }

    public void next()
    {
        if (currIdx < tutorials.Length - 1)
        {
            currIdx++;
            activeTutorChecker();
            UpdateButtons();
        }
    }

    public void prev()
    {
        if (currIdx > 0)
        {
            currIdx--;
            activeTutorChecker();
            UpdateButtons();
        }
    }

    public void closeTutorial()
    {
        Time.timeScale = 1f;
        tutorialPanel.SetActive(false);
    }

    private void openTutorial()
    {
        Time.timeScale = 0f;
        tutorialPanel.SetActive(true);
    }
}
