using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextColor : MonoBehaviour
{
    private Text text;



    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(ChangeColor());
    }
    IEnumerator ChangeColor()
    {

        text.color = new Color(170/255f,172/255f,170/255f,255/255f);
        yield return new WaitForSeconds(1.5f);
          text.color = new Color(31/255f,32/255f,31/255f,255/255f);
          yield return new WaitForSeconds(1.5f);
          StartCoroutine(ChangeColor());
    }
}
