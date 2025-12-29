using UnityEngine;

public class SimpleMove : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // Klavyeden Sağ/Sol ok tuşlarını al
        float moveX = Input.GetAxis("Horizontal"); 
        
        // Karakteri o yöne it
        transform.Translate(Vector2.right * moveX * speed * Time.deltaTime);
    }
}