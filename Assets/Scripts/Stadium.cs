using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

namespace LineAR
{
    public class Stadium : MonoBehaviour
    {
        /// <summary>
        /// Disables the input of the anchor plane attached to this gameobject.
        /// Usually called after the player setup the plane. It's locked.
        /// </summary>
        public void DisableAnchorInput() {
            GetComponent<PlaneFinderBehaviour>().enabled = false;
            GetComponent<AnchorInputListenerBehaviour>().enabled = false;
        }
    }
}