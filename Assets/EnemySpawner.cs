using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject _enemy_obj;
    [SerializeField] private Transform _enemy_parant;

    private Vector3 _sapwn_position;

    private bool _stop_spawn;

    private void Update()
    {
        if (!_stop_spawn)
        {
            _sapwn_position = new Vector3(Random.Range(-40.0f, 40.0f), Random.Range(5.0f, 25.0f), 35.0f);
            StartCoroutine(Spawner());
            _stop_spawn = true;
        }

    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(0.1f);
        Instantiate(_enemy_obj, _sapwn_position, Quaternion.identity, _enemy_parant);
        _stop_spawn = false;
    }
}
