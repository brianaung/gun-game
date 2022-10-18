// using UnityEngine;

// public class DestroyOffScreen : MonoBehaviour
// {
//     // Triggered as soon as the object is outside of the camera frustum.
//     // private void OnBecameInvisible()
//     // {
//     //     Destroy(gameObject);
//     // }

//     // void DestroyObjectDelayed()
//     // {
//     //     // Kills the game object in 5 seconds after loading the object
//     //     Destroy(gameObject, 5);
//     // }
//     private float startTime = Time.time;
//     void Start() {
//         startTime = 0;
//     }
    
//     void Update()
//     {
//         // if (!gameObject.transform.hasChanged)
//         // {
//         //     print("The transform has not changed!");
//         //     Destroy(gameObject);
//         //     transform.hasChanged = true;

//         // }
//         if (Time.time-startTime>2) {
//             Destroy(gameObject);
//         }
        
//     }
// }
