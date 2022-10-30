using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public int playerKills;
    public string startScene;
    public bool gameOver = false;

    public bool gameWin = false;
    public string bossScene;
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
    private void Update()
    {
        if(playerKills > 20)
        {
            loadBossScene();
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

    public void winGame()
    {
        gameWin = true;
    }

    public void playAgain()
    {
        SceneManager.LoadScene(startScene);
    }

    public void loadBossScene()
    {
        SceneManager.LoadScene(bossScene);
    }
}
