using UnityEngine;
using System.Collections;

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

    SpellManager _spell;

    private MiniCubeBihaviour _bihaviour;
    private Rigidbody _rbEnemy;
    private bool _stoper; //Кастыль слияния противников (блрчит удаление второго противника при соприкосновении)
    private bool _stopCorutine; //Костыль на остановку корутины
    private void Start()
    {
        SetAgressivBehaviour();

        _spell = FindObjectOfType<SpellManager>();
        _spell._spellCust += SetBehaviour;
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

    public void SetBehaviour(int behaviour)
    {
        switch (behaviour)
        {
            case 1:
                SetFeerBehaviour();
                break;
            case 2:
                SetRandomBehaviour();
                break;
            case 3:
                break;
        }
    }

    private void SetRandomBehaviour()
    {
        if (!_stopCorutine)
        {
            _bihaviour = new MiniCubeBihaviourRandom();
            _bihaviour.StartCube(this.gameObject);
            StartCoroutine(StopSpell());
            _stopCorutine = true;
        }
    }

    private void SetFeerBehaviour()
    {
        if (!_stopCorutine)
        {
            _bihaviour = new MiniCubeBihaviourFeer();
            _bihaviour.StartCube(this.gameObject);
            StartCoroutine(StopSpell());
            _stopCorutine = true;
        }
    }

    private void SetAgressivBehaviour()
    {
        _bihaviour = new MiniCubeBihaviourAgressiv();
        _bihaviour.StartCube(this.gameObject);
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
        _bihaviour.UpdateCube();

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

    IEnumerator StopSpell()
    {
        yield return new WaitForSeconds(2f);
        SetAgressivBehaviour();
        _stopCorutine = false;
    }

    private void OnDestroy()
    {
        _spell._spellCust -= SetBehaviour;
    }
}
