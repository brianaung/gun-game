using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class MenuContoller : MonoBehaviour
{
    public string gameScene = "StartScene";
    public RectTransform credits;
    public RectTransform back;
    public GameObject buttons;
    public RectTransform Title;
    public RectTransform controls;
    public GameObject playerCharacter;
    public GameObject Ak47;
    public GameObject Flamethrower;
    [SerializeField] AudioSource buttonSound;

    private void Awake() 
    {
        controls.gameObject.SetActive(false);
        credits.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
    }
        
    public void StartButton()
    {
        SceneManager.LoadScene(gameScene);
    }

    public void InstructionButton()
    {
        controls.gameObject.SetActive(true);
        back.gameObject.SetActive(true);
        buttons.SetActive(false);
        Title.gameObject.SetActive(false);
        playerCharacter.SetActive(false);
        Ak47.SetActive(false);
        Flamethrower.SetActive(false);
        buttonSound.Play();
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
        buttonSound.Play();
    }

    public void BackButton()
    {
        credits.gameObject.SetActive(false);
        controls.gameObject.SetActive(false);
        back.gameObject.SetActive(false);
        buttons.SetActive(true);
        Title.gameObject.SetActive(true);
        playerCharacter.SetActive(true);
        Ak47.SetActive(true);
        Flamethrower.SetActive(true);
        buttonSound.Play();
    }
}
