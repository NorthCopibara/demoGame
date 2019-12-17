using UnityEngine;


public class EnemyController : MonoBehaviour
{
    public LayerMask ground;

    #region Enemy Stats
    [SerializeField] private Enemy _enemy;
    private float _forceUp;
    private float _forceMovement;
    private int _hialth;
    #endregion

    Rigidbody _rbEnemy;
    Vector3 _targetMovement = new Vector3(0, 0, 0); //Заменить на конткретные координаты

    private bool _upCondition; //Блок поворота
    RaycastHit hit;
    Ray ray;
    bool dam = false;

    private void Start()
    {
        _rbEnemy = GetComponent<Rigidbody>();

    #region Enemy init
    _forceUp = _enemy._forceUp;
    _forceMovement = _enemy._forceMovement;
    _hialth = _enemy._health;
    #endregion
    }

private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!_upCondition)
        {
            Vector3 difference = _targetMovement - transform.position;
            difference.Normalize();
            float rotation_y = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, rotation_y, 0f);


            _rbEnemy.transform.Translate(Vector3.forward * _forceMovement * Time.deltaTime);
        }
    }

   /* private void OnMouseDown()
    {
        if (Physics.Raycast(ray, out hit, ground))
        {
            Debug.Log(hit.point);
            dam = true;
            var Coliders = Physics.OverlapSphere(hit.point, 10.0f);
            


            foreach (var x in Coliders)
            {
                if(x.tag == "Enemy")
                {
                    Damage();
                    //break;
                }
            }

            dam = false;
        }
    }*/

    public void Damage()
    {
        _hialth -= 50;
        if (_hialth <= 0)
            Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Plane")
        {
            _rbEnemy.AddForce(Vector3.up * _forceUp * Time.deltaTime, ForceMode.Force);
        }

        
    }

    
}
