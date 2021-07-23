using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 
public class automatique : MonoBehaviour {

	public Canvas s1 ; 
	public Canvas s2 ;
	public Canvas s3 ;
 	public Button b1; 
	public Button b2; 
	public static bool auto ; 
     public Button[]  tab = new Button[4] ; 
	private static int i = 0 ;  
	public static bool []  j = new bool[4] ; 
	public RawImage chek;  
 	// Use this for initialization
	void Start () {
 		s1.enabled = false;
		s2.gameObject.SetActive (true);
		auto = false ; 
		b1.gameObject.SetActive (true); 
		b2.gameObject.SetActive (false); 
		tab [0].gameObject.SetActive (false);
		tab [1].gameObject.SetActive (false); 
		tab [2].gameObject.SetActive (false);
		tab [3].gameObject.SetActive (false); 
		j [0] = false; j [1] = false; j [2] = false; j [3] = false; 	
  	}
 
	// Update is called once per frame
	void Update () {
		chngbutton (); 
	}
	public void enbalemg()
	{
		s1.enabled = true; 
	}
	public void changbut()
	{ 
		if (auto == false) {
			auto = true;
			s2.gameObject.SetActive (false); 
			s3.gameObject.SetActive (true); 
			//tab [0].gameObject.SetActive (false);
		} else {
			auto = false;
			//tab [0].gameObject.SetActive (true);
			s2.gameObject.SetActive(true) ; 
			s3.gameObject.SetActive (false);
		}
	}
	public void chngbutton()
	{
		if (auto == false) {
			b1.gameObject.SetActive (true); 
			b2.gameObject.SetActive (false);
			s3.gameObject.SetActive(false) ;
 		} 
		else {
			b1.gameObject.SetActive (false); 
			b2.gameObject.SetActive (true);
			chek.gameObject.SetActive (false);

 
			 
 		}
			
	}
	public void chang()
	{
		if (i == 3) {
			tab [i].gameObject.SetActive (false);
			i = 0; 
			tab [i].gameObject.SetActive (true);
			if(j[0]==true)
			chek.gameObject.SetActive (true); 
			else
			chek.gameObject.SetActive (false);	
		} else {
			tab [i].gameObject.SetActive (false);
			i++; 
			tab [i].gameObject.SetActive (true);
			if(j[i]==true)
				chek.gameObject.SetActive (true);
			else
				chek.gameObject.SetActive (false);
		}
	}
	public void chang1()
	{
		if (i == 0) {
			tab [i].gameObject.SetActive (false);
			i = 3; 
			tab [i].gameObject.SetActive (true);
			if(j[3]==true)
				chek.gameObject.SetActive (true);
			else
				chek.gameObject.SetActive (false);
		} else {
			tab [i].gameObject.SetActive (false);
			i--; 
			tab [i].gameObject.SetActive (true);
			if(j[i]==true)
				chek.gameObject.SetActive (true);
			else
				chek.gameObject.SetActive (false);
		}
	}
	public void choisithm()
	{
		if (j[0] == false) {
			j[0] = true; 
			j[1] = false;
			j[2] = false; 
			j[3] = false; 
			chek.gameObject.SetActive (true);  
		} else {
			j[0] = false; 
			j[1] = false;
			j[2] = false; 
			j[3] = false; 
			chek.gameObject.SetActive (false);  
		}
	}
	public void choisithm1()
	{
		if (j[1] == false) {
			j[0] = false; 
			j[1] = true;
			j[2] = false; 
			j[3] = false;
			chek.gameObject.SetActive (true);  
		} else {
			j[0] = false; 
			j[1] = false;
			j[2] = false; 
			j[3] = false;
			chek.gameObject.SetActive (false); 

		}
	}
	public void choisithm2()
	{
		if (j[2] == false) {
			j[0] = false; 
			j[1] = false;
			j[2] = true; 
			j[3] = false;
			chek.gameObject.SetActive (true);  
		} else {
			j[0] = false; 
			j[1] = false;
			j[2] = false; 
			j[3] = false; 
			chek.gameObject.SetActive (false); 

		}
	}
	public void choisithm3()
	{
		if (j[3] == false) {
			j[0] = false; 
			j[1] = false;
			j[2] = false; 
			j[3] = true; 
			chek.gameObject.SetActive (true);  
		} else {
			j[0] = false; 
			j[1] = false;
			j[2] = false; 
			j[3] = false;
			chek.gameObject.SetActive (false); 

		}
	}
}
