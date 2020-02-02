using UnityEngine;
using System.Collections;

public class MiniCubeTest : Character, ITakeDamage, IPoolible
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
    private bool _stopCorutine; //Костыль на остановку корутины
    private void Start()
    {
        SetAgressivBehaviour();

        EnemySetup(0);
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

    protected override void Update() 
    {
        
    }

    IEnumerator StopSpell()
    {
        yield return new WaitForSeconds(2f);
        SetAgressivBehaviour();
        _stopCorutine = false;
    }

    public void OnSpawn()
    {

    }
    public void OnDespawn()
    {

    }
}
