using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperEnemy : MonoBehaviour
{
    Rigidbody _rbSuperEnemy;
    [SerializeField] private float _forceUp;

    private void Start()
    {
        _rbSuperEnemy = GetComponent<Rigidbody>();

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Plane")
        {
            _rbSuperEnemy.AddForce(Vector3.up * _forceUp * Time.deltaTime, ForceMode.Force);
        }

        if (collision.transform.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            transform.localScale = new Vector3(transform.localScale.x + 0.1f, transform.localScale.y + 0.1f, transform.localScale.y + 0.1f);
        }
    }
}
