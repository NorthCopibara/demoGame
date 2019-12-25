using UnityEngine;


public class EnemyController : MonoBehaviour
{
    #region Enemy Stats
    public Enemy[]   _enemy;

    private int      _lvl;
    private float    _forceUp;
    private float    _forceMovement;
    private int      _hialth;
    private float    _scale;
    private Material _skin;
    #endregion

    private bool _stoper;

    public int _myLvl { get; private set; }

    private Rigidbody _rbEnemy;
    private Vector3 _targetMovement = new Vector3(0, 0, 0); //Заменить на конткретные координаты

    [SerializeField] private ParticleSystem _partical;

    private void Start()
    {
        _rbEnemy = GetComponent<Rigidbody>();
        EnemySetup(EnemyLvl());
    }

    private int EnemyLvl()
    {
        int _rand = Random.Range(0, 100000);

        if (_rand < 20000)
        {
            _myLvl = 0;
        }
        else
        if(_rand < 38000)
        {
            _myLvl = 1;
        }
        else
        if (_rand < 54000)
        {
            _myLvl = 2;
        }
        else
        if (_rand < 68000)
        {
            _myLvl = 3;
        }
        else
        if (_rand < 80000)
        {
            _myLvl = 4;
        }
        else
        if (_rand < 88000)
        {
            _myLvl = 5;
        }
        else
        if (_rand < 94000)
        {
            _myLvl = 6;
        }
        else
        if (_rand < 96000)
        {
            _myLvl = 7;
        }
        else
        if (_rand <= 100000)
        {
            _myLvl = 8;
        }
        return _myLvl;
    }

    public void StopDefCollision(bool stop)
    {
        _stoper = stop;
    }

    private void EnemySetup(int myLvl)
    {
        #region Enemy init
        _forceUp         = _enemy[myLvl]._forceUp;
        _forceMovement   = _enemy[myLvl]._forceMovement;
        _hialth          = _enemy[myLvl]._health;
        _skin            = _enemy[myLvl]._skin;
        _scale           = _enemy[myLvl]._scale;
        #endregion

        GetComponent<MeshRenderer>().material = _skin;
        transform.localScale = new Vector3(_scale, _scale, _scale);
    }

    private void Update() //Разваричиваемся по направлению таргета
    {
        Vector3 difference = _targetMovement - transform.position;
        difference.Normalize();
        float rotation_y = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, rotation_y, 0f);

        _rbEnemy.transform.Translate(Vector3.forward * _forceMovement * Time.deltaTime);
    }

    public void Damage() //Урон с тача
    {
        _hialth --;
        if (_hialth <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision) //Подпрыгивание
    {
        if (collision.transform.tag == "Plane")
        {
            _partical.Play();
            _rbEnemy.AddForce(Vector3.up * _forceUp * Time.deltaTime, ForceMode.Force);
        }
        if (collision.transform.tag == "Enemy")
        {
            EnemyController _noI = collision.gameObject.GetComponent<EnemyController>();

            if (!_stoper && _myLvl < _enemy.Length - 1 && _myLvl > _noI._myLvl)
            {
                _noI.StopDefCollision(true);
                _myLvl++;
                EnemySetup(_myLvl);
            }
            else
                if(_noI._myLvl != _enemy.Length - 1 && _myLvl < _noI._myLvl)
                    Destroy(this.gameObject);
        }
    }
}
