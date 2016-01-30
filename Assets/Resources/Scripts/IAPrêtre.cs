using UnityEngine;
using System.Collections;

public class IAPrêtre : MonoBehaviour {

    public GameObject visionGauche;
    public GameObject visionDroite;
    public GameObject centerOfChurch;
    public GameObject backOfChurch;
    public float speedRotate = 1f;
    public float timeInLookingState = 2f;
    public float timeInPrayingState = 10f;

    public GameObject[] players;

    Vector3 angleLeft;
    Vector3 angleRight;
    float baseRotation;
    float timer = 0;
    float maxTimeForPraying;
    float maxTimeInLookingState;

    PRIESTSTATE state;
    enum PRIESTSTATE 
    {
        PRAY,
        ROTATE,
        LOOKING,
    }
	// Use this for initialization
	void Start () {
        state = PRIESTSTATE.PRAY;
        players = GameObject.FindGameObjectsWithTag("Player");
        baseRotation = transform.localRotation.y;
        maxTimeForPraying = Random.Range(1, 2);
	}
	
	// Update is called once per frame
	void Update () {
        calculateAngle();
        switch (state)
        {
            case PRIESTSTATE.PRAY:
                prayForTheBirdGod();
                break;
            case PRIESTSTATE.ROTATE:
                break;
            case PRIESTSTATE.LOOKING:
                seekingHotBoy();
                break;
        }

        Debug.DrawRay(transform.position, angleLeft*60, Color.red);
        Debug.DrawRay(transform.position, angleRight*60, Color.red);
	}

    void prayForTheBirdGod()
    {
        timer = Mathf.Min(timer + Time.deltaTime, maxTimeForPraying);

        if(timer == maxTimeForPraying)
        {
            timer = 0;
            state = PRIESTSTATE.ROTATE;
            StartCoroutine("rotateForLooking",centerOfChurch);
        }

        //Invoke(rotateForPraying(centerOfChurch), 5f);
    }


    void seekingHotBoy()
    {
        timer = Mathf.Min(timer + Time.deltaTime, maxTimeInLookingState);

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].GetComponent<playerScript>().state == playerScript.STATE.ISTWERKING)
            {

                Vector3 currentVector = players[i].transform.position - transform.position;
                float angle = Vector3.Angle(transform.forward, currentVector);
                if (angle < 15 && angle > -15)
                {
                    players[i].GetComponent<playerScript>().launchStun();
                }
            }
        }

        if (timer == maxTimeInLookingState)
        {
            timer = 0;
            state = PRIESTSTATE.ROTATE;
            StartCoroutine("rotateForPraying");
        }
    }

    IEnumerator rotateForLooking(GameObject target)
    {
        bool right = false;
        if(Random.Range(0,100) < 50)
        {
            right = true;
        }
        Vector3 currentVector = target.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, currentVector);
        while (angle > 4 || angle < -4)
        {
            currentVector = target.transform.position - transform.position;
            angle = Vector3.Angle(transform.forward, currentVector);
            if(right)
                transform.Rotate(transform.up, Time.deltaTime * speedRotate);
            else
                transform.Rotate(transform.up, -Time.deltaTime * speedRotate);
            yield return null;
        }

        state = PRIESTSTATE.LOOKING;
        maxTimeInLookingState = Random.Range(timeInLookingState - timeInLookingState / 2, timeInLookingState + timeInLookingState / 2);

        yield return null;
    }

    IEnumerator rotateForPraying()
    {
        bool right = false;
        if (Random.Range(0, 100) < 50)
        {
            right = true;
        }
        Vector3 currentVector = backOfChurch.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, currentVector);
        while (angle > 4 || angle < -4)
        {
            currentVector = backOfChurch.transform.position - transform.position;
            angle = Vector3.Angle(transform.forward, currentVector); 
            if (right)
                transform.Rotate(transform.up, Time.deltaTime * speedRotate);
            else
                transform.Rotate(transform.up, -Time.deltaTime * speedRotate);
            yield return null;
            yield return null;
        }

        state = PRIESTSTATE.PRAY;
        timer = 0;
        maxTimeForPraying = Random.Range(timeInPrayingState - timeInPrayingState / 2, timeInPrayingState + timeInPrayingState / 2);
        yield return null;
    }


    void calculateAngle()
    {
        angleLeft = visionGauche.transform.position - transform.position;
        angleRight = visionDroite.transform.position - transform.position;
        angleLeft.Normalize();
        angleRight.Normalize();
    }


}
