using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;

    public AudioClip gameMusic;

    public AudioClip damagedSFX;
    public AudioClip playerHitSFX;
    public AudioClip enemyHitSFX;
    public AudioClip flashSFX;
    public AudioClip pickTowerSFX;
    public AudioClip placeTowerSFX;
    public AudioClip WeaponSwingSFX;
    public AudioClip zapSFX;

    private void Start()
    {
        musicSource.clip = gameMusic;
        musicSource.Play();
    }

    public void playSFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }
}
