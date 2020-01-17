using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Patterns;

public class SetGame : Singleton<SetGame>
{
    public Lvl _setLvl { get; private set; }

    public int _setDamage = 1;
    public float _setRange = 3;
    public int _countAttack = 1;

    public Spell _spell;

    public enum Spell
    {
        Feer,
        Randomness,
        Stoper
    }

    public void SetGameLvl(Lvl lvl) 
    {
        _setLvl = lvl;
        _spell = Spell.Randomness;
    }
}
