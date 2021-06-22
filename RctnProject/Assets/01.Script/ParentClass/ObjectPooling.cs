using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling<T> where T : MonoBehaviour
{
    private Queue<T> m_queue;
    private GameObject prefab;
    private Transform parent;

    //기본 5개의 값을 가지는 오브젝트 풀을 만들어낸다.
    public ObjectPooling(GameObject prefab, Transform parent, int count = 5) // 프리팹,기본 5
    {

        this.prefab = prefab;
        this.parent = parent;
        m_queue = new Queue<T>();
        for(int i = 0; i < count; i++)
        {
        GameObject obj = GameObject.Instantiate(prefab, parent);
            T t = obj.GetComponent<T>();
            obj.SetActive(false);
            m_queue.Enqueue(t);
        }
    }

    public T GetOrCreate()
    {
        T t = m_queue.Peek(); // Queue 의 시작 부분에서 개체를 제거하지 않고 반환합니다.
        if (t.gameObject.activeSelf) //맨처음에 있는 원소조차 활성화되어있다면 큐가 전부 가용중이라는 뜻
        {
            GameObject temp = GameObject.Instantiate(prefab,parent);
            t = temp.GetComponent<T>();
           // m_queue.Enqueue(t); //개체를 Queue의 끝 부분에 추가합니다.
        }
        else
        {
            t = m_queue.Dequeue(); // Queue 의 시작 부분에서 개체를 제거하고 반환합니다.
            t.gameObject.SetActive(true);
           m_queue.Enqueue(t); //빼내었거나 새로 생성한 애를 맨 마지막에 넣는다.
        }
        return t;
    }
}


