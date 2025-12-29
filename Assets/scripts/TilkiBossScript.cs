using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilkiBossScript : MonoBehaviour
{
    public Transform Player;
    public Transform KuyrukTransform;

    public Transform AgizPos;
    public GameObject FireballObjcet;

    public Transform TilkiHead;
    // Start is called before the first frame update
    void Start()
    {
        //StartCoroutine(KuyrukDarbesi());
        StartCoroutine(FireballAt());
    }

    IEnumerator FireballAt()
    {
        while(true)//gerekince kapanacak
        {
            yield return new WaitForSeconds(3);
            GetComponent<Animator>().SetTrigger("Attack");
            yield return new WaitForSeconds(0.1f);
            Instantiate(FireballObjcet,AgizPos.transform.position,new Quaternion(0,0,0,0));
            
        }


    }


    void FixedUpdate()
    {
        //Vector3 HedefVec= Player.position - TilkiHead.transform.position;
        //float pitchAngle = Mathf.Atan(HedefVec.y/HedefVec.x) * Mathf.Rad2Deg + 45;
        //float yawAngle = Mathf.Atan(HedefVec.z/HedefVec.x) * Mathf.Rad2Deg;

        //Debug.Log(pitchAngle);Debug.Log(yawAngle);
        //Debug.Log(HedefVec.y); Debug.Log(HedefVec.x);
        //Vector3 hold = TilkiHead.transform.localRotation.eulerAngles;
        //hold.x = pitchAngle;hold.z = yawAngle;
        //TilkiHead.transform.localRotation = Quaternion.Euler(hold);

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
   
}
