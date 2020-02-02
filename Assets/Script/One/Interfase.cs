using UnityEngine;

public interface ICanAttack 
{
    void Attack(Vector3 point, int damage, int countAttack, float range);
}

public interface ITakeDamage
{
    void TakeDamage(int damage);
}

public interface ICanUpSupper  //Supper это объект который вливает в себя другие объекты, в этом случае SupperCube
{
    void UpSupper();
}

public interface ITakeUpSupper 
{
    void TakeUpSupper(float up);
}