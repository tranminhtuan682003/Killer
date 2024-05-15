using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager instance { get; private set; }
    public List<Sound> playerSounds;
    public List<Sound> backgroundSounds;
    private Queue<Sound> backgroundQueue = new Queue<Sound>();
    private AudioSource currentBackgroundSource;

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
        ListPlayerSounds();
        ListBackgroundSounds();
    }

    private void ListPlayerSounds()
    {
        foreach (Sound sound in playerSounds)
        {
            sound.soundsource = gameObject.AddComponent<AudioSource>();
            sound.soundsource.clip = sound.audioclip;
            sound.soundsource.volume = sound.volume;
            sound.soundsource.pitch = sound.pitch;
        }
    }

    private void ListBackgroundSounds()
    {
        foreach (Sound sound in backgroundSounds)
        {
            sound.soundsource = gameObject.AddComponent<AudioSource>();
            sound.soundsource.clip = sound.audioclip;
            sound.soundsource.volume = sound.volume;
            sound.soundsource.pitch = sound.pitch;
        }
    }

    public void PlayerSounds(string name)
    {
        Sound sound = playerSounds.Find(s => s.name == name);
        sound.soundsource.Play();
    }

    public void BackgroundSounds(string name)
    {
        Sound sound = backgroundSounds.Find(s => s.name == name);
        backgroundQueue.Enqueue(sound);

        if (currentBackgroundSource == null || !currentBackgroundSource.isPlaying)
        {
            StartCoroutine(PlayBackgroundQueue());
        }
    }

    public IEnumerator PlayBackgroundQueue()
    {
        while (backgroundQueue.Count > 0)
        {
            Sound sound = backgroundQueue.Dequeue();
            currentBackgroundSource = sound.soundsource;
            currentBackgroundSource.Play();

            yield return new WaitForSeconds(currentBackgroundSource.clip.length);
        }
    }

    public void Stop(string name)
    {
        Sound soundBackground = backgroundSounds.Find(s => s.name == name);
        soundBackground.soundsource.Stop();
    }
    public void StopPlayerSounds(string name)
    {
        Sound soudPlayer = playerSounds.Find(s => s.name == name);
        soudPlayer.soundsource.Stop();
    }

    public void SetVolume()
    {
        foreach (Sound music in backgroundSounds)
        {
            music.soundsource.volume = 0;
        }
    }
    public void EditVolume( float value)
    {
        foreach (Sound music in backgroundSounds)
        {
            music.soundsource.volume = value;
        }
    }
}
