using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Timer : MonoBehaviour
{
   [SerializeField] private Image timerBackground ;
   [SerializeField] private Image timerFill ;
   [SerializeField] public TMP_Text timerText ;

   public int Duration;
   public int RemainDuration;

   private void Start() {
    End();
   }

   public void StartTimer() {
    Debug.Log("start Timer");
    timerFill.enabled = true;
    timerBackground.enabled = true;
    timerText.enabled = true;
    Being(Duration);
   }


   private void Being(int seconds) {
    RemainDuration = seconds;
    StartCoroutine(udateTimer());
   }

   private IEnumerator udateTimer() {
    while (RemainDuration>=0)
    {
        timerFill.fillAmount = Mathf.InverseLerp(0,Duration,RemainDuration);
        RemainDuration--;
        yield return new WaitForSeconds(1f);
        
    }
    End();
   }

   private void End() {
    timerFill.enabled = false;
    timerBackground.enabled = false;
    timerText.enabled = false;
   }
}
