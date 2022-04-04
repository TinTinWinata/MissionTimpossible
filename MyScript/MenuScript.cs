using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuScript : MonoBehaviour
{
    Resolution[] resolutions;
    [SerializeField] GameObject startUi;
    [SerializeField] GameObject settingUI;
    [SerializeField] TMP_Dropdown resolutionDropdown;
    [SerializeField] TMP_Dropdown qualityDropdown;
    private bool setting;
    // Start is called before the first frame update
    void Start()
    {
        MusicManagerScript.playSound = "menu";
        resolutionDropdown.ClearOptions();
        qualityDropdown.ClearOptions();
        setting = false;

        resolutions = Screen.resolutions;
        List<string> resolutionList = new List<string>();

        foreach(Resolution currRes in resolutions)
        {
            string temp = currRes.width + " x " + currRes.height;
            resolutionList.Add(temp);
        }

        resolutionDropdown.AddOptions(resolutionList);

        List<string> qualityList = new List<string>();
        string[] qualities = QualitySettings.names;
        

        foreach(string currQuality in qualities)
        {
            qualityList.Add(currQuality);
        }
        qualityDropdown.AddOptions(qualityList);

    }

    // Update is called once per frame
    void Update()
    {
        if(setting)
        {
            settingUI.SetActive(true);
            startUi.SetActive(false);
        }
        else
        {
            settingUI.SetActive(false);
            startUi.SetActive(true);
        }
    }

    public void Setting()
    {
        setting = true;
    }
    public void FalseSetting()
    {
        setting = false;
    }
    public void Play()
    {
        SceneManager.LoadScene("MainScene");
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void FullScreen()
    {
        Screen.fullScreen = !Screen.fullScreen;

    }

    public void Resolution(int num)
    {
        try
        {
            Screen.SetResolution(resolutions[num].width, resolutions[num].height, Screen.fullScreen);
        }
        catch (System.NullReferenceException e)
        {

        }
    }

    public void Quality(int num)
    {
        QualitySettings.SetQualityLevel(num, true);
    }
}
