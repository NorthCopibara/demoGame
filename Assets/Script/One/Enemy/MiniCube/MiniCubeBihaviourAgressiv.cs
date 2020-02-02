using UnityEngine;

public class MiniCubeBihaviourAgressiv : MiniCubeBihaviour
{
    GameObject obj;
    Rigidbody _rb;
    public override void StartCube(GameObject _obj)
    {
        obj = _obj;
        _rb = obj.GetComponent<Rigidbody>();
    }
    public override void UpdateCube()
    {
        Vector3 difference = Vector3.zero - obj.transform.position;
        difference.Normalize();
        float rotation_y = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.Euler(0f, rotation_y, 0f);
    }
}
