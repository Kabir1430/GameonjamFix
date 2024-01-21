using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gamemanager : MonoBehaviour
{
        private bool isPaused = false;
        public GameObject pausepanel;
         public Slider soundSlider;
         public AudioSource audioSource;

         void Start()
         {
            soundSlider.onValueChanged.AddListener(OnSoundVolumeChanged);

        // Initialize sound volume based on the default or saved value
        float savedVolume = PlayerPrefs.GetFloat("SoundVolume", 0.5f);
        soundSlider.value = savedVolume;
        OnSoundVolumeChanged(savedVolume);
         }
          void OnSoundVolumeChanged(float volume)
    {
        // Set the volume of the audio source
        audioSource.volume = volume;

        // Save the volume value for future sessions
        PlayerPrefs.SetFloat("SoundVolume", volume);
    }
    
    void Update()
    {
        
        // Check for "P" key input to toggle pause
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
            pausepanel.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void TogglePause()
    {
        isPaused = !isPaused;

        Time.timeScale = isPaused ? 0 : 1;
    }
}
