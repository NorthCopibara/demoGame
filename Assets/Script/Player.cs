using UnityEngine;

public class Player : MonoBehaviour, IAttack
{
    RaycastHit hit;
    Ray ray;

    private float _range; //Радиус поражения
    private int _damage;
    private bool _aoe;

    SetGame _setGame;

    private void Start()
    {
        _setGame = SetGame.Instance;

        SetAttack(_setGame._setRange, _setGame._setDamage, _setGame._aoe);
    }

    public void SetAttack(float range, int damage, bool aoe) 
    {
        _aoe = aoe;
        _range = range;
        _damage = damage;
    }

    private void Update() //Ловим рейкасты
    {
        #region Несколько тачей
        /*foreach (var r in Input.touches)
        {
            if (r.phase == TouchPhase.Began)
            {
                ray = Camera.main.ScreenPointToRay(r.position);
                if (Physics.Raycast(ray, out hit))
                {
                    var Coliders = Physics.OverlapSphere(hit.point, _range);

                    foreach (var x in Coliders)
                    {
                        if (x.tag == "Enemy")
                        {
                            x.transform.GetComponent<EnemyController>().Damage();
                            break;
                        }
                    }
                }
            }
        }*/
        #endregion

        #region Один тач
        if (Input.GetMouseButtonDown(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                var Coliders = Physics.OverlapSphere(hit.point, _range);

                foreach (Collider x in Coliders)
                {
                    if (x.tag == "Enemy")
                    {
                        Attack(x, _damage);

                        if(!_aoe) //Если массовый урон отключен
                            break;
                    }
                }
            }
        }
        #endregion
    }

    public void Attack(Collider character, int damage) 
    {
        character.transform.GetComponent<IDamage>().Damage(damage);
    }
}
