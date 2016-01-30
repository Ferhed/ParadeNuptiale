using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NunManager : MonoBehaviour {

    List<GameObject> nunList;
    public GameObject[] bench;
    public GameObject[] spawners;


    float timer = 0;
    public float timeToPopNun = 5f;

	// Use this for initialization
	void Start () {
        nunList = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        timer = Mathf.Min(timer + Time.deltaTime, timeToPopNun);
        if(timer == timeToPopNun)
        {
            
            addNun();
        }
	}



    void addNun()
    {
       
        if (nunList.Count < bench.Length)
        {
            timer = 0;
            GameObject currentBench = bench[Random.Range(0, bench.Length)];
            while (currentBench.GetComponent<benchScript>().nun)
            {
                currentBench = bench[Random.Range(0, bench.Length)];
            }
            GameObject currentNun = Instantiate((Resources.Load("Prefab/Nun", typeof(GameObject))), spawners[Random.Range(0, spawners.Length)].transform.position, Quaternion.identity) as GameObject;

            nunList.Add(currentNun);

            if (currentNun)
            {
                Debug.Log("nun");
            }
            currentBench.GetComponent<benchScript>().nun = currentNun; 
            if (currentBench.GetComponent<benchScript>().nun)
            {
                Debug.Log("bench");
            }
            currentNun.GetComponent<NunScript>().goToTheBench(currentBench);
        }
    }
}
