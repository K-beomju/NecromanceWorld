using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class Uipanel : MonoBehaviour
{
    private Text deadText;
    private RectTransform rTr;
    public Ease ease;
    public int delay;

    private void Awake()
    {
        deadText = GetComponent<Text>();
        rTr = GetComponent<RectTransform>();
    }

    void Start()
    {
        transform.DOMoveY(transform.position.y + 0.5f,delay).SetEase(ease).SetLoops(-1,LoopType.Yoyo);
    }

    public void SetPosition(Vector3 pos)
    {
        rTr.position = pos;
    }


}
