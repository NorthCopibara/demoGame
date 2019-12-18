using UnityEngine;

public class Player : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    private float range = 3.0f; //Радиус поражения

    private void Update() //Ловим рейкасты
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                var Coliders = Physics.OverlapSphere(hit.point, range);

                foreach (var x in Coliders)
                {
                    if (x.tag == "Enemy")
                    {
                        x.transform.GetComponent<EnemyController>().Damage();
                        break;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos() //Для видимости в инспекторе
    {
        if (Input.GetMouseButton(0))
            Gizmos.DrawSphere(hit.point, range);
    }
}
