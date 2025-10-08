using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    public GameObject gameOverLayout;
    public PlayerData pd;
    public ShooterData shd;
    public StunnerData std;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Ballerina") || col.gameObject.CompareTag("TungTung"))
        {
            Time.timeScale = 0f;
            gameOverLayout.SetActive(true);
        }
    }

    public void playAgain()
    {
        resetData();
        SceneManager.LoadScene(1);
    }

    public void quitGame()
    {
        resetData();
        SceneManager.LoadScene(0);
    }

    private void resetData()
    {
        //player data
        pd.level = 1;
        pd.exp = 0;
        pd.expNeeded = 100;
        pd.resource = 0;

        //shooter data
        shd.level = 1;

        //stunner data
        std.level = 1;
    }
}
