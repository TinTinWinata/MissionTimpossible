using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManagerScript : MonoBehaviour
{
    public static string playSound;

    [SerializeField] AudioClip menuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip bossMusic;
    [SerializeField] AudioClip afterSceneMusic;


    static AudioSource audioSrc;
    bool wait;

    // Start is called before the first frame update
    void Start()
    {
        wait = true;
        playSound = null;
        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!audioSrc.isPlaying)
        {
            if(wait)
             StartCoroutine(playAmbient());
        }
        if(playSound != null)
        {
            PlaySound(playSound);
            playSound = null;
        }
    }
    IEnumerator playAmbient()
    {
        wait = false;

        PlaySound("game");
        yield return new WaitForSeconds(60);
        wait = true;
    }



    public void PlaySound(string str)
    {
        if (str == "menu")
        {
            audioSrc.PlayOneShot(menuMusic);
        }
        else if (str == "game")
        {
            audioSrc.PlayOneShot(gameMusic);
        }
        else if (str == "boss")
        {
            audioSrc.PlayOneShot(bossMusic);
        }else if(str == "after")
        {
            audioSrc.PlayOneShot(afterSceneMusic);
        }
    }
}
