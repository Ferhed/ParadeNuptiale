using UnityEngine;
using System.Collections;

public class NunScript : MonoBehaviour {


    int twerkMax;
    int twerkCurrent;

    bool ending = false;
    GameObject confessionalForEnd;

    NavMeshAgent agent;
	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
	}
	
	// Update is called once per frame
	void Update () {
        if(confessionalForEnd)
        {
            if (Vector3.Distance(confessionalForEnd.transform.position, transform.position) < 2)
            {
                //lvlUpPoint
            }
        }
	}

    public void goToTheBench(GameObject bench)
    {
        if(!agent)
            agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(new Vector3(bench.transform.position.x, transform.position.y, bench.transform.position.z));
    }

    public bool plzTwkMe(GameObject player)
    {
        twerkCurrent++;
        if(twerkCurrent == twerkMax)
        {
            //confessionalForEnd = player.GetComponent<ScriptPlayer>().confessional;
            agent.SetDestination(confessionalForEnd.transform.position);
            return true;
        }
        else
        {
            return false;
        }
    }
}
