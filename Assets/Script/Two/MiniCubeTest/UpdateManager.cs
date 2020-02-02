using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    private void Update()
    {
        for (var j = 0; j < MonoCashed.allTicks.Count; j++)
        {
            MonoCashed.allTicks[j].Tick();
        }
    }

    private void FixedUpdate()
    {
        for (var j = 0; j < MonoCashed.allFixedTicks.Count; j++)
        {
            MonoCashed.allFixedTicks[j].FixedTick();
        }
    }
}
