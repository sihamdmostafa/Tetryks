using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 
using UnityEngine.AI ; 
public class ess : MonoBehaviour {
	public InputField p1 ; 
	// Use this for initialization
	void Start () {
		
		
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (p1.text);
	}
	public void IntilCH () {/*
		foreach (var item in challanges) {
			var x = Instantiate (element);
			x.transform.SetParent( elementHolder.transform);
			x.transform.localScale = new Vector3 (1, 1, 1);
			(x.GetComponent<Button> ()).onClick.AddListener (() =>  { });
		 */	
			}
}
