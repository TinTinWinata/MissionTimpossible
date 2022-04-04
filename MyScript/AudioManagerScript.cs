using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    [SerializeField] AudioClip deathSong;
    [SerializeField] AudioClip victorySong;

    public static string playAudio;
    AudioSource audioSrc;


    // Start is called before the first frame update
    void Start()
    {
        playAudio = null;
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
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
            audioSrc.PlayOneShot(deathSong);
        }
        if (str == "victory")
        {
            audioSrc.PlayOneShot(victorySong);
        }
    }
}
