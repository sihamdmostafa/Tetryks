using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AssemblyCSharp
{
public class SoundManager : MonoBehaviour {
	public AudioClip MoveSound; //le son utilisé avec le mouvement 
	public AudioClip RotateSound; //le son utilisé avec la rotation
	public AudioClip LineDeleteSound; //le son utilisé lorsque on supprime une ligne
	public AudioClip GameMusic; // la musique de jeu 
    public AudioClip NewJokerSound; // le son lorsque on gagne un joker
    
	private static AudioSource X;
	void Start () { // Start est comme un constructeur pour initialiser les attributs
		X = GetComponent<AudioSource> ();
		X.clip = GameMusic;
		X.Play ();
		X.loop = true;
                
	}

	// Update is called once per frame
	void Update () {
		transform.position += new Vector3 (0, -1, 0) * Time.deltaTime;
			son (); 
	}
	public void son () {//Une methode qui active le son selon le deplacment de la tétriminos par example si on deplace a droit on active un son ...etc 
            if ((GameManager.SoundIsOn)) { 
            if ((Input.GetKeyDown (KeyCode.RightArrow))||(Input.GetKeyDown (KeyCode.LeftArrow))) {
			transform.position += new Vector3 (1, 0, 0);
                Move(); 
		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			transform.Rotate (0, 0, -90);
                Move(); 
		}
            }
            /*else {
			GetComponent<AudioSource> ().Pause ();
		}*/
        }
    public void NewJoker() //une methede qui active le son lorsque on prendre un joker 
        {
            X.PlayOneShot(NewJokerSound);
        }
    public void Move()
    {//une methede qui active le son lorsque on Déplacer la pièce (gauche ou droit)
		X.PlayOneShot (MoveSound);
	}
    public void RotateShape()
    {//une methede qui active le son lorsque on fait une rotation
		X.PlayOneShot (RotateSound);
	}
    public void OnLineDelete()
    {//une methede qui active le son lorsque on prendre un ligne est suprrimer
		X.PlayOneShot (RotateSound);
	}
    public static void SetMusic()
    {//une methode qui active le son de jeu
		if (GameManager.MusicIsOn) {
			X.Stop ();
		} else {
			X.Play ();
		}
	}
}
}