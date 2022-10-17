using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject theEnemy;
    public Vector3[] Floor;
    private int enemyCount;
    public int enemyNumber;
    
    private int xPosMin;
    private int xPosMax;
    private int zPosMin;
    private int zPosMax;

    
    // Start is called before the first frame update
    void Start()
    {
        var center = (Floor[0] + Floor[3]) / 2;
        var lossyX = Mathf.Abs(((Floor[0] - Floor[1]) /2).x);
        var lossyZ = Mathf.Abs(((Floor[0] - Floor[2]) / 2).z);
        xPosMin = (int) (center.x-lossyX);
        xPosMax = (int) (center.x+lossyX);
        zPosMin = (int) (center.z - lossyZ);
        zPosMax = (int) (center.z + lossyZ);
        if(lossyX >= 10 && lossyZ >= 10){
            // StartCoroutine(EnemyDrop());
        }
    }


    private void OnTriggerEnter(Collider col){
            Debug.Log(col.gameObject.tag);
        if(col.gameObject.tag == "Player"){
        }
    }
    IEnumerator EnemyDrop()
    {
        while(this.enemyCount < enemyNumber){
            int xPos, zPos;
            xPos = Random.Range(xPosMin, xPosMax);
            zPos = Random.Range(zPosMin, zPosMax);
            Instantiate(theEnemy, new Vector3(xPos, 1, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            this.enemyCount+=1;
        }
    }
}
