using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FaderInOut : MonoBehaviour
{
    public float fadeSpeed = 1.5f;

    private bool sceneStarting = true;

    private Image guiTexture;

    void Awake()
    {
        guiTexture = GetComponent<Image>();

    }

    void Update()
    {
        if(sceneStarting)
        {
            StartScene();
        }
    }

    void FadeToClear()
    {
        guiTexture.color = Color.Lerp(guiTexture.color, Color.clear, fadeSpeed * Time.deltaTime);
    }

    void FadeToBlack()
    {
        guiTexture.color = Color.Lerp(guiTexture.color, Color.black, fadeSpeed * Time.deltaTime);
    }


    void StartScene()
    {
        guiTexture.enabled = true;
        FadeToClear();

        if (guiTexture.color.a <= 0.05f)
        {
            guiTexture.color = Color.clear;
            guiTexture.enabled = false;

            sceneStarting = false;
        }
    }


    public void Endscene()
    {
        guiTexture.enabled = true;
        FadeToBlack();

        if(guiTexture.color.a >= .95f)
        {
            SceneManager.LoadScene(0);
        }
    }

}
