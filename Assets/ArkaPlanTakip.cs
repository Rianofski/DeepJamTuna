using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArkaPlanTakip : MonoBehaviour
{
    public Transform Player;
    public float XCarpan;
    public float YCarpan;
    public float Yoffset;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos =GetComponent<Transform>().position;
        pos.x = Player.position.x* XCarpan;
        pos.y = Yoffset - Player.position.y* YCarpan;
        GetComponent<Transform>().position = pos;

    }
}
