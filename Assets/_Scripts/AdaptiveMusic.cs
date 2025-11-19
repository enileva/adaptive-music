using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AdaptiveMusic : MonoBehaviour
{
    public AudioSource musicSource;
    public Slider healthSlider;
    public Slider volumeSlider;
    public TMP_Text pauseButtonText;
    private bool isPaused = false;
    public Button pauseButton;

    [Header("Day/Night Settings")]
    public Button DayNight;
    public TMP_Text DayNightText;
    public string sun = "\u263C";
    public string moon = "\u263E";
    public Material skyboxDay;
    public Material skyboxNight;
    private bool isDay = true;
    private Image dayNightButtonImage;

    [Header("Pitch Settings")]
    public float normalPitch = 1.0f;
    public float maxPitch = 1.5f;
    public float minHealthThreshold = 10f;


    void Start()
    {
        dayNightButtonImage = DayNight.GetComponent<Image>();
        DayNightText.text = sun;
        DayNightText.color = Color.orange;
        dayNightButtonImage.color = Color.white;
        PlayMusic();
        pauseButton.image.color = Color.red;   // playing
        pauseButtonText.text = "Pause";
    }

    void Update()
    {
        float health = healthSlider.value;
        float healthPercent = Mathf.Clamp01(health);

        if (healthPercent * 100f > minHealthThreshold)
        {
            float t = 1f - healthPercent;
            musicSource.pitch = Mathf.Lerp(normalPitch, maxPitch, t);
        }
        else
        {
            musicSource.pitch = maxPitch;
        }

        if (volumeSlider != null)
        {
            musicSource.volume = volumeSlider.value;
        }
    }

    public void PlayMusic()
    {
        if (!musicSource.isPlaying)
        {
            musicSource.Play();
            isPaused = false;
        }
    }

    public void PauseMusic()
    {
        if (!isPaused)
        {
            pauseButton.image.color = Color.green; // paused
            pauseButtonText.text = "Play";
            musicSource.Pause();
            isPaused = true;
        }
        else
        {
            pauseButton.image.color = Color.red;   // playing
            pauseButtonText.text = "Pause";
            musicSource.Play();
            isPaused = false;
        }
    }

    public void SwitchDayNight()
    {
        if (isDay)
        {
            DayNightText.text = moon;
            DayNightText.color = Color.white;
            dayNightButtonImage.color = Color.black;
            RenderSettings.skybox = skyboxNight;
            isDay = false;
        }
        else
        {
            DayNightText.text = sun;
            DayNightText.color = Color.orange;
            dayNightButtonImage.color = Color.white;
            RenderSettings.skybox = skyboxDay;
            isDay = true;
        }
    }
}