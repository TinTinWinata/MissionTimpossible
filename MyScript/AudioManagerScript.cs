using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [SerializeField] AudioClip movementSound;
    [SerializeField] AudioClip deathSong;
    [SerializeField] AudioClip victorySong;
    [SerializeField] AudioClip doorOpenSound;

    public static string playAudio;
    AudioSource audioSrc;


    public static bool isPlaying;
    [SerializeField] float movementVolume = 1f;
    [SerializeField] float movementPitch = 1f;


    // Start is called before the first frame update
    void Start()
    {
        playAudio = null;
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        isPlaying = audioSrc.isPlaying;
        if (playAudio != null)
        {
            PlaySound(playAudio);
            playAudio = null;
        }
    }

    public void PlaySound(string str)
    {
        if(str == "death")
        {
            audioSrc.volume = 1f;
            audioSrc.pitch = 1f;
            audioSrc.PlayOneShot(deathSong);
        }
        if (str == "victory")
        {
            audioSrc.volume = 1f;
            audioSrc.pitch = 1f;
            audioSrc.PlayOneShot(victorySong);
        }
        if (str == "move")
        {
            audioSrc.volume = movementVolume;
            audioSrc.pitch = movementPitch;
            audioSrc.PlayOneShot(movementSound);
        }
        if (str == "door")
        {
            audioSrc.volume = 1f;
            audioSrc.pitch = 1f;
            audioSrc.PlayOneShot(doorOpenSound);
        }
    }
}
