﻿using UnityEngine;

public class MiniCube : Character, ITakeDamage
{
    public override int Armor { get; set; }
    public override int Health { get; set; }
    public override float UpSupper { get; set; }
    public override float Scale { get; set; }
    public override float ForceUp { get; set; }
    public override float ForceMove { get; set; }
    public override Material MaterialEnemy { get; set; }
    public override int Lvl { get; set; }

    [SerializeField] private Enemy[] _stateEnemy;

    private MiniCubeBihaviour _bihaviour;
    private Rigidbody _rbEnemy;
    private bool _stoper; //Кастыль слияния противников (блрчит удаление второго противника при соприкосновении)
    private void Start()
    {
        _bihaviour = new MiniCubeBihaviourAgressiv();
       // _bihaviour = new MiniCubeBihaviourRandom();
       //_bihaviour = new MiniCubeBihaviourFeer();
    }

    public void EnemySetup(int myLvl)
    {
        #region Enemy init
        Health = _stateEnemy[myLvl]._health;
        UpSupper = _stateEnemy[myLvl]._damade;
        Scale = _stateEnemy[myLvl]._scale;
        ForceMove = _stateEnemy[myLvl]._forceMovement;
        ForceUp = _stateEnemy[myLvl]._forceUp;
        MaterialEnemy = _stateEnemy[myLvl]._skin;
        Lvl = myLvl;
        #endregion

        _rbEnemy = GetComponent<Rigidbody>();

        GetComponent<MeshRenderer>().material = MaterialEnemy;
        transform.localScale = new Vector3(Scale, Scale, Scale);
    }

    public void TakeDamage(int damage) //Урон с тача
    {
        Health -= damage;
        if (Health <= 0)
            Destroy(gameObject);
        else
            EnemySetup(Health - 1);
    }

    public void StopDefCollision(bool stop)
    {
        _stoper = stop;
    }

    protected override void Update() 
    {
        _bihaviour.UpdateCube(this.gameObject);

        if (!_stoper)
            _rbEnemy.transform.Translate(Vector3.forward * ForceMove * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision) //Подпрыгивание
    {
        if (collision.transform.tag == "Plane") //Прыжок
        {
            //_partical.Play();
            _rbEnemy.AddForce(Vector3.up * ForceUp * Time.deltaTime, ForceMode.Force);
        }

        if (collision.transform.tag == "Enemy") //Коллизия с другим проивником
        {
            MiniCube _noI = collision.gameObject.GetComponent<MiniCube>();

            if (Lvl < 10 - 1 && Lvl > _noI.Lvl)
            {
                _noI.StopDefCollision(true); //Один из enemy повышает свой уровень
                Lvl++;
                EnemySetup(Lvl);
            }
            else
                if (_noI.Lvl != 10 - 1 && Lvl < _noI.Lvl)
                Destroy(this.gameObject); //Друго уничтожается
        }
    }
}
