using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _forceUp;
    [SerializeField] private float _forceMovement;
    Rigidbody _rbEnemy;

    public Transform _targetMovement;

    private bool _upCondition;

    private void Start()
    {
        _rbEnemy = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, _targetMovement.position, _forceMovement * Time.deltaTime);

        // Тот самый поворот
        // вычисляем разницу между текущим положением и положением мыши
        Vector3 difference = _targetMovement.position - transform.position;
        difference.Normalize();
        // вычисляемый необходимый угол поворота
        float rotation_y = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        // Применяем поворот вокруг оси Z
        transform.rotation = Quaternion.Euler(0f, rotation_y, 0f);

        if (!_upCondition)
        {
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
