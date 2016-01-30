using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {


    static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        instance = this;
    }


   
	
}
