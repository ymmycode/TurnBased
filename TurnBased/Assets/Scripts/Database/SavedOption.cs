using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SavedOption : MonoBehaviour
{
    public AudioMixer mixer;
    public float savedVolume;
    float initVolume;

    public void setLevel(float sliderValue)
    {
        savedVolume = sliderValue; //save the value  in the new variable
        mixer.SetFloat("MyMaster", Mathf.Log10(sliderValue) * 20);
    }
    void Start()
    {
    }

    public void SyncOption()
    {
        StartCoroutine(Main.Instance.Web.UpdateVolume(Main.Instance.Web.userID));
    }

    void Update()
    {
        Main.Instance.Web.savedVolumeValue = savedVolume;
    }


}
