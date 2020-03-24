using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loader : MonoBehaviour
{
    public GameObject prefab;

    public List<GameObject> obj = new List<GameObject>();
    public List<Transform> objTransform = new List<Transform>();

    private void Awake()
    {
        //ManagerPool.AddPool(PoolType.MiniCube).;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            for (int i = 0; i < 3; i++)
            {
                obj.Add(ManagerPool.Spawn(PoolType.MiniCube, prefab));
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            for (int i = 0; i < obj.Count; i++)
            {
                ManagerPool.DeSpawn(PoolType.MiniCube, obj[i]);
            }
        }
    }


}
