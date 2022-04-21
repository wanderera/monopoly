using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class mapManager : MonoBehaviour
{
    public Sprite mySprite;
    private GameObject[,] planes;
    public GameObject prefab;

    private string[,] gridTypes;

    private int width;
    private int height;

    string getGridType(int i, int j){
    	return gridTypes[i,width-1-j];
    }


    void Start()
    {
    	TextAsset dataset = Resources.Load<TextAsset>("setting");
  		string[] dataLines = dataset.text.Split('\n'); // Split also works with simple arguments, no need to pass char[]
 		string[] dims = dataLines[0].Split(',');
 		width = int.Parse(dims[0]);
 		height = int.Parse(dims[1]);
		gridTypes = new string[ width, height];
		print("init grid:"+width+"x"+height);

  		for(int i = 0; i < height; i++) {
    		string[] data = dataLines[i+1].Split(',');
    		for(int d = 0; d < width; d++) {
      			gridTypes[d,i] = data[d];
    		}
  		}

  		for(int i = 0; i < height; i++) {
			string a = "["+i+"]: ";
    		for(int d = 0; d < width; d++) {
      			a = a+gridTypes[d,i]+",";
    		}
  			print(a);
  		}



    	width = 4;
    	height = 4;
    	planes = new GameObject[width, height];
    	prefab = new GameObject();

    	Vector3 rUnit = new Vector3(0.5f,  0.25f, 0);
    	Vector3 cUnit = new Vector3(-0.5f,  0.25f, 0);

    	for (int x=0; x<width; x++){
    		for (int y=0; y<height; y++){
    			Vector3 pos = (x-width/2)*rUnit + (y-height/2)*cUnit;
    			planes[x, y] = Instantiate(prefab, pos, Quaternion.identity);
    			if (getGridType(x,y) != "*"){
        			SpriteRenderer sr = planes[x, y].AddComponent<SpriteRenderer>() as SpriteRenderer;
    				sr.sprite = mySprite;
    				if (getGridType(x, y) == "0") sr.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
    				else if (getGridType(x, y) == "1") sr.color = new Color(1.0f, 0.0f, 0.0f, 1.0f);
    				else if (getGridType(x, y) == "2") sr.color = new Color(0.0f, 1.0f, 0.0f, 1.0f);
    				else if (getGridType(x, y) == "-") sr.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    			}
    		}
    	}



    }

}
