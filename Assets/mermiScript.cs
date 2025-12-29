using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mermiScript : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 PlayerPos;
    public float FireballSpeedConstant;
    void Start()
    {
        PlayerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 speed = PlayerPos- transform.position;
        speed=speed.normalized* FireballSpeedConstant;
        GetComponent<Rigidbody>().velocity = speed;
        Destroy(gameObject,5);
    }

    // Update is called once per frame
   
}
