using UnityEngine;

public enum SFX
{
    Theme,
    Ocean,
    GameOver,
}

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [SerializeField]
    private AudioClip[] sfxSounds;

    private AudioSource audioSource;

    private void Awake()
    {
        instance = this;
        audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(SFX sfxSound, float volumeScale = 1f)
    {
        audioSource.clip = sfxSounds[(int)sfxSound];
        audioSource.PlayOneShot(audioSource.clip, volumeScale);
    }
}
