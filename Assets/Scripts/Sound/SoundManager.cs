using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    [Header("Audio Source phát SFX")]
    public AudioSource sfxSource;

    [Header("Danh sách sound")]
    public AudioClip shootSFX;
    public AudioClip moveSFX; // 👈 thêm sound di chuyển

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            LoadSettings();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadSettings()
    {
        float volume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        int state = PlayerPrefs.GetInt("SFXState", 1);

        sfxSource.volume = volume;
        sfxSource.mute = (state == 0);
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && !sfxSource.mute)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void SetVolume(float value)
    {
        sfxSource.volume = value;
        PlayerPrefs.SetFloat("SFXVolume", value);
        PlayerPrefs.Save();
    }

    public void SetSFX(bool isOn)
    {
        sfxSource.mute = !isOn;
        PlayerPrefs.SetInt("SFXState", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}