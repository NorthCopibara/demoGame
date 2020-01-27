using UnityEngine;

public class MiniCubeBihaviourFeer : MiniCubeBihaviour
{
    public override void UpdateCube(GameObject obj)
    {
        Vector3 difference = Vector3.zero - obj.transform.position;
        difference.Normalize();
        float rotation_y = Mathf.Atan2(difference.x, difference.z) * Mathf.Rad2Deg;
        obj.transform.rotation = Quaternion.Euler(0f, rotation_y + 180, 0f);
    }
}
