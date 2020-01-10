using UnityEngine;

public interface IAttack 
{
    void Attack(Collider character, int damage);
}

public interface IDamage
{
    void Damage(int damage);
}