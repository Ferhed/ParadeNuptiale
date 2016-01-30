using UnityEngine;
using System.Collections;

public class player : MonoBehaviour 
{

    public float speed = 10f;
    public float deadZone = 0.5f;
    public float gravity = 20.0F;

    public float delayEndStun;
    public float timeStuned;

    public CharacterController cc;

    Vector3 moveDirection;

    public bool move;

    public STATE state;

    public enum STATE
    {
        ISTWERKING,
        NARMOL,
        STUN
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

            moveDirection = new Vector3(Input.GetAxis("L_XAxis_1" /*+ playerNumber*/), 0, -Input.GetAxis("L_YAxis_1" /*+ playerNumber*/));
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

            if (Input.GetAxis("L_XAxis_1" /*+ playerNumber*/) < -deadZone)
                {
                    Debug.Log("ntm");


                    move = true;

                }
            else if (Input.GetAxis("L_XAxis_1" /*+ playerNumber*/) > deadZone)
                {
                    move = true;

                }

            if (Input.GetAxis("L_YAxis_1" /*+ playerNumber*/) < -deadZone)
                {
                    move = true;

                }

            if (Input.GetAxis("L_YAxis_1" /*+ playerNumber*/) > deadZone)
                {
                    move = true;

                }

            if (Input.GetButtonDown("Attack_1" /*+ playerNumber*/))
                {
                    isTwerking();
                }

        
    }

    void isTwerking()
       { 
              state = STATE.ISTWERKING;
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
    }
        

}


