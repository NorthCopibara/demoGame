using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Объекты противников")]
    [SerializeField] private GameObject[] _enemy_obj;

    [Space(5)]
    [Header("Трансформ родителя")]
    [SerializeField] private Transform _enemy_parant;

    [Space(5)]
    [Header("Время между спавном противников")]
    [SerializeField] private float _dTimeSpawn;

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
        yield return new WaitForSeconds(_dTimeSpawn);
        Instantiate(_enemy_obj[Random.Range(0, _enemy_obj.Length)], _sapwn_position, Quaternion.identity, _enemy_parant);
        _stop_spawn = false;
    }
}
