using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monoTest : MonoBehaviour
{
    Test<int> game = new Test<int>(41);
    Test<string> game_1 = new Test<string>("csdf");
    void Start()
    {
        Debug.Log(game.game);
        Debug.Log(game_1.game);
    }
}
