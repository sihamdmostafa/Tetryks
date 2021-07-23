using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AssemblyCSharp {
	public class ScoreManager : MonoBehaviour {
		public static int Score = 0; //le score du l'utilisateur
		public static int Level = 1; //le niveau de l'utilisateur
		public static float Speed = 0.8f; //la vitesse de tétriminos
		public static float SpeedBydefault = 0.8f;
		private static int max = 1000; //la sielle du score
		public static double time; //le temps de jeu actuelle
		public static double GameTime = 120;
		public static bool TimeStopped = false;
		public GameObject objet;
		// Use this for initialization
		void Start () {
			Time.timeScale = 1;
			time = GameTime;

		}
		public static void StartGame () {
			Score = 0;
			Level = 1;
		}
		// Update is called once per frame

		void Update () {
			if (!TimeStopped)
				if (GameManager.GameMode == 1) {
					if (time <= 0.0) {
						GameManager.GameIsOver = true;
					} else {
						time -= Time.deltaTime;
					}
				}

		}
		public static void ResetTime () {
			time = GameTime;
			StopTimer ();
		}
		public static void IncreaseScoree (int y) {
			// augmenter le nombre de point (le score) selon le nombre de ligne disparu. 
			switch (y) {
				case 1: //si une ligne est disparu alors le score est augmeter selon la formule donner dans le cahier charge f(p,n) = p(n+1)
					Score += 40 * (Level);
					GameManager.Ui.Score.GetComponent<Animator> ().Play ("New State");
					break;
				case 2: //deux ligne est disparu
					Score += 120 * (Level);
					GameManager.Ui.Score.GetComponent<Animator> ().Play ("New State");
					break;
				case 3: //trois ligne est disparu
					Score += 300 * (Level);
					GameManager.Ui.Score.GetComponent<Animator> ().Play ("New State");
					break;
				case 4: //quatre ligne est disparu c'est le max
					Score += 1200 * (Level);
					GameManager.Ui.Score.GetComponent<Animator> ().Play ("New State");
					break;
				default:
					break;
			}
		}

		public static void StopTimer () {
			TimeStopped = true;
		}

		public static void LevelCheck () { //pour verfier est ce que le score est atteint un certain seill pour augmenter le nivau
			if (GameManager.GameMode == 0)
				if (Score > max * Level) {
					MovementController.UseJokerGame ();
					LevelUp ();

				}
		}
		static void LevelUp () { //pour augmenter le score
			Level++;
			Speed -= 0.2f;
			SpeedBydefault -= 0.2f;
			GameManager.Ui.Level.GetComponent<Animator> ().Play ("New State");

		}

		public static void ResetScore () { //pour rendre le score a 0 lorsque le jeu est fini 
			if (GameManager.accountManager.currentPlayer.getBestScores () > Score) {
				GameManager.onlineRating.AddHighScore (GameManager.accountManager.currentPlayer.getUsername (), ScoreManager.Score);
				GameManager.accountManager.currentPlayer.setScore (Score);
			}
			GameManager.accountManager.currentPlayer.setBestLevel (Level);
			Score = 0;
		}
	}
}