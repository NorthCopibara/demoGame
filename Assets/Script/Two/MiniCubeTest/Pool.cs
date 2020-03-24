using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Pool 
{
    private Transform _parentPool;

    private Dictionary<int, Stack<GameObject>> cachedObj = new Dictionary<int, Stack<GameObject>>();
    private Dictionary<int, int> cachedIds = new Dictionary<int, int>();

    public void SetParant(Transform parant)
    {
        _parentPool = parant;
    }

    /*
     * Суть идеи:
     * Мы смотрим на айди префаба и по его подобию создаем объекты на сцене, но у них другие id
     * Поэтому мы берем и создаем словарь с ключем в виде id префаба и значением id созданного объекта, что позволяет различать объекты по их принадлежности
    */
    public GameObject Spawn(GameObject prefab, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Transform parant = null) //Метод спавна объектов
    {
        int key = prefab.GetInstanceID(); //Добавление в словарь объектов
        Stack<GameObject> stack;
        bool stacked = cachedObj.TryGetValue(key, out stack); //Есть ли еже стек?

        if (stacked && stack.Count > 0) //Если стек есть, то можно вытаскивать из него
        {
            Transform transform = stack.Pop().transform; //pop - берет верхнее значение из контейнера, выдает нам и удаляет его
            transform.SetParent(parant);
            transform.rotation = rotation;
            transform.gameObject.SetActive(true);
            if (parant) transform.position = position;
            else transform.localPosition = position;
            IPoolible poolable = transform.GetComponent<IPoolible>();
            if (poolable != null) poolable.OnSpawn();
            return transform.gameObject;
        }

        if (!stacked) cachedObj.Add(key, new Stack<GameObject>()); //Если стека нет, то создаем его

        GameObject createdPrefab = Populate(prefab, position, rotation, parant);
        cachedIds.Add(createdPrefab.GetInstanceID(), key);

        return createdPrefab;
    }

    public void Despawn(GameObject go)
    {
        go.SetActive(false);
        cachedObj[cachedIds[go.GetInstanceID()]].Push(go); //После выключение объекта забразываем его в стек

        IPoolible poolable = go.GetComponent<IPoolible>();
        if (poolable != null) poolable.OnDespawn();

        if (_parentPool != null) go.transform.SetParent(_parentPool);
    }

    public void Dispose() //Используется для полного уничтожения пулов
    {
        _parentPool = null;
        cachedObj.Clear();
        cachedIds.Clear();
    }

    private GameObject Populate(GameObject prefab, Vector3 position = default(Vector3), Quaternion rotation = default(Quaternion), Transform parant = null)
    {
        Transform go = Object.Instantiate(prefab, position, rotation, parant).transform;

        if (parant) go.position = position;
        else go.localPosition = position;

        return go.gameObject;
    }


}
