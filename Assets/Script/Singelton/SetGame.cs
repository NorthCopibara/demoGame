﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;

public class SetGame : Singleton<SetGame>
{
    public Lvl _setLvl { get; private set; }

    public int _setDamage = 2;
    public float _setRange = 3;
    public bool _aoe = false;


    public void SetGameLvl(Lvl lvl) 
    {
        _setLvl = lvl;
    }
}