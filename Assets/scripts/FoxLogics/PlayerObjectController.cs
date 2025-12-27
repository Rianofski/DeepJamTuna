using System;
using UnityEngine;

namespace DefaultNamespace.FoxLogics
{
    public class PlayerObjectController : MonoBehaviour
    {
        private bool hasFoxObject;

        public bool HasFoxObject()
        {
            return hasFoxObject;
        }

        private void Start()
        {
            GlobalEvents.FoxObjectCollected += OnFoxObjectCollected;
        }
        
        private void OnDestroy()
        {
            GlobalEvents.FoxObjectCollected -= OnFoxObjectCollected;
        }

        private void OnFoxObjectCollected()
        {
            hasFoxObject = true;
        }
    }
}