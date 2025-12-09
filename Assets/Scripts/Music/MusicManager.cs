using UnityEngine;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance;

    [Header("Audio nguồn phát nhạc")]
    public AudioSource bgm;

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
        float volume = PlayerPrefs.GetFloat("GameVolume", 1f);
        int musicState = PlayerPrefs.GetInt("MusicState", 1);

        bgm.volume = volume;

        if (musicState == 1) bgm.Play();
        else bgm.Pause();
    }

    public void SetVolume(float value)
    {
        bgm.volume = value;
        PlayerPrefs.SetFloat("GameVolume", value);
        PlayerPrefs.Save();
    }

    public void SetMusic(bool isOn)
    {
        if (isOn) bgm.Play();
        else bgm.Pause();

        PlayerPrefs.SetInt("MusicState", isOn ? 1 : 0);
        PlayerPrefs.Save();
    }
}
