using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pool : MonoBehaviour
{

    public GameObject[] items;

    public GameObject GetPooledObject()
    {
        for(int i = 0; i < items.Length; i++)
        {
            if (!items[i].activeInHierarchy)
                return items[i];
        }
        return null;
    }
}
