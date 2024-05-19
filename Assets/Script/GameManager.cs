using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance {  get; private set; }
    public GameObject[] panelSetting;
    private int sceneIndex = 0;
    public string[] nameSound;
    public Slider volumeSlide;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    private void Start()
    {
        volumeSlide.value = 1;
        PlaySound();
    }
    private void PlaySound()
    {
        if(sceneIndex == 0)
        {
            MusicManager.instance.BackgroundSounds("ThangDien");
        }
    }
    public void Setting()
    {
        panelSetting[0].SetActive(true);
    }
    public void ChooseLevels()
    {
        panelSetting[1].SetActive(true);
    }
    public void PlayGame()
    {
        sceneIndex = 1;
        SceneManager.LoadScene(sceneIndex);
        if(sceneIndex == 1)
        {
            MusicManager.instance.Stop("ThangDien");
            for(int i = 0;i<nameSound.Length;i++)
            {
                MusicManager.instance.BackgroundSounds(nameSound[i]);
            }
            MusicManager.instance.BackgroundSounds("LetGame");
        }
    }
    public void EndGame()
    {
        UIScene2.instance.EndGame();
    }
    public void Exit()
    {
        foreach(var item in panelSetting)
        {
            if(item.activeInHierarchy)
            {
                item.SetActive(false);
            }
        }
    }
    public void EditVolume()
    {
        MusicManager.instance.EditVolume(volumeSlide.value);
    }
    public void DisSound()
    {
        MusicManager.instance.SetVolume();
    }
}
