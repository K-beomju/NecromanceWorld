using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeadPanel : MonoBehaviour
{

      AsyncOperation async;
     public GameObject backMusic;
     public AudioSource audioSource;
    void OnEnable()
    {
        audioSource.Play();
        backMusic.gameObject.SetActive(false);
          StartCoroutine(MoveScene("GameScene", 0));

    }


    IEnumerator MoveScene(string name, int count)
   {
        async = SceneManager.LoadSceneAsync(name);
       async.allowSceneActivation = false;

       float progress = async.progress;
       while (progress < 0.9f)
       {
           Debug.Log(string.Format("btnCallback[{0}] ==> {1}%\n",count,progress * 100.0f));
           yield return null;
           progress = async.progress;
       }
       yield return null;



   }
   public void StartScene()
   {
       async.allowSceneActivation = true;
   }




}
