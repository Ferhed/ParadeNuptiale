using UnityEngine;
using System.Collections;

public class NunScript : MonoBehaviour {


    public int twerkMax;
    int twerkCurrent;

    bool ending = false;
    GameObject confessionalForEnd;
    Animation animation;

    NavMeshAgent agent;
    GameObject destination;

    float timeToSmile = 0;
    bool end = false;

    public GameObject bench;
    public NunManager NM;

    NUNSTATE state;

    enum NUNSTATE
    {
        WALKING,
        PRAYING,
        SMILING,
    }
    void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animation = transform.GetChild(0).GetComponentInChildren<Animation>();
    }

	// Use this for initialization
	void Start () {
        state = NUNSTATE.WALKING;
	}
	
	// Update is called once per frame
	void Update () {

        Debug.Log(state);


        switch(state)
        {
            case NUNSTATE.WALKING:
                walking();
                break;
            case NUNSTATE.SMILING:
                smiling();
                break;
            case NUNSTATE.PRAYING:
                praying();
                break;
        }



	}

    void praying()
    {

        transform.LookAt(GameManager.Instance.priest.transform);
    }
     
    void smiling()
    {
        timeToSmile = Mathf.Min(timeToSmile + Time.deltaTime, 2f);
        if(timeToSmile == 2f)
        {
            animation.Play("IDLE");
            state = NUNSTATE.PRAYING;
            transform.LookAt(GameManager.Instance.priest.transform);
        }

    }


    void walking()
    {
        if (destination)
        {
            if (end)
            {
                if (Vector3.Distance(destination.transform.position, transform.position) < 2.5f)
                {
                }
            }
            else
            {
                if (Vector3.Distance(destination.transform.position, transform.position) < 1)
                {
                    state = NUNSTATE.PRAYING;
                    animation.Play("IDLE"); 
                    transform.LookAt(GameManager.Instance.priest.transform);
                }
            }
        }
    }

    public void goToTheBench(GameObject bench)
    {
        if (!animation)
            animation = transform.GetChild(0).GetComponentInChildren<Animation>();
        animation.Play("WALK");
        state = NUNSTATE.WALKING;
        destination = bench;
        if(!agent)
            agent = GetComponent<NavMeshAgent>();
        agent.SetDestination(new Vector3(bench.transform.position.x, transform.position.y, bench.transform.position.z));
    }

    public bool plzTwkMe(GameObject player)
    {
        if (!end)
        {
            if (state != NUNSTATE.SMILING)
            {
                state = NUNSTATE.SMILING;
                transform.LookAt(player.transform);
                animation.Play("EXCITED");
            }
            timeToSmile = 0;
            twerkCurrent++;
            if (twerkCurrent == twerkMax)
            {
                state = NUNSTATE.WALKING;
                animation.Play("WALK");
                end = true;
                confessionalForEnd = player.GetComponent<playerScript>().confessionnal;
                destination = confessionalForEnd;
                transform.LookAt(confessionalForEnd.transform);
                agent.SetDestination(confessionalForEnd.transform.position);
                bench.GetComponent<benchScript>().nun = null;
                bench = null;
                NM.nunList.Remove(this.gameObject);

                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
}
