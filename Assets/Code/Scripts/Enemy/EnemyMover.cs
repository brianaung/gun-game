using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMover : MonoBehaviour
{
    // External parameters/variables

    [SerializeField] private float moveSpeed;
    [SerializeField] private float enemyHealth;
    [SerializeField] private ContactController contact;
    
    public GameObject thePlayer;
    private Transform target;
    private Vector3 moveDirection;
    private bool stop = false;
    
    private void Awake()
    {
        thePlayer = GameObject.FindGameObjectsWithTag("Player")[0];
        this.target = thePlayer.transform;
    }

    // Update is called once per frame
    private void Update()
    {   
        if(stop)
        {
            StartCoroutine(Stop());
        }
        else
        {
            Vector3 floor_direction = new Vector3(this.moveDirection.x, 0.0f, this.moveDirection.z);
            var targetMovePosition = transform.position + (floor_direction*moveSpeed*Time.deltaTime);
            var collide = Physics.Raycast(transform.position, floor_direction, this.moveSpeed * Time.deltaTime);

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
    }

    private void FixedUpdate()
    {
        move2Direction();
    }

    private void OnTriggerEnter(Collider col) {
        if(contact.getRole() == "boss" && col.tag == "Player"){
            stop = true;
        }
    }

    private void move2Direction(){
        Vector3 direction = (target.position - transform.position).normalized;
        float angleX = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angleX, 0));
        this.moveDirection = direction;
    }

    IEnumerator Stop()
    {
        yield return new WaitForSeconds(1.5f);
        stop = false;
    }
}
