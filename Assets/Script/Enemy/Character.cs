using UnityEngine;

public abstract class Character: MonoBehaviour
{
    public abstract int Lvl { get; set; }
    public abstract int Armor { get; set; }
    public abstract int Health { get; set; }
    public abstract float UpSupper { get; set; }
    public abstract float Scale { get; set; }
    public abstract float ForceUp { get; set; }
    public abstract float ForceMove { get; set; }
    public abstract Material MaterialEnemy { get; set; }

    protected abstract void Update();
}
