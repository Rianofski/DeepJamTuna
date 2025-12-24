using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OyuncuTakip : MonoBehaviour
{
    public Transform OyuncuTakipTransform;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Takip());
    }

    // Update is called once per frame
    void Update()
    {

        
    }

    IEnumerator Takip()
    {
        while(true)
        {
            yield return new WaitForFixedUpdate();
            Vector3 error = OyuncuTakipTransform.position - transform.position;
            Vector3 takipvec;
            takipvec = error / 8;

            //PLATFORMUN YÜKSEKLÝÐÝNE GÖRE KAMERA Y'SÝ AYARLANABÝLÝR
            takipvec.y = takipvec.y / 5;
            takipvec.z = 0;

            if (Mathf.Sqrt(error.x * error.x + error.y * error.y) < 0.1f)
            {
                takipvec = OyuncuTakipTransform.position;
                takipvec.z = transform.position.z;
                transform.position = takipvec;
            }
          
            else
            transform.position += takipvec;






        }
    }
}
