using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    
    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Interactable"))
        {
            collision.transform.GetComponent<InteractableObjectScript>().AlinabilirHaleGetir(true);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Interactable"))
        {
            collision.transform.GetComponent<InteractableObjectScript>().AlinabilirHaleGetir(false);
        }
    }
}
