using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
	private Vector3 horizontalStep;
	private Vector3 verticalStep;

	private Sprite[] spriteSheet;
	private SpriteRenderer spriteRenderer;

	private float stepDuration; // in sec
	enum STEPSTATUS {IDLE, LEFTFOOT, MIDDLE, RIGHTFOOT};
	private STEPSTATUS currentStepStatus;
	private Vector3 walkingVelocity;
	private float animationPassTime;
	private float nextAnimationTime;
	private int startAnimationIndex;
	public string playerName;

    // Start is called before the first frame update
    void Start()
    {

		horizontalStep = new Vector3(0.5f, -0.5f, 0);
		verticalStep = new Vector3(0.5f, 0.5f, 0);
		walkingVelocity = new Vector3(0.0f, 0.0f, 0.0f);
        stepDuration = 1.0f;
        currentStepStatus = STEPSTATUS.IDLE;
        rewindAnimationTimer();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        if(playerName == "") playerName = "char";
        spriteSheet = Resources.LoadAll<Sprite>(playerName);
        spriteRenderer.sprite = spriteSheet[1];
    }

    void setToSubSprite(int i){
    	spriteRenderer.sprite = spriteSheet[i]; 
    }

    void startAnimation(string s){
    	if (s == "left"){
    		startAnimationIndex = 4;
			walkingVelocity = -horizontalStep;
    	}else if (s == "right"){
    		startAnimationIndex = 7;
			walkingVelocity = horizontalStep;
    	}else if (s == "down"){
    		startAnimationIndex = 1;
			walkingVelocity = -verticalStep;
    	}else if (s == "up"){
    		startAnimationIndex = 10;
			walkingVelocity = verticalStep;
    	}

    	currentStepStatus = STEPSTATUS.LEFTFOOT;
    	setToSubSprite(startAnimationIndex-1);
    	rewindAnimationTimer();
    }

    void rewindAnimationTimer(){
    	//Debug.Log("rewind"+animationPassTime+" "+nextAnimationTime+" "+currentStepStatus);
    	animationPassTime = 0;
    	nextAnimationTime = stepDuration/3;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputMove = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        if (currentStepStatus == STEPSTATUS.IDLE){
        	if (inputMove.x < 0) startAnimation("left");
        	else if (inputMove.x > 0) startAnimation("right");
        	else if (inputMove.y < 0) startAnimation("down");
        	else if (inputMove.y > 0) startAnimation("up");
        	
        }else{
        	animationPassTime += Time.deltaTime;
			transform.position += walkingVelocity*Time.deltaTime;
        	if ( animationPassTime >= nextAnimationTime){
        		if (currentStepStatus == STEPSTATUS.LEFTFOOT){	
        			setToSubSprite(startAnimationIndex);
        			currentStepStatus = STEPSTATUS.MIDDLE;
        			rewindAnimationTimer();
        		}else if (currentStepStatus == STEPSTATUS.MIDDLE){
        			setToSubSprite(startAnimationIndex+1);
        			currentStepStatus = STEPSTATUS.RIGHTFOOT;
        			rewindAnimationTimer();
        		}else if (currentStepStatus == STEPSTATUS.RIGHTFOOT){
        			setToSubSprite(startAnimationIndex);
        			currentStepStatus = STEPSTATUS.IDLE;
        			rewindAnimationTimer();
        			walkingVelocity.x = 0; walkingVelocity.y = 0;
        		}

        	}
        }
    }
}
