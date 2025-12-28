using UnityEngine;

namespace DefaultNamespace.FoxLogics
{
    public class FoxCollider : MonoBehaviour
    {
        private void OnTriggerEnter(Collider collision)
        {
            Debug.Log(collision.gameObject.name);
            if(collision.gameObject.CompareTag("Player"))
            {
                GlobalEvents.FoxColliderTriggered?.Invoke();
                Debug.Log("FOX COLLIDER TRIGGERED");
            }
        }
    }
}