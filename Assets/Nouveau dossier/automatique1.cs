using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class automatique1 : MonoBehaviour {

	//public Canvas popupthem;

	public RawImage them1;
	public RawImage them2;
	public RawImage them3;
	public RawImage them4 ; 
	public Button[]  tab1 = new Button[4] ; 
	public Canvas s3; 
	// Use this for initialization
	void Start () {
		tab1 [0].gameObject.SetActive (false);
		tab1 [1].gameObject.SetActive (false);
		tab1 [2].gameObject.SetActive (false);
		tab1 [3].gameObject.SetActive (false);
		//popupthem.enabled = false;
	}
	// Update is called once per frame
	void Update () {
		if (automatique.auto == true) {

			autothem (); 
		}
		else
		{
			if (automatique.j[0] == true) {
				them1.gameObject.SetActive (true);
				them2.gameObject.SetActive (false);
				them3.gameObject.SetActive (false);
				them4.gameObject.SetActive (false);
			} else if (automatique.j[1] == true) {
				them1.gameObject.SetActive (false);
				them2.gameObject.SetActive (true);
				them3.gameObject.SetActive (false);
				them4.gameObject.SetActive (false);
			} else if (automatique.j[2] == true) {
				them1.gameObject.SetActive (false);
				them2.gameObject.SetActive (false);
				them3.gameObject.SetActive (true);
				them4.gameObject.SetActive (false);
			} else if (automatique.j[3] == true) {
				them1.gameObject.SetActive (false);
				them2.gameObject.SetActive (false);
				them3.gameObject.SetActive (false);
				them4.gameObject.SetActive (true);
			}
			else {
				them1.gameObject.SetActive (false);
				them2.gameObject.SetActive (false);
				them3.gameObject.SetActive (true);
				them4.gameObject.SetActive (false);
			}
		}
	}

	public void autothem ()
	{

		int month = DateTime.Now.Month;
		int heure =DateTime.Now.Hour ;
		Debug.Log (heure);
		if (compmois(month))
		{
			compheu (heure, 1);
		}
		else
		{
			compheu (heure, 0);
		}

	}
	public Boolean compmois(int mois)
	{
		if (mois >= 10 || mois <= 03)
		{

			return true;

		}
		else 
		{
			return false;

		}

	}
	public void compheu(int heure, int ind)
	{
		if (ind==1)
		{
			if (heure >= 7 & heure <= 10)
			{
				Debug.Log ("1"); 
				them1.gameObject.SetActive (true);
				them2.gameObject.SetActive (false);
				them3.gameObject.SetActive (false);
				them4.gameObject.SetActive (false);
				tab1 [0].gameObject.SetActive (true);
				tab1 [1].gameObject.SetActive (false);
				tab1 [2].gameObject.SetActive (false);
				tab1 [3].gameObject.SetActive (false);
			}
			if (heure > 10 & heure <= 17)
			{
				Debug.Log ("2"); 
				them1.gameObject.SetActive (false);
				them2.gameObject.SetActive (true);
				them3.gameObject.SetActive (false);
				them4.gameObject.SetActive (false);
				tab1 [0].gameObject.SetActive (false);
				tab1 [1].gameObject.SetActive (true);
				tab1 [2].gameObject.SetActive (false);
				tab1 [3].gameObject.SetActive (false);

			}
			if (heure > 17 & heure <= 18)
			{
				Debug.Log ("3"); 
				them1.gameObject.SetActive (false);
				them2.gameObject.SetActive (false);
				them3.gameObject.SetActive (true);
				them4.gameObject.SetActive (false);
				tab1 [0].gameObject.SetActive (false);
				tab1 [1].gameObject.SetActive (false);
				tab1 [2].gameObject.SetActive (true);
				tab1 [3].gameObject.SetActive (false);

			}
			if ((heure > 18 & heure <= 23) ||( heure >= 00 & heure < 7))
			{
				Debug.Log("4"); 
				them1.gameObject.SetActive (false);
				them2.gameObject.SetActive (false);
				them3.gameObject.SetActive (false);
				them4.gameObject.SetActive (true);
				tab1 [0].gameObject.SetActive (false);
				tab1 [1].gameObject.SetActive (false);
				tab1 [2].gameObject.SetActive (false);
				tab1 [3].gameObject.SetActive (true);

			}
		}
		else 
		{
			if (heure >= 5 & heure <= 10)
			{
				Debug.Log ("5"); 
				them1.gameObject.SetActive (true);
				them2.gameObject.SetActive (false);
				them3.gameObject.SetActive (false);
				them4.gameObject.SetActive (false);
				tab1 [0].gameObject.SetActive (true);
				tab1 [1].gameObject.SetActive (false);
				tab1 [2].gameObject.SetActive (false);
				tab1 [3].gameObject.SetActive (false);

			}
			if (heure > 10 & heure <= 19)
			{
				them1.gameObject.SetActive (false);
				them2.gameObject.SetActive (true);
				them3.gameObject.SetActive (false);
				them4.gameObject.SetActive (false);
				tab1 [0].gameObject.SetActive (false);
				tab1 [1].gameObject.SetActive (true);
				tab1 [2].gameObject.SetActive (false);
				tab1 [3].gameObject.SetActive (false);

			}
			if (heure > 19 & heure < 21 )
			{
				them1.gameObject.SetActive (false);
				them2.gameObject.SetActive (false);
				them3.gameObject.SetActive (true);
				them4.gameObject.SetActive (false);
				tab1 [0].gameObject.SetActive (false);
				tab1 [1].gameObject.SetActive (false);
				tab1 [2].gameObject.SetActive (true);
				tab1 [3].gameObject.SetActive (false);

			}
			if ((heure >= 21 & heure <=23) || ( heure >=00 & heure < 5))
			{
				them1.gameObject.SetActive (false);
				them2.gameObject.SetActive (false);
				them3.gameObject.SetActive (false);
				them4.gameObject.SetActive (true);
				tab1 [0].gameObject.SetActive (false);
				tab1 [1].gameObject.SetActive (false);
				tab1 [2].gameObject.SetActive (false);
				tab1 [3].gameObject.SetActive (true);

			}
		}

	}
}
