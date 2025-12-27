using System;
using UnityEngine;

namespace DefaultNamespace.FoxLogics
{
    public class PlayerAngelInteractionController : MonoBehaviour
    {
        [SerializeField] private PlayerObjectController playerObjectController;

        private void Start()
        {
            GlobalEvents.PlayerDetectedToAngel += OnPlayerDetectedToAngel;
        }
        
        private void OnDestroy()
        {
            GlobalEvents.PlayerDetectedToAngel -= OnPlayerDetectedToAngel;
        }

        private void OnPlayerDetectedToAngel()
        {
            Debug.Log("angel detected to player -- repositioning");
            //TODO detection anim & handle the respositioning 
            transform.position = new Vector3(-3.31f, 2.52f, -0.65f);
        }
    }
}