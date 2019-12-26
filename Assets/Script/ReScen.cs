using UnityEngine.SceneManagement;
using UnityEngine;

public class ReScen : MonoBehaviour
{
    public void NewGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
