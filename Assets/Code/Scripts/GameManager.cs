using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int playerKills;
    public string startScreen;
    public bool gameOver = false;
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
    }

    public void playAgain()
    {
        SceneManager.LoadScene(startScreen);
    }
}
