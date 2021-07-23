using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AssemblyCSharp {
	public class Joker : MonoBehaviour {
		public bool gameOver; // un booleen pour indiquer si le jeu est terminé 
		public bool UsedJoker; // un booleen pour indiquer si on a deja utilisé un joker dans la partie courante

		void Start () {
			UsedJoker = false;
		}

		void Update () { }
		/* la methode ifaday retourne un booleen qui indique si le joueur a le droit d'un nouveau joker ou pas, elle fait un comparaise 
		 entre les deux dates en entrés*/
		public bool ifaday (DateTime last_login, DateTime current_login) {
			bool day_bool = false;
			if ((current_login.Year == last_login.Year) && ((current_login.DayOfYear - last_login.DayOfYear) == 1)) day_bool = true;
			else if (current_login.Year != last_login.Year) {
				if ((current_login.Year - last_login.Year) == 1) {

					if ((current_login.DayOfYear + last_login.DayOfYear - last_login.DayOfYear) == 1) day_bool = true;

				}
			}
			if (day_bool) {

				if (current_login.TimeOfDay >= last_login.TimeOfDay) return true;
			}
			return false;
		}
		/*la methode CheckDate utilise la methode ifaday, elle retourne un booleen qui indique si le joueur a le droit d'avoir un nouveau joker
		 * en comparant la date actuelle avec la derniere fois il avait un joker */
		public bool CheckDate (DateTime LastTime) {
			var t = DateTime.Now;
			if (ifaday (LastTime, t)) {
				GameManager.accountManager.currentPlayer.nbrSilverJokers++;
				GameManager.Ui.NewJocker.enabled = true;
				return true;
			} else return false;
		}
		/* cette methode vide la grille apr utilisation de joker elle manipule le "Ui items" */
		public void EmptyGrid () {
			if (GameManager.accountManager.currentPlayer.getNbrSilverJokers () > 0) {
				GameManager.GameIsOver = false;
				if ((GameManager.accountManager.currentPlayer.nbrSilverJokers) != 0) UsedJoker = true;
				else UsedJoker = false;
				if (UsedJoker) {
					GameManager.Ui.UseJokerMenu.enabled = false;
					GameManager.movementController.enabled = true;
					MovementController.UseJokerGame ();
					UsedJoker = false;
					GameManager.accountManager.currentPlayer.nbrSilverJokers--;

				}
			}
		}
	}
}