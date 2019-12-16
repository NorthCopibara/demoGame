using UnityEngine;

public class Player : MonoBehaviour
{
    RaycastHit hit;
    Ray ray;

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        if (Physics.Raycast(ray, out hit))
        {
            Destroy(hit.transform.gameObject);
        }
    }
}
