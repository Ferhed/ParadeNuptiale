using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

    public float speed = 10f;
    public float deadZone = 0.5f;
    public float gravity = 20.0F;

    public int playerNumber;


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
	void Start () {

        cc = GetComponent<CharacterController>();


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
            Debug.Log("ntm");


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

        if (Input.GetButtonDown("Attack_" + playerNumber))
        {
            isTwerking();
        }
    }

        void isTwerking()
        { 
            
        }

        

	}


