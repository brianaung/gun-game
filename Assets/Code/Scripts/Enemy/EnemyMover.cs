using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMover : MonoBehaviour
{
    // External parameters/variables

    [SerializeField] private float moveSpeed;
    [SerializeField] private float enemyHealth;
    
    public GameObject thePlayer;
    private Transform target;
    private Vector3 moveDirection;
    
    private void Awake()
    {
        thePlayer = GameObject.FindGameObjectsWithTag("Player")[0];
        this.target = thePlayer.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        var targetMovePosition = transform.position + (this.moveDirection*moveSpeed*Time.deltaTime);
        var collide = Physics.Raycast(transform.position, this.moveDirection, this.moveSpeed * Time.deltaTime);

        if(collide == false){
            transform.position = targetMovePosition;
        }   
        else{
            transform.rotation = Quaternion.Euler(new Vector3(0, -180, 0));
            var testDirection = new Vector3(0.0f, this.moveDirection.y, this.moveDirection.z);
            collide = Physics.Raycast(transform.position, testDirection, this.moveSpeed*Time.deltaTime);
            if(collide == false){
                transform.position +=(testDirection*this.moveSpeed*Time.deltaTime);
            }
        }     
    }

    private void FixedUpdate()
    {
        move2Direction();
    }

    private void move2Direction(){
        Vector3 direction = (target.position - transform.position).normalized;
        float angleX = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angleX, 0));
        this.moveDirection = direction;
    }
}
