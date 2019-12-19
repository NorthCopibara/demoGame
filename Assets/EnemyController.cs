using UnityEngine;


public class EnemyController : MonoBehaviour
{
    #region Enemy Stats
    public Enemy[] _enemy;

    private int      _lvl;
    private float    _forceUp;
    private float    _forceMovement;
    private int      _hialth;
    private float    _scale;
    private Material _skin;
    #endregion

    public int _myLvl { get; private set;}

    Rigidbody _rbEnemy;
    Vector3 _targetMovement = new Vector3(0, 0, 0); //Заменить на конткретные координаты

    [SerializeField] private ParticleSystem _partical;

    private void Start()
    {
        _rbEnemy = GetComponent<Rigidbody>();

         _myLvl = Random.Range(0, _enemy.Length); //Генерация уровня противника
        //_myLvl = 1;
        EnemySetup(_myLvl);
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
    }
}
