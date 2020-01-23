using UnityEngine;

public class SuperCube : MonoBehaviour
{
    Rigidbody _rbSuperEnemy;
    [SerializeField] private float _forceUp;
    //SerializeField] private ParticleSystem _partical;
    private void Start()
    {
        _rbSuperEnemy = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Plane") //Прыгаем на плейне
        {
            //_partical.Play();
            _rbSuperEnemy.AddForce(Vector3.up * _forceUp * Time.deltaTime, ForceMode.Force);
        }

        if (collision.transform.tag == "Enemy")
        {
            float _damage = collision.gameObject.GetComponent<MiniCube>().UpSupper;

            Destroy(collision.gameObject);
            transform.localScale = new Vector3(transform.localScale.x + _damage, transform.localScale.y + _damage, transform.localScale.y + _damage);

            if(transform.localScale.x >= 5)
            {
                Time.timeScale = 0; //Покак просто замерзаем в случае проигрыша
            }
        }
    }
}
