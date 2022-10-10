using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private int playerKills;
    public string gameOverScene;
    public string startScreen;
    private bool gameOver = false;
    private void Awake() {

        if(Instance == null)
        {
            Instance = this;
        }

        if(Instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void updateKills()
    {
        playerKills++;
    }

    public void endGame()
    {
        gameOver = true;
        SceneManager.LoadScene(gameOverScene);
    }

    public void playAgain()
    {
        playerKills = 0;
        gameOver = false;
        SceneManager.LoadScene(startScreen);
    }
}
