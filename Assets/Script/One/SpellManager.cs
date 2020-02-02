using UnityEngine;

public class SpellManager : MonoBehaviour
{
    public delegate void SpellCust(int state);
    public SpellCust _spellCust;

    SetGame _game;

    private void Start()
    {
        _game = SetGame.Instance;
    }

    public void Cust() 
    {
        switch (_game._spell) 
        {
            case SetGame.Spell.Feer:
                _spellCust?.Invoke(1);
                break;
            case SetGame.Spell.Randomness:
                _spellCust?.Invoke(2);
                break;
            case SetGame.Spell.Stoper:
                _spellCust?.Invoke(3);
                break;
        }
    }
}
