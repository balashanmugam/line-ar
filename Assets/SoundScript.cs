using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundScript : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag("music");
        if (objects.Length > 1)
            Destroy(this.gameObject); // we do this because when we shift from the mesh scene back to the men, another gameobject gets created and the music becomes unstable.
        DontDestroyOnLoad(this.gameObject); //not to destroy this object as the scene changes

    }
}
