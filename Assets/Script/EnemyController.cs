using System.Collections;
using UnityEngine;


public class EnemyController : MonoBehaviour, IDamage
{
    #region Enemy Stats
    public Enemy[]   _enemy;

    private float    _forceUp;
    private float    _forceMovement;
    private int      _hialth;
    private float    _scale;
    private Material _skin;
    #endregion

    private bool _stoper; //Кастыль слияния противников (блрчит удаление второго противника при соприкосновении)

    public int _myLvl { get; private set; }

    private Rigidbody _rbEnemy;
    private Vector3 _targetMovement = new Vector3(0, 0, 0); //Заменить на конткретные координаты

    [SerializeField] private ParticleSystem _partical;

    #region
    private int _state = 0;
    private bool _storRotate;
    private bool _soper;

    private SpellManager _spell;
    #endregion



    private void Start()
    {
        _rbEnemy = GetComponent<Rigidbody>();

        _spell = FindObjectOfType<SpellManager>();
        _spell._spellCust += SetState;
    }

    private void SetState(int state) 
    {
        _state = state;
    }

    public void EnemySetup(int myLvl)
    {
        #region Enemy init
        _forceUp         = _enemy[myLvl]._forceUp;
        _forceMovement   = _enemy[myLvl]._forceMovement;
        _hialth          = _enemy[myLvl]._health;
        _skin            = _enemy[myLvl]._skin;
        _scale           = _enemy[myLvl]._scale;
        #endregion
        _myLvl = myLvl;
        GetComponent<MeshRenderer>().material = _skin;
        transform.localScale = new Vector3(_scale, _scale, _scale);
    }

    private void Update() //Разваричиваемся по направлению таргета
    {
        switch (_state)
        {
            case 0:
                NormalRotate();
                break;
            case 1: //Фир
                FeerRotate();
                break;
            case 2: //Хаотичность
                RandomRotate();
                break;
            case 3: //Заморозка
                StopMove();
                break;
        }

        if(!_stoper)
            _rbEnemy.transform.Translate(Vector3.forward * _forceMovement * Time.deltaTime);
    }


#region Spells
    private void NormalRotate() 
    {
        Vector3 difference = _targetMovement - transform.position;
        difference.Normalize();
        float rotation_y = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, rotation_y, 0f);
    }

    private void FeerRotate()
    {
        Vector3 difference = _targetMovement - transform.position;
        difference.Normalize();
        float rotation_y = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, rotation_y + 180, 0f);
        if (!_storRotate)
        {
            StartCoroutine(SpellTime(2f));
            _storRotate = true;
        }
    }

    private void RandomRotate()
    {
        Vector3 difference = _targetMovement - transform.position;
        difference.Normalize();
        if (!_storRotate)
        {
            transform.rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
            StartCoroutine(SpellTime(2f));
            _storRotate = true;
        }
    }

    private void StopMove()
    {
        if (!_storRotate)
        {
            StartCoroutine(SpellTime(2f));
            _stoper = true;
            _storRotate = true;
        }
    }
#endregion


    public void Damage(int damage) //Урон с тача
    {
        _hialth -= damage;
        if (_hialth <= 0)
            Destroy(gameObject);
        else
            EnemySetup(_hialth - 1);
    }
    
    public void StopDefCollision(bool stop)
    {
        _stoper = stop;
    }

    private void OnCollisionEnter(Collision collision) //Подпрыгивание
    {
        if (collision.transform.tag == "Plane")
        {
            //_partical.Play();
            _rbEnemy.AddForce(Vector3.up * _forceUp * Time.deltaTime, ForceMode.Force);
        }
        if (collision.transform.tag == "Enemy")
        {
            EnemyController _noI = collision.gameObject.GetComponent<EnemyController>();

            if (_myLvl < _enemy.Length - 1 && _myLvl > _noI._myLvl)
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

    IEnumerator SpellTime(float timeSpell)
    {
        yield return new WaitForSeconds(timeSpell);
        _state = 0;
        _stoper = false;
        _storRotate = false;
    }

    private void OnDestroy()
    {
        _spell._spellCust -= SetState;
    }
}
