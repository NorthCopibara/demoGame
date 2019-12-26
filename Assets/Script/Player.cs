using UnityEngine;

public class Player : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    private float range = 3.0f; //Радиус поражения

    private void Update() //Ловим рейкасты
    {

        foreach (var r in Input.touches)
        {
            if (r.phase == TouchPhase.Began)
            {
                ray = Camera.main.ScreenPointToRay(r.position);
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
    }
    

    /*private void OnDrawGizmos() //Для видимости в инспекторе
    {
        if (Input.GetMouseButton(0))
            Gizmos.DrawSphere(hit.point, range);
    }*/
}
