using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public AudioMixer audiomixer;
    public GameObject slider;
    Resolution[] resolutions;
    public Dropdown resolutiondropdown;
    private void Start()
    {
        resolutions=Screen.resolutions;
        resolutiondropdown.ClearOptions();
        //List<string> options = new List<string>
        //for (int i =0;i<resolutions.Length;i++)
        //{
            
        //}
    }
    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audiomixer.SetFloat("MyExposedParam", volume);

    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }


}