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
    private float lossyX;
    private float lossyZ;
    private bool inRoom = false;
    private int updatenum = 0;

    
    // Start is called before the first frame update
    void Start()
    {
        if(Floor.Length == 0){
            var center = transform.position;
            lossyX = Mathf.Abs(transform.lossyScale.x);
            lossyZ = Mathf.Abs(transform.lossyScale.z);
            xPosMin = (int) (center.x - lossyX);
            xPosMax = (int) (center.x + lossyX);
            zPosMin = (int) (center.z - lossyZ);
            zPosMax = (int) (center.z - lossyZ);
        }
        if(Floor.Length > 3){
            var center = (Floor[0] + Floor[3]) / 2;
            lossyX = Mathf.Abs(((Floor[0] - Floor[1]) /2).x);
            lossyZ = Mathf.Abs(((Floor[0] - Floor[2]) / 2).z);
            xPosMin = (int) (center.x-lossyX);
            xPosMax = (int) (center.x+lossyX);
            zPosMin = (int) (center.z - lossyZ);
            zPosMax = (int) (center.z + lossyZ);
        }
        
    }

    private void Update() {
        if(lossyX > 9 && lossyZ > 9 && inRoom && updatenum==0){
            updatenum++;
            StartCoroutine(EnemyDrop());
        }
        if(Floor.Length == 0 && updatenum < 5){
            updatenum++;
            StartCoroutine(EnemyDrop());
        }
    }

    private void OnTriggerEnter(Collider col) {
        if(col.tag == "Position"){
            inRoom = true;
        }
        
    }

    IEnumerator EnemyDrop()
    {
        while(this.enemyCount < enemyNumber){
            int xPos, zPos;
            xPos = Random.Range(xPosMin, xPosMax);
            zPos = Random.Range(zPosMin, zPosMax);
            Instantiate(theEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            if(Floor.Length!=0) yield return new WaitForSeconds(0.5f);
            else yield return new WaitForSeconds(10f);
            this.enemyCount+=1;
        }
    }
}
