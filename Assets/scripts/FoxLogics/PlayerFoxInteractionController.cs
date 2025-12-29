using System;
using UnityEngine;

namespace DefaultNamespace.FoxLogics
{
    public class PlayerFoxInteractionController : MonoBehaviour
    {
        [SerializeField] private PlayerObjectController playerObjectController;

        public TilkiBossScript tilkiscript;
        private void Start()
        {
            GlobalEvents.FoxColliderTriggered += OnFoxColliderTriggered;
        }
        
        private void OnDestroy()
        {
            GlobalEvents.FoxColliderTriggered -= OnFoxColliderTriggered;
        }

        private void OnFoxColliderTriggered()
        {
            if (playerObjectController.HasFoxObject())
            {
                Debug.Log("fox companion -- can pass");
                //TODO tilki companion animasyonu
            }
            else
            {
                Debug.Log("fox companion -- can't pass");
                //TODO tilki attack animasyonu ve repositioning
                // (daha anlaşılır olması için reposition animasyon bitince yapılabilir)
                transform.position = new Vector3(-3.31f, 2.52f, -0.65f);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            //if(other.CompareTag()) tilki boss enable edecek collider
        }
    }
}