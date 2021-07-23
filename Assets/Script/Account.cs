using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
namespace AssemblyCSharp {
	[Serializable]
	public class Account {
		public string username;
		public DateTime lastLogin;
		private DateTime newLogin;
		private int successiveLoginsCpt;
		private int nbrGoldenJokers;
		public int nbrSilverJokers;
		private int bestLevel;
		private int bestScores = 0;
		public Account (String username) {
			this.username = username;
			successiveLoginsCpt = 0;
			nbrGoldenJokers = 0;
			nbrSilverJokers = 0;
			bestLevel = 0;
			bestScores = 0;
		}

		public string getUsername () {
			return username;
		}

		public DateTime getLastLogin () {
			return lastLogin;
		}

		public DateTime getNewLogin () {
			return newLogin;
		}
		public int getSuccessiveLoginsCpt () {
			return successiveLoginsCpt;
		}

		public int getNbrGoldenJokers () {
			return nbrGoldenJokers;
		}

		public int getNbrSilverJokers () {
			return nbrSilverJokers;
		}

		public int getBestLevel () {
			return bestLevel;
		}

		public int getBestScores () {
			return bestScores;
		}

		public void setUsername (string username) {
			this.username = username;
		}

		public void setLastLogin (DateTime lastLogin) {
			this.lastLogin = lastLogin;
		}

		public void setNewLogin (DateTime newLogin) {
			this.newLogin = newLogin;
		}

		public void setSuccessiveLoginsCpt (int successiveLoginsCpt) {
			this.successiveLoginsCpt = successiveLoginsCpt;
		}

		public void setNbrGoldenJoker (int nbrGoldenJokers) {
			this.nbrGoldenJokers = nbrGoldenJokers;
		}

		public void setNbrSilverJoker (int nbrSilverJokers) {
			this.nbrSilverJokers = nbrSilverJokers;
		}

		public void setBestLevel (int bestLevel) {
			if (bestLevel > this.bestLevel)
				this.bestLevel = bestLevel;
		}

		public void setScore (int score) {
			if (this.bestScores < score) {
				this.bestScores = score;
			}
		}

		public void display () {
			Debug.Log (getBestScores ());
		}
	}
} // end of class Account
// end of namespace AssemblyCSharp