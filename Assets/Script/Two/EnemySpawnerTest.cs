using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTest : MonoBehaviour
{
    [Header("Объекты противников")]
    [SerializeField] private GameObject _enemy_obj;
    
    [Space(5)]
    [Header("Transform родителя")]
    [SerializeField] private Transform _enemy_parant;
    
    [Header("Количиство enemy")]
    [SerializeField] private int _enemyCountTest;

    private void Start()
    {
        for (int i = 0; i < _enemyCountTest; i++)
        {
            for (int j = 0; j < _enemyCountTest; j++)
            {
                Instantiate(_enemy_obj, new Vector3(i * 2, 5, j * 2), Quaternion.identity, _enemy_parant);
            }
        }
    }

   
}

