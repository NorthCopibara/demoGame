using UnityEngine;
public abstract class MiniCubeBihaviour:  MonoBehaviour
{
    protected Vector3 _targetMovement = new Vector3(0, 0, 0);
    public abstract void UpdateCube(GameObject obj);
}
