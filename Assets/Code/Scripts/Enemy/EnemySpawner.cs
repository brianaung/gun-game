using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private int maxEnemyNumber;

    public GameObject theEnemy;
    public string position;
    private int enemyCount;
    
    private int xPosMin;
    private int xPosMax;
    private int zPosMin;
    private int zPosMax;

    

    void Awake(){
        xPosMin = (int) (transform.position.x-2*transform.lossyScale.x);
        xPosMax = (int) (transform.position.x+2*transform.lossyScale.x);
        // xPosMax = (int) (transform.position.x+transform.lossyScale.x);
        zPosMin = (int) (transform.position.z-transform.lossyScale.z/2);
        zPosMax = (int) (transform.position.z + transform.lossyScale.z/2);
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while(this.enemyCount < maxEnemyNumber){
            int xPos = Random.Range(xPosMin, xPosMax);
            int zPos = Random.Range(zPosMin, zPosMax);
            Debug.Log((xPos, zPos));
            Instantiate(theEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            this.enemyCount+=1;
        }
    }
}
