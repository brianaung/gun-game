using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private int maxEnemyNumber;

    public GameObject theEnemy;
    private int xPos;
    private int zPos;
    private int enemyCount;

    void Awake(){
    }
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyDrop());
    }

    IEnumerator EnemyDrop()
    {
        while(this.enemyCount < maxEnemyNumber){
            xPos = Random.Range(0, 10);
            zPos = Random.Range(30, 50);
            Instantiate(theEnemy, new Vector3(xPos, 0, zPos), Quaternion.identity);
            yield return new WaitForSeconds(0.5f);
            this.enemyCount+=1;
        }
    }
}
