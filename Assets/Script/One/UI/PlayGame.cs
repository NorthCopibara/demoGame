using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayGame : MonoBehaviour
{
    public Lvl lvl;
    SetGame _game;

    private void Awake()
    {
        _game = SetGame.Instance;
    }

    public void Play()
    {
        _game.SetGameLvl(lvl);
        SceneManager.LoadScene("Game");

    }
}
