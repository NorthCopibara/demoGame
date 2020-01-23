using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    SetGame _game;

    [Header("Объекты противников")]
    [SerializeField] private GameObject _enemy_obj;
    [Header("Время между спавном противников")]
    [SerializeField] private float _dTimeSpawn;
    

    [Space(5)]
    [Header("Transform родителя")]
    [SerializeField] private Transform _enemy_parant;
    

    private int _enemyCount;

    [Header("Количиство волн")]
    [SerializeField] private int _vaweCount;
    [Header("Время между волнами")]
    [SerializeField] private float _dTimeVawe;

    GenerateLvlEnemy _generateLvlEnemy = new GenerateLvlEnemy();
    private Vector3 _sapwn_position;

    private bool _stop_spawn;
    private int _enemyNumber;
    private int _vaweNumber;
    private bool _stopVawe;

    private void Awake()
    {
        #region SetLvlSpawn
        _game = SetGame.Instance;
        _enemyCount = _game._setLvl._enemyCount;
        _generateLvlEnemy.SetRandom(_game._setLvl._randomEnemy);
        #endregion
    }

    private void Update()
    {
        Vawe(_enemyCount); //Вызываем волну отдавая туда количество противников в волне
    }

    public void Vawe(int count) 
    {
        if (_enemyNumber < count)
        {
            if (!_stop_spawn)
            {
                _sapwn_position = new Vector3(Random.Range(-30.0f, 30.0f), Random.Range(5.0f, 25.0f), 40.0f);
                StartCoroutine(Spawner());

                _enemyNumber++;
                _stop_spawn = true;
            }
        }
        else 
        {
            if (_vaweNumber < _vaweCount && !_stopVawe)
            {
                StartCoroutine(NextWave());
                _vaweNumber++;
                _stopVawe = true;
            }
        }
    }

    IEnumerator Spawner()
    {
        yield return new WaitForSeconds(_dTimeSpawn);
        GameObject enemy = Instantiate(_enemy_obj, _sapwn_position, Quaternion.identity, _enemy_parant);

        enemy.GetComponent<MiniCube>().EnemySetup(_generateLvlEnemy.EnemyLvl());

        _stop_spawn = false;
    }

    IEnumerator NextWave() //Запускается отсчет до следующей волны после окончавния предыдущей
    {
        yield return new WaitForSeconds(_dTimeVawe);

        //Перезапуск полны
        _enemyNumber = 0;
        _stop_spawn = false;

        //Разрешаем снова вызвать следующую волну
        _stopVawe = false;
    }
}

public class GenerateLvlEnemy 
{
    List<int> _setRandom = new List<int>();

    public void SetRandom(int[] setRandom) 
    {
        for (int i = 0; i < setRandom.Length; i++) 
        {
            _setRandom.Add(setRandom[i]);
        }
    }

    public int EnemyLvl()
    {
        int _rand = Random.Range(0, 100000);

        if (_rand < _setRandom[0] * 1000)
        {
            return 0;
        }
        else
        if (_rand < _setRandom[1] * 1000)
        {
            return 1;
        }
        else
        if (_rand < _setRandom[2] * 1000)
        {
            return 2;
        }
        else
        if (_rand < _setRandom[3] * 1000)
        {
            return 3;
        }
        else
        if (_rand < _setRandom[4] * 1000)
        {
            return 4;
        }
        else
        if (_rand < _setRandom[5] * 1000)
        {
            return 5;
        }
        else
        if (_rand < _setRandom[6] * 1000)
        {
            return 6;
        }
        else
        if (_rand < _setRandom[7] * 1000)
        {
            return 7;
        }
        else
        if (_rand <= _setRandom[8] * 1000)
        {
            return 8;
        }
        else
            return 0;
    }
}
