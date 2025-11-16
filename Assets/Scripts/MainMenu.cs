using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void LoadNewGame()
    {
        SceneManager.LoadScene(1); // Load Day 1
    }
}
