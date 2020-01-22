using UnityEngine;

public class EnemyCharacter : Character
{
    public override int Armor { get; set; }
    public override int Health { get; set; }
    public override float UpSupper { get; set; }
    public override float Scale { get; set; }
    public override float ForceUp { get; set; }
    public override float ForceMove { get; set; }
    public override Material MaterialEnemy { get; set; }
    public override int Lvl { get; set; }

    [SerializeField] protected Enemy[] _stateEnemy;
    
    public void EnemySetup(int myLvl)
    {
        #region Enemy init
        Health = _stateEnemy[Lvl]._health;
        UpSupper = _stateEnemy[Lvl]._damade;
        Scale = _stateEnemy[Lvl]._scale;
        ForceMove = _stateEnemy[Lvl]._forceMovement;
        ForceUp = _stateEnemy[Lvl]._forceUp;
        MaterialEnemy = _stateEnemy[Lvl]._skin;
        #endregion
        Lvl = myLvl;
        GetComponent<MeshRenderer>().material = MaterialEnemy;
        transform.localScale = new Vector3(Scale, Scale, Scale);
    }
}
