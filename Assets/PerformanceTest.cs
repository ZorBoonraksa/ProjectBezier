using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerformanceTest : MonoBehaviour
{
    [SerializeField]
    GameObject Prefab;
    [SerializeField]
    int amount = 1;
    private void Awake() {
        if(amount < 1)
        {
            amount = 1;
        }
        for(int i = 0 ; i < amount ; i++)
        {
            Instantiate(Prefab, new Vector3(0,0,0),Quaternion.identity);
        }
    }
}
