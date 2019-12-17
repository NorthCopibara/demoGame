using UnityEngine;

public class Player : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    private float range = 3.0f;

    private void Update()
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


    private void OnMouseDown()
    {
        /*if (Physics.Raycast(ray, out hit))
        {
            var Coliders = Physics.OverlapSphere(hit.point, range);

            foreach (var x in Coliders)
            {
                if (x.tag == "Enemy")
                {
                    x.transform.GetComponent<EnemyController>().Damage();
                    //break;
                }
            }
        }*/
        /*if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.GetComponent<EnemyController>().Damage();
            }

        }*/
    }

    private void OnDrawGizmos()
    {
        if (Input.GetMouseButton(0))
            Gizmos.DrawSphere(hit.point, range);
    }
}
