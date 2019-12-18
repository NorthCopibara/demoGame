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
        if (collision.transform.tag == "Plane") //Прыгаем на плейне
        {
            _rbSuperEnemy.AddForce(Vector3.up * _forceUp * Time.deltaTime, ForceMode.Force);
        }

        if (collision.transform.tag == "Enemy")
        {
            float _damage = collision.gameObject.GetComponent<EnemyController>()._enemy._damade;
            Destroy(collision.gameObject);
            transform.localScale = new Vector3(transform.localScale.x + _damage, transform.localScale.y + _damage, transform.localScale.y + _damage);

            if(transform.localScale.x >= 5)
            {
                Time.timeScale = 0;
            }
        }
    }
}
