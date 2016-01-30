using UnityEngine;
using System.Collections;

public class NunScript : MonoBehaviour {


    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void goToTheBench(GameObject bench)
    {
        Debug.Log(bench.transform.position);
        if(!agent)
            agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(new Vector3(bench.transform.position.x, transform.position.y, bench.transform.position.z));
    }
}
