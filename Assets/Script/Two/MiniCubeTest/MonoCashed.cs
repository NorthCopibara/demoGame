using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonoCashed : MonoBehaviour
{
    public static List<MonoCashed> allTicks = new List<MonoCashed>();
    public static List<MonoCashed> allFixedTicks = new List<MonoCashed>();

    private void OnEnable()
    {
        allTicks.Add(this);
    }

    private void OnDisable()
    {
        allTicks.Remove(this);
    }

    public void Tick()
    {
        OnTick();
    }

    public void FixedTick()
    {

    }

    public virtual void OnTick()
    {

    }
}
