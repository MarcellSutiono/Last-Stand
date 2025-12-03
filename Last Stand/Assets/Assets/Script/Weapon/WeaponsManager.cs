using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsManager : MonoBehaviour
{
    [SerializeField] private GameObject shooterGO;
    [SerializeField] private GameObject stunnerGO;
    [SerializeField] private GameObject knockerGO;

    [SerializeField] private ShooterData shd;
    [SerializeField] private StunnerData std;
    [SerializeField] private KnockerData kd;

    [SerializeField] private Slider shooter;
    [SerializeField] private Slider stunner;
    [SerializeField] private Slider knocker;

    private bool shooterBroken = false;
    private bool stunnerBroken = false;
    private bool knockerBroken = false;

    private void Update()
    {
        shooterHandler();
        stunnerHandler();
        knockerHandler();
    }

    private void shooterHandler()
    {
        shooter.value = shd.health;

        if(shd.health <= 0 && !shooterBroken)
        {
            shooterBroken = true;
            shooterGO.SetActive(false);
            StartCoroutine(cooldown(5f, "sh"));
        }
    }

    private void stunnerHandler()
    {
        stunner.value = std.health;

        if (std.health <= 0 && !stunnerBroken)
        {
            stunnerBroken = true;
            stunnerGO.SetActive(false);
            StartCoroutine(cooldown(5f, "st"));
        }
    }

    private void knockerHandler()
    {
        knocker.value = kd.health;

        if (kd.health <= 0 && !knockerBroken)
        {
            knockerBroken = true;
            knockerGO.SetActive(false);
            StartCoroutine(cooldown(5f, "kn"));
        }
    }

    private IEnumerator cooldown(float time, string type)
    {
        if(type == "sh")
        {
            yield return new WaitForSeconds(time);
            shd.health = shd.maxHealth;
            shooterBroken = false;
            shooterGO.SetActive(true);
        }
        else if (type == "st")
        {
            yield return new WaitForSeconds(time);
            std.health = std.maxHealth;
            stunnerBroken = false;
            stunnerGO.SetActive(true);
        }
        else if (type == "kn")
        {
            yield return new WaitForSeconds(time);
            kd.health = kd.maxHealth;
            knockerBroken = false;
            knockerGO.SetActive(true);
        }
    }
}
