using UnityEngine;


public class EnemyController : MonoBehaviour
{
    #region Enemy Stats
    public Enemy _enemy;

    private float _forceUp;
    private float _forceMovement;
    private int _hialth;
    #endregion

    Rigidbody _rbEnemy;
    Vector3 _targetMovement = new Vector3(0, 0, 0); //Заменить на конткретные координаты

    [SerializeField] private ParticleSystem _partical;

    private void Start()
    {
        _rbEnemy = GetComponent<Rigidbody>();

    #region Enemy init
    _forceUp = _enemy._forceUp;
    _forceMovement = _enemy._forceMovement;
    _hialth = _enemy._health;
    #endregion
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
