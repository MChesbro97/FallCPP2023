using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class CanvasManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    [Header("Button")]
    public Button playButton;
    public Button settingsButton;
    public Button quitButton;
    public Button backButton;
    public Button returnToMenuButton;
    public Button returnToGameButton;

    [Header("Menus")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject settingsMenu;

    [Header("Text")]
    public Text livesText;
    public Text masterVolSliderText;
    public Text musicVolSliderText;
    public Text sfxVolSliderText;

    [Header("Slider")]
    public Slider masterVolSlider;
    public Slider musicVolSlider;
    public Slider sfxVolSlider;



    // Start is called before the first frame update
    void Start()
    {
        if (playButton)
            playButton.onClick.AddListener(() => GameManager.Instance.changeScene(1));
        if (returnToMenuButton)
            returnToMenuButton.onClick.AddListener(() => GameManager.Instance.changeScene(0));

        if (returnToGameButton)
            returnToGameButton.onClick.AddListener(UnpauseGame);

        if (settingsButton)
            settingsButton.onClick.AddListener(ShowSettingsMenu);

        if (backButton)
            backButton.onClick.AddListener(ShowMainMenu);

        if (quitButton)
            quitButton.onClick.AddListener(Quit);

        if (livesText)
        {
            GameManager.Instance.OnLivesValueChanged.AddListener((value) => UpdateLivesText(value));
            livesText.text = "Lives: " + GameManager.Instance.lives.ToString();
        }

        if (masterVolSlider)
        {
            masterVolSlider.onValueChanged.AddListener((value) => OnMasterSliderValueChanged(value));
            float newValue;
            audioMixer.GetFloat("MasterVol", out newValue);
            masterVolSlider.value = newValue + 80;
            if (masterVolSliderText)
                masterVolSliderText.text = (Mathf.Ceil(newValue + 80).ToString());
        }

        if (musicVolSlider)
        {
            musicVolSlider.onValueChanged.AddListener((value) => OnMusicSliderValueChanged(value));

            float newValue;
            audioMixer.GetFloat("MusicVol", out newValue);
            musicVolSlider.value = newValue + 80;
            if (musicVolSliderText)
                musicVolSliderText.text = (Mathf.Ceil(newValue + 80).ToString());
        }

        if (sfxVolSlider)
        {
            sfxVolSlider.onValueChanged.AddListener((value) => OnSFXSliderValueChanged(value));

            float newValue;
            audioMixer.GetFloat("SFXVol", out newValue);
            sfxVolSlider.value = newValue + 80;
            if (sfxVolSliderText)
                sfxVolSliderText.text = (Mathf.Ceil(newValue + 80).ToString());

        }
    }

    void OnMasterSliderValueChanged(float value)
    {
        masterVolSliderText.text = value.ToString();
        audioMixer.SetFloat("MasterVol", value - 80);
    }

    void OnMusicSliderValueChanged(float value)
    {
        musicVolSliderText.text = value.ToString();
        audioMixer.SetFloat("MusicVol", value - 80);
    }
    void OnSFXSliderValueChanged(float value)
    {
        sfxVolSliderText.text = value.ToString();
        audioMixer.SetFloat("SFXVol", value - 80);
    }

    void UnpauseGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    void UpdateLivesText(int value)
    {
        if (livesText)
            livesText.text = "Lives: " + value.ToString();
    }

    void ShowSettingsMenu()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    private void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu) return;

        if (Input.GetKeyDown(KeyCode.P))
        {
            pauseMenu.SetActive(!pauseMenu.activeSelf);

            //hints for the lab
            if (pauseMenu.activeSelf)
            {
                Time.timeScale = 0;
            }
            else
            {
                Time.timeScale = 1;
            }
        }
    }
}