using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class VideoScript : MonoBehaviour
{
    [SerializeField] public List<PlayableDirector> filmList;
    private int playing = 0;
    public static bool nextFilm;
    public static int playFilmIndex;
    // Start is called before the first frame update
    public static bool noVideo;

   
    void Start()
    {
        noVideo = true;
        PlayNextFilm();
        playFilmIndex = -1;
    }

    // Update is called once per frame
    void Update()
    {

        if(nextFilm)
        {
            PlayNextFilm();
        }
        if(playFilmIndex != -1)
        {
            PlayFilm(playFilmIndex);
        }
     }
    
    public void PlayNextFilm()
    {
        filmList[playing].Play();
        playing += 1;
    }
    public void PlayFilm(int idx)
    {
        playing = idx;
        filmList[idx].Play();
        playFilmIndex = -1;
    }

}
