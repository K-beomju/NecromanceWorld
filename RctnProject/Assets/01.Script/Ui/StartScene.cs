using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
   public Animator animator;

   public float transtime = 1f;

   void Update()
   {
       if(Input.GetMouseButton(0))
       {
            LoadNextLevel();
       }
   }

   public void LoadNextLevel()
   {
       StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));

   }

   IEnumerator  LoadLevel(int levelIndex)
   {

       animator.SetTrigger("Start");
       yield return new WaitForSeconds(transtime);
        SceneManager.LoadScene(levelIndex);


   }




}
