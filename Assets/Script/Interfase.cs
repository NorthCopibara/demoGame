using UnityEngine;

public interface IAttack 
{
    void Attack(Vector3 point, int damage, int countAttack, float range);
}

public interface IDamage
{
    void Damage(int damage);
}