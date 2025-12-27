using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{
    bool alindi = false;
    void Start()
    {
        
    }

    public void AlinabilirHaleGetir(bool ackapa)
    {
        
        if(ackapa&&!alindi)
            StartCoroutine(AlinacakKontrol());
        else
            if(!alindi)
                StopAllCoroutines();

    }

    public IEnumerator AlinacakKontrol()
    {
        while (true)
        {
            yield return new WaitForFixedUpdate();

            if ((Input.GetKey(KeyCode.E)))
            {
                StopAllCoroutines();
                alindi = true;
                GlobalEvents.FoxObjectCollected?.Invoke();
                StartCoroutine(OyuncuyaGit());//her obje envantere gitmek zorunda de�il
            }
        }
    }
    
    IEnumerator OyuncuyaGit()
    {
        float temptime = 0;
        Transform oyuncuTransform = GameObject.FindGameObjectWithTag("Player").transform;
        while(temptime<1)
        {
            yield return new WaitForFixedUpdate();
            temptime += Time.deltaTime;

            Vector3 newpos=Vector3.Lerp(transform.position, oyuncuTransform.position,temptime);
            transform.position = newpos;

            Vector3 newScale = Vector3.Lerp(transform.localScale, new Vector3(0, 0, 0), temptime);
            transform.localScale = newScale;
        }
    }
}
