using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilkiBossScript : MonoBehaviour
{
    public Transform Player;
    public Transform KuyrukTransform;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(KuyrukDarbesi());
    }

    IEnumerator KuyrukDarbesi()
    {
        float kuyrukdususHizi = 0.5f;

        //kuyruk takibi
        Vector3 vec = Player.position - KuyrukTransform.position;
        float tempangle = Vector3.SignedAngle(new(-1, 0, 0), vec, new(0, -1, 0));
        Vector3 hold = KuyrukTransform.transform.rotation.eulerAngles;
        hold.y = -tempangle;
        KuyrukTransform.transform.rotation = Quaternion.Euler(hold);

        //indirme
        float gecenZaman = 0;
        Vector3 KuyrukRot = KuyrukTransform.transform.rotation.eulerAngles;
        Vector3 KuyrukGidecekRot = new(KuyrukRot.x, KuyrukRot.y, 90);
        Vector3 newKuyrukRot = new(0, 0, 0);
        while (gecenZaman < kuyrukdususHizi) 
        {
            gecenZaman += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            newKuyrukRot = Vector3.Lerp(KuyrukRot, KuyrukGidecekRot, gecenZaman/ kuyrukdususHizi);
            KuyrukTransform.transform.rotation = Quaternion.Euler(newKuyrukRot);
        }
        yield return new WaitForSeconds(1);


        //kaldýrma
        gecenZaman = 0;
        KuyrukRot = KuyrukTransform.transform.rotation.eulerAngles;
        KuyrukGidecekRot = new(KuyrukRot.x, KuyrukRot.y, 0);
        while (gecenZaman < kuyrukdususHizi)
        {
            gecenZaman += Time.deltaTime;
            yield return new WaitForFixedUpdate();
            newKuyrukRot = Vector3.Lerp(KuyrukRot, KuyrukGidecekRot, gecenZaman/ kuyrukdususHizi);
            KuyrukTransform.transform.rotation = Quaternion.Euler(newKuyrukRot);
        }
        yield return new WaitForSeconds(4);
        StartCoroutine(KuyrukDarbesi());
    }
    // Update is called once per frame
    void FixedUpdate()
    {
      
    }
}
