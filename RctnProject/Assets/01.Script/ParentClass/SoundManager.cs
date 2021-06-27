using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource backMusic;
    private bool isBack;


    void Start()
    {

        UiManager.instance.musicBtn.onClick.AddListener(SetBackVolume);
        isBack = false;

    }

    public void SetBackVolume()
    {
        if(!isBack)
        {
               UiManager.instance.musicTxt.text = "OFF";
            GameManager.instance.necroAudio.gameObject.SetActive(false);
            GameManager.instance.deadAudio.gameObject.SetActive(false);
            for (int i = 0; i < GameManager.instance.attackAudio.Length; i++)
            {
            GameManager.instance.attackAudio[i].gameObject.SetActive(false);
            }
            backMusic.gameObject.SetActive(false);
            isBack = true;
        }
        else
        {

              UiManager.instance.musicTxt.text = "ON";
             GameManager.instance.necroAudio.gameObject.SetActive(true);
            GameManager.instance.deadAudio.gameObject.SetActive(true);
            for (int i = 0; i < GameManager.instance.attackAudio.Length; i++)
            {
            GameManager.instance.attackAudio[i].gameObject.SetActive(true);
            }
            backMusic.gameObject.SetActive(true);
            isBack = false;
        }
    }
}
