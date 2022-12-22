using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public Sound[] sounds;

    private Dictionary<string, Sound> soundMap;

    private void Awake()
    {
        // this is needed to place AudioManager object
        // at the root level, and DontDestroyOnLoad
        // requires GameObjects to be at the root level. 
        gameObject.transform.parent = null;

        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        Instance = this;
        soundMap = new Dictionary<string, Sound>();

        foreach (Sound sound in sounds)
        {
            sound.Source = gameObject.AddComponent<AudioSource>();
            sound.Source.clip = sound.Clip;
            sound.Source.loop = sound.Loop;
            sound.Source.volume = sound.Volume;
            sound.Source.pitch = sound.Pitch;
            
            soundMap.Add(sound.Clip.name, sound);
        }
    }

    void Start()
    {
        Play("COLORMAN");
    }

    public void Play(string name)
    {
        if (soundMap.ContainsKey(name) == false)
        {
            Debug.LogWarning($"Sound {name} does not exist");
            return;
        }

        Sound sound = soundMap[name];
        
        if (sound.Loop == true)
        {
            sound.Source.Play();
        }
        else
        {
            sound.Source.PlayOneShot(sound.Clip);
        }
    }

    public void PlayIfNotAlready(string name)
    {
        if (soundMap.ContainsKey(name) == false)
        {
            Debug.LogWarning($"Sound {name} does not exist");
            return;
        }

        Sound sound = soundMap[name];

        if (sound.Source.isPlaying == true) return;

        sound.Source.PlayOneShot(sound.Clip);
    }

    public void PlayIncrementalPitch(string name, float amount)
    {
        if (soundMap.ContainsKey(name) == false)
        {
            Debug.LogWarning($"Sound {name} does not exist");
            return;
        }

        Sound sound = soundMap[name];

        sound.Source.PlayOneShot(sound.Clip);
        sound.Source.pitch += amount;
    }

    public void ResetPitch(string name)
    {
        if (soundMap.ContainsKey(name) == false)
        {
            Debug.LogWarning($"Sound {name} does not exist");
            return;
        }

        Sound sound = soundMap[name];

        sound.Source.pitch = sound.Pitch;
    }

    public void Stop(string name)
    {
        if (soundMap.ContainsKey(name) == false)
        {
            Debug.LogWarning($"Sound {name} does not exist");
            return;
        }

        Sound sound = soundMap[name];
        
        sound.Source.Stop();
    }

    public void Mute(string name, bool mute)
    {
        if (soundMap.ContainsKey(name) == false)
        {
            Debug.LogWarning($"Sound {name} does not exist");
            return;
        }

        Sound sound = soundMap[name];
        sound.Source.mute = mute;
    }

    public void MuteAll(bool mute)
    {
        foreach (Sound sound in soundMap.Values)
        {
            sound.Source.mute = mute;
        }
    }
}

[Serializable]
public class Sound
{
    public AudioClip Clip;
    public bool Loop;
    [Range(0f, 1f)]
    public float Volume = 1f;
    [Range(.1f, 3f)]
    public float Pitch = 1f;
    [HideInInspector]
    public AudioSource Source;
}
