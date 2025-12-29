using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayeriOldur : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform PlayerDogmaYeri;
    void Start()
    {
        
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //oldün
            other.transform.position = PlayerDogmaYeri.transform.position;
        }

       

    }
}
