using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour 
{

    public float speed = 10f;
    public float deadZone = 0.5f;
    public float gravity = 20.0f;
    public float radiusDetection = 1f;

    public float delayEndStun;
    public float timeStuned;
    public float timeTwerking = 1f;

    GameObject confessionnal;
    public int playerNumber;
    float cdTwerk;

    public CharacterController cc;

    Vector3 moveDirection;

    public bool move;

    public STATE state;
    LayerMask nunLayer = 1 << 8;

    float currentTwerk = 0f;

    public enum STATE
    {
        ISTWERKING,
        NARMOL,
        STUN,
    }
        
	// Use this for initialization
	void Start () 
    {

        cc = GetComponent<CharacterController>();

        delayEndStun = 1.0f;
        timeStuned = 0.0f;


	    state = STATE.NARMOL;
	}
	
	// Update is called once per frame
    void Update()
    {
        cdTwerk = Mathf.Max(cdTwerk - Time.deltaTime, 0f);
        switch (state)
        {
            case STATE.NARMOL:
                isNormal();
                break;

            case STATE.ISTWERKING:
                isTwerking();
                break;

            case STATE.STUN:
                isStun();
                break;
        }

       
    }

    void isNormal()
    {

        moveDirection = new Vector3(Input.GetAxis("L_XAxis_" + playerNumber), 0, -Input.GetAxis("L_YAxis_" + playerNumber));
        moveDirection = transform.TransformDirection(moveDirection);

        move = false;

        if (cc.isGrounded)
            {
                moveDirection *= speed;
            }
        else
            {
                moveDirection *= speed / 2;
            }

        moveDirection.y -= gravity * 2 * Time.deltaTime;
        cc.Move(moveDirection * Time.deltaTime);

        if (Input.GetAxis("L_XAxis_" + playerNumber) < -deadZone)
        {
            Debug.Log(gameObject.name);
            move = true;
        }
        else if (Input.GetAxis("L_XAxis_" + playerNumber) > deadZone)
        {
            move = true;

        }

        if (Input.GetAxis("L_YAxis_" + playerNumber) < -deadZone)
        {
            move = true;

        }

        if (Input.GetAxis("L_YAxis_" + playerNumber) > deadZone)
        {
            move = true;

        }

        if (Input.GetButtonDown("Attack_" + playerNumber) && cdTwerk == 0)
        {
            cdTwerk = 0.2f;
            currentTwerk = 0;
            seekNun();
            //Launch Anim Twerk
            isTwerking();
        }

        
    }

    void isTwerking()
       {
            state = STATE.ISTWERKING;
            currentTwerk = Mathf.Min(currentTwerk + Time.deltaTime, timeTwerking);

            if(currentTwerk == timeTwerking)
            {
                state = STATE.NARMOL;
            }

            if (Input.GetButtonDown("Attack_"+ playerNumber) && cdTwerk == 0)
            {
                cdTwerk = 0.2f;
                currentTwerk = 0;
                seekNun();
                //Launch Anim Twerk
                isTwerking();
            }

       }


    void seekNun()
    {
        Collider[] nunArround = new Collider[0];

        nunArround = Physics.OverlapSphere(transform.position, radiusDetection,nunLayer);
        if(nunArround.Length != 0)
        {
            foreach(Collider co in nunArround)
            {
                co.GetComponent<NunScript>().plzTwkMe(gameObject);
            }
        }

    }

   void isStun()
    {
        timeStuned = timeStuned + Time.deltaTime;
        if (timeStuned >= delayEndStun)
        { 
            state = STATE.NARMOL;
        }
    }


    public void launchStun()
    {
        timeStuned = 0;
        state = STATE.STUN;
        Debug.Log("Aie je suis stun");
    }

}


