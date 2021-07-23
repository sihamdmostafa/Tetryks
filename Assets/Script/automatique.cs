using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 
namespace AssemblyCSharp {

public class automatique : MonoBehaviour {

	//public Canvas s1 ; 
	public Canvas s2 ;// s2 et s3 sont des fenetre
	public Canvas s3 ;
 	public Button b1; //b1 et b2 sont des button pour detecter le mode (automatique ou manuelle)
	public Button b2; 
	public static bool auto ; //un boolean pour detecter si on est dans le mode automatique ou manuelle
     public Button[]  tab = new Button[4] ; //Contient les themes se forme d'un button 
	private static int i = 3 ;  
	public static bool []  j = new bool[4] ; //Contient l'etat de chaque theme c'est a dire est que il activer ou non
	public RawImage chek;  
 	// Use this for initialization
	void Start () {
 		s3.enabled = false;
		s2.enabled = true; 
 		auto = false ; 
		b1.gameObject.SetActive (true); 
 		b2.gameObject.SetActive (false); 
		tab [0].gameObject.SetActive (false);
		tab [1].gameObject.SetActive (false); 
		tab [2].gameObject.SetActive (false);
		tab [3].gameObject.SetActive (true); 
		j [0] = false; j [1] = false; j [2] = false; j [3] = false; 	
  	}
 
	// Update is called once per frame
	void Update () {
		chngbutton (); 
	}

    public void changbut() //change le mode (du mode manuelle vers le mode automatique)  
	{ 
		if (auto == false) {
			auto = true;
				s2.enabled = false; 
				s3.enabled = true;  
		 
		} else {
			auto = false;
				s2.enabled = true; 
				s3.enabled = false;
		}
	}
    //change le mode (du mode automatique vers le mode manuelle)  
	public void chngbutton()
	{
		if (auto == false) {
			b1.gameObject.SetActive (true); 
			b2.gameObject.SetActive (false);
				s3.enabled = false;
 		} 
		else {
			b1.gameObject.SetActive (false); 
			b2.gameObject.SetActive (true);
			chek.gameObject.SetActive (false);

 
			 
 		}
			
	}
    //dans le mode manuelle la methde chang permet de parcourir les theme de gauch a droit 
	public void chang()
	{
		if (i == 3) {//i c'est un indice contient l'indice de theme activer
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
    //dans le mode manuelle la methde chang permet de parcourir les theme de droit  a gauch 
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
    //Les méthodes choisithm (), choisithm1(), choisithm2(), choisithm3() : permet de choisir l’une des themes  . 
	public void choisithm()
	{
		if (j[0] == false) {//c'est l'utilisaeur choisi le theme qui est a l'indice 0 alors va activer ce theme et deactiver les autres
			j[0] = true; 
			j[1] = false;
			j[2] = false; 
			j[3] = false; 
			chek.gameObject.SetActive (true);  
		} else { //sinon on deactive le tout
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
			Colors.CurrentTheme=new Color(255,255,255,255); 
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
}