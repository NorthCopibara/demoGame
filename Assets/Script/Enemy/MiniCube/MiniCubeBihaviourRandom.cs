using UnityEngine;

public class MiniCubeBihaviourRandom : MiniCubeBihaviour
{
    private bool _storRotate;

    public override void UpdateCube(GameObject obj)
    {
        Vector3 difference = _targetMovement - obj.transform.position;
        difference.Normalize();
        if (!_storRotate)
        {
            _storRotate = true;
            obj.transform.rotation = Quaternion.Euler(0f, Random.Range(0, 360), 0f);
        }
    }
}
