using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public CameraEffect camEffect;

    void Awake()
    {
        instance = this;
    }

     public static void CamShake(float intense, float during)
    {
        instance.camEffect.SetShake(intense, during);
    }
}
