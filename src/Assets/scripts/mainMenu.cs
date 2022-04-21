using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

	private Sprite[] spriteSheet;
	//private SpriteRenderer spriteRenderer;
	//private int currentFrame;
	private int framesPerSecond;

	public void Start(){
		//spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteSheet = Resources.LoadAll<Sprite>("menuBackground_3x5");
        this.GetComponent<UnityEngine.UI.Image>().sprite = spriteSheet[0];
        //currentFrame = 0;
        framesPerSecond = 10;

	}

	public void Update(){
		int index = (int)(Time.time * framesPerSecond); 
		index = index % spriteSheet.Length; 
		this.GetComponent<UnityEngine.UI.Image>().sprite = spriteSheet[index];

	}

    // Update is called once per frame
    public void playGame()
    {
        SceneManager.LoadScene("selectPlayer"); 
    }

    public void quitGame()
    {
    	Application.Quit();
    }
}
