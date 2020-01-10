using UnityEngine;

[CreateAssetMenu(fileName = "New Lvl", menuName = "Lvl")]
public class Lvl : ScriptableObject
{
    [Header("Вероятности появления разных уровней противников")]
    public int[] _randomEnemy;

    [Header("Минимальное количество противников в волне")] //каждую волну прибовляется по 3 противника
    public int _enemyCount;
}
