using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Enemy")]
public class Enemy : ScriptableObject
{
    public float _forceMovement;
    public float _forceUp;
    public int _health;

    public float _damade;
    public Material _skin;
}
