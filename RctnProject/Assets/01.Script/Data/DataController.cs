using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

[Serializable]
public struct GroupData
{
    public int mob01count;
    public int mob02count;
    public int mob03count;
    public int mob04count;
    public int mob05count;
    public int mob06count;
}


public class DataController : MonoBehaviour
{
      public GroupData groupData;
    private LivingPlayer living;
    public PlayerGroup playerGroup;


    //[ContextMenu("To Json Data")]
    public void SaveGroupDataToJson()
    {
        string jsonData = JsonUtility.ToJson(groupData, true);
        string path = Path.Combine(Application.dataPath, "groupData.json");
        File.WriteAllText(path, jsonData);

    }

   // [ContextMenu("From Json Data")]
   public void LoadGroupDataToJson()
    {
        string path = Path.Combine(Application.dataPath, "groupData.json");
        string jsonData = File.ReadAllText(path);
        groupData = JsonUtility.FromJson<GroupData>(jsonData);
    }

     void OnApplicationQuit()
    {
        for (int i = 0; i < playerGroup.transform.childCount; i++)
        {

            groupData.mob01count =  playerGroup.transform.childCount;

        }
        SaveGroupDataToJson();
    }


    void Start()
    {
       LoadGroupDataToJson();

    }





}
