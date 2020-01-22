using UnityEngine;

public class Player : MonoBehaviour, ICanAttack
{
    RaycastHit hit;
    Ray ray;

    private float _range; //Радиус поражения
    private int _damage;
    private int _countAttack;

    SetGame _setGame;

    private void Start()
    {
        _setGame = SetGame.Instance;

        SetAttack(_setGame._setRange, _setGame._setDamage, _setGame._countAttack);
    }

    public void SetAttack(float range, int damage, int countAttack) 
    {
        _range = range;
        _damage = damage;
        _countAttack = countAttack;
    }

    private void Update() //Ловим рейкасты
    {
        #region Для андройд
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
                Attack(hit.point, _damage, _countAttack, _range);
            }
        }
        #endregion
    }

    public void Attack(Vector3 point, int damage, int countAttack, float range) 
    {
        var Coliders = Physics.OverlapSphere(point, range);
        int i = 0;

        foreach (Collider x in Coliders)
        {
            if (x.tag == "Enemy")
            {
                x.transform.GetComponent<ITakeDamage>().TakeDamage(damage);

                i++; //Счетчик количества энеми под атакай за 1 каст
                if(i >= countAttack)
                    break;
            }
        }
        
    }
}
