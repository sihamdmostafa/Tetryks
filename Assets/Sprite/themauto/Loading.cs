using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ; 

public class Loading : MonoBehaviour {
	public Transform loadingbar ; 
	public Transform textindicator; 
 	[SerializeField] private float currentAmount; 
	[SerializeField] private float speed ; 
	// Update is called once per frame
	void Update () {
		if (currentAmount < 100) {
			currentAmount += speed * Time.deltaTime; 
			textindicator.GetComponent<Text> ().text = ((int)currentAmount).ToString () + "%"; 
 		} else {
 			textindicator.GetComponent<Text> ().text = "Fin"; 


		}
		loadingbar.GetComponent<Image> ().fillAmount = currentAmount / 100; 
		
	}
}
