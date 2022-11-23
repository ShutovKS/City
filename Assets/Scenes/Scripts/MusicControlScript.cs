using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControlScript : MonoBehaviour
{
    public static MusicControlScript instance;
    private bool vkl;

    private void Start()
    {
        vkl = true;    
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void VolumControl()
    {
        if (vkl == true)
        {
            GetComponent<AudioSource>().volume = 0;
            vkl = false;
        }
        else
        {
            GetComponent<AudioSource>().volume = 1;
            vkl= true;
        }
    }
}
