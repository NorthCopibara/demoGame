using UnityEngine;

public class MiniCubeBihaviourRandom : MiniCubeBihaviour
{
    private bool _storRotate;

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
        if (!_storRotate)
        {
            _storRotate = true;
            obj.transform.rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
        }
    }
}
