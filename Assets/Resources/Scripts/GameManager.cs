using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    static GameManager instance;
    public GameObject[] confessional;
    public GameObject priest;

    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        instance = this;
        confessional = GameObject.FindGameObjectsWithTag("Confessional");
        priest = GameObject.FindGameObjectWithTag("Priest");
    }

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
