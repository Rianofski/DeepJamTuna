using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class restrart : MonoBehaviour
{

    public Vector3 BaslangicKonumu;
    public Vector3 BaslangicKonumu2;

    private void Start()
    {
        BaslangicKonumu = transform.position;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("TilkiKuyruk"))
        {
            //oldün
            transform.position = BaslangicKonumu;
        }

        if (other.gameObject.CompareTag("DeathBySuSu"))
        {
            //oldün
            transform.position = BaslangicKonumu2;
        }

    }
    
}
