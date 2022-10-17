using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MenuContoller : MonoBehaviour
{
    public string gameScene = "StartScene";
    public RectTransform instructions;
    public RectTransform credits;
    public RectTransform back;
    public GameObject buttons;
    public RectTransform Title;
    public GameObject objects;
    public GameObject playerCharacter;
    public GameObject Ak47;
    public GameObject Flamethrower;
    private void Awake() 
    {
        instructions.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
    }
        
    public void StartButton()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void InstructionButton()
    {
        instructions.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
        buttons.SetActive(false);
        Title.gameObject.SetActive(false);
        playerCharacter.SetActive(false);
        Ak47.SetActive(false);
        Flamethrower.SetActive(false);
    }

    public void CreditButton()
    {
        credits.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
        buttons.SetActive(false);
        Title.gameObject.SetActive(false);
        playerCharacter.SetActive(false);
        Ak47.SetActive(false);
        Flamethrower.SetActive(false);
    }

    public void BackButton()
    {
        credits.gameObject.SetActive(false);
        instructions.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
        buttons.SetActive(true);
        Title.gameObject.SetActive(true);
        playerCharacter.SetActive(true);
        Ak47.SetActive(true);
        Flamethrower.SetActive(true);
    }
}
