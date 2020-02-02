using UnityEngine;

public class MiniCubeController : MonoCashed
{
    Rigidbody _rbEnemy;

    private void Start()
    {
        _rbEnemy = GetComponent<Rigidbody>();
    }

    public override void OnTick()
    {
        _rbEnemy.transform.Translate(Vector3.forward * 10 * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) //Подпрыгивание
    {
        if (collision.transform.tag == "Plane") //Прыжок
        {
            _rbEnemy.AddForce(Vector3.up * 300 * Time.deltaTime, ForceMode.Force);
        }
    }
}
