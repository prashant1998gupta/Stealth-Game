using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlayerSighting : MonoBehaviour
{
    public Vector3 position = new Vector3(1000f, 1000f, 1000f);
    public Vector3 resetPosition = new Vector3(1000f, 1000f, 1000f);
    public float lightHightIntensity = 0.25f;
    public float lightLowIntensity = 0f;
    public float fadeSpeed = 7f;
    public float musicFadeSpeed = 1f;

    private AlarmLight alarm;
    private Light mainLight;
    private AudioSource panicAudio;
    private AudioSource[] siren;

    void Awake()
    {
        alarm = GameObject.FindGameObjectWithTag(Tags.alarm).GetComponent<AlarmLight>();
        mainLight = GameObject.FindGameObjectWithTag(Tags.mainLight).GetComponent<Light>();
        panicAudio = transform.Find("SecondaryMusic").GetComponent<AudioSource>();
        GameObject[] sirenGameObject = GameObject.FindGameObjectsWithTag(Tags.siren);
        siren = new AudioSource[sirenGameObject.Length];

        for (int i = 0; i < siren.Length; i++)
        {
            siren[i] = sirenGameObject[i].GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        SwitchAlarms();
        MusicFading();
    }
    void SwitchAlarms()
    {
        alarm.alarmOn = (position != resetPosition);

        float newIntensity;

        if(position != resetPosition)
        {
            newIntensity = lightLowIntensity;
        }
        else
        {
            newIntensity = lightHightIntensity;
        }

        mainLight.intensity = Mathf.Lerp(mainLight.intensity, newIntensity, fadeSpeed * Time.deltaTime);

        for (int i = 0; i < siren.Length; i++)
        {
            if((position != resetPosition) && !siren[i].isPlaying)
            {
                siren[i].Play();
            }
            else if(position == resetPosition)
            {
                siren[i].Stop();
            }
        }
    }

    void MusicFading()
    {
        if(position != resetPosition)
        {
            GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0.8f, musicFadeSpeed * Time.deltaTime);
        }
        else
        {
            GetComponent<AudioSource>().volume = Mathf.Lerp(GetComponent<AudioSource>().volume, 0.8f, musicFadeSpeed * Time.deltaTime);
            panicAudio.volume = Mathf.Lerp(panicAudio.volume, 0f, musicFadeSpeed * Time.deltaTime);
        }
    }

}
