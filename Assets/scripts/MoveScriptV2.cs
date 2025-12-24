using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveScriptV2 : MonoBehaviour
{
    public bool YerdeHareketEdebilir = true;
    public void FixedUpdate()
    {
        
        if(YerdeHareketEdebilir)//zýplamýyor düþmüyor yerde
        {
            Vector3 speed;
            if (Input.GetKey(KeyCode.A))
            {
                
                //newSped = new(veloMag, speed.y, speed.z);
                //MyRigidbdoy.velocity = newSped;

            }

        }
    }
}
