using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MenuContoller : MonoBehaviour
{
    public string gameScene = "StartScene";
    public void StartButton()
    {
        SceneManager.LoadScene(gameScene);
    }
}
