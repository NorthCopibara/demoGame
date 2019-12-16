using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _forceUp; //Enemy
    [SerializeField] private float _forceMovement; //Enemy

    private int _hialth = 100;

    Rigidbody _rbEnemy;
    public Transform _targetMovement; //Заменить на конткретные координаты

    private bool _upCondition; //Блок поворота
    RaycastHit hit;
    Ray ray;

    private void OnMouseDown()
    {
        if (Physics.Raycast(ray, out hit))
        {
            Destroy(hit.transform.gameObject);
        }
    }
    private void Start()
    {
        _rbEnemy = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!_upCondition)
        {
            Vector3 difference = _targetMovement.position - transform.position;
            difference.Normalize();
            float rotation_y = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, rotation_y, 0f);


            _rbEnemy.transform.Translate(Vector3.forward * _forceMovement * Time.deltaTime);
        }
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Plane")
        {
            _rbEnemy.AddForce(Vector3.up * _forceUp * Time.deltaTime, ForceMode.Force);
        }
    }
}
