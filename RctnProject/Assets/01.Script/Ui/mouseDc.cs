using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseDc : MonoBehaviour
{

    void Update()
    {
          var heading =   GameManager.instance.crossHair.transform.position - GameManager.instance.crossTxt.position;
        if(heading.sqrMagnitude > 0.5f)
        {
        GameManager.instance.crossTxt.transform.position = Vector3.Lerp(  GameManager.instance.crossTxt.transform.position,
         new Vector3(GameManager.instance.crossHair.transform.position.x,GameManager.instance.crossHair.transform.position.y - 1,
         GameManager.instance.crossHair.transform.position.z), Time.deltaTime * 3f);

        }
    }
}
