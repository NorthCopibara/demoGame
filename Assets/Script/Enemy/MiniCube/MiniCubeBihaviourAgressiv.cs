using UnityEngine;

public class MiniCubeBihaviourAgressiv : MiniCubeBihaviour
{
    public override void UpdateCube(GameObject obj)
    {
        Vector3 difference = _targetMovement - obj.transform.position;
        difference.Normalize();
        float rotation_y = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.Euler(0f, rotation_y, 0f);
    }
}
