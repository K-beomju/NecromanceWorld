using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Uipanel : MonoBehaviour
{
    private Text deadText;
    private RectTransform rTr;

    private void Awake()
    {
        deadText = GetComponent<Text>();
        rTr = GetComponent<RectTransform>();
    }

    public void SetPosition(Vector3 pos)
    {
        rTr.position = pos;
    }


}
