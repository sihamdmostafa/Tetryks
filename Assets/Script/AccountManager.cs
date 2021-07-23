using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp {
	[Serializable]
	public class AccountManager : MonoBehaviour {
		public GameObject element;
		public GameObject elementHolder;
		public GameObject element1;
		public GameObject elementHolder1;
		public InputField field;
		//public Button b1 ;
		public static AccountManager accountManager;
		private List<Account> accounts = new List<Account> ();
		private List<GameObject> elem1 = new List<GameObject> ();
		// private bool firstTime = true;
		public Account currentPlayer = null;
		private Text nom;
		private Button btn;
		public Canvas logincv;
		public Canvas signcv;
		public Canvas exist;

		void Start () {
			exist.enabled = false;
			//newAccount ();
			loadAccounts ();
			this.initialiselist ();
			//	this.Intialise (); 
			misAjour ();

		}

		void Update () {

			saveAccounts ();
		}

		public void newAccount () {

			var trouv = false;
			Debug.Log (field.text);
			if (string.IsNullOrEmpty (field.text)) {
				Debug.Log ("here");
				GameManager.Ui.ShowBlankName ();
			} else {
				foreach (Account user in accounts) {
					if (user.getUsername ().ToLower () == (field.text).ToLower ()) {
						trouv = true;
						field.text = "";
						break;
					}

				}
				if (trouv == true) {
					signcv.enabled = true;
					exist.enabled = true;
				} else {
					currentPlayer = new Account (field.text);
					accounts.Add (currentPlayer);
					currentPlayer.setNbrSilverJoker (5);
					DateTime now = DateTime.Now;
					currentPlayer.setLastLogin (now);
					saveAccounts ();
					logincv.enabled = false;
					signcv.enabled = false;
					misAjour ();
					this.initialiselist ();
					//        this.Intialise();
				}
			}
		}
		public void misAjour () {

		}

		public void login (String username) {

			Account aUser = null;
			bool found = false;
			foreach (Account user in accounts) {
				if (user.getUsername ().Equals (username)) {
					found = true;
					aUser = user;
					break;
				}
			}
			if (found) {

				currentPlayer = aUser;
				GameManager.onlineRating.AddHighScore (currentPlayer.getUsername (), currentPlayer.getBestScores ());
				Debug.Log ("Last login was : " + currentPlayer.lastLogin);
				if (GameManager.jocker.CheckDate (currentPlayer.lastLogin)) currentPlayer.lastLogin = DateTime.Now;
				Debug.Log ("las login is : " + currentPlayer.lastLogin);
				this.Intialise ();
				logincv.enabled = false;
			}

		}
		// end of login

		public void saveAccounts () {
			FileStream fs = new FileStream (Application.persistentDataPath + "/AccountsRecord", FileMode.Open);
			BinaryFormatter bf = new BinaryFormatter ();
			bf.Serialize (fs, accounts);
			fs.Close ();
		}

		public void loadAccounts () {
			FileStream fs;
			if (!File.Exists (Application.persistentDataPath + "/AccountsRecord")) {
				fs = new FileStream (Application.persistentDataPath + "/AccountsRecord", FileMode.Create);
				Debug.Log ("here");
				//			firstTime = false;
			} else {
				fs = new FileStream (Application.persistentDataPath + "/AccountsRecord", FileMode.Open);
				BinaryFormatter bf = new BinaryFormatter ();
				accounts = (List<Account>) bf.Deserialize (fs);
				Debug.Log ("accounts found");
			} // end of if else
			fs.Close ();
		}
		// end of method loadAccounts

		public void initialiselist () {
			element.SetActive (true);
			foreach (var item in accounts) {
				var x = Instantiate (element);
				x.transform.SetParent (elementHolder.transform);
				x.transform.localScale = new Vector3 (1, 1, 1);
				x.GetComponent<Button> ().onClick.AddListener (() => {
					login (item.username);
				});

				foreach (var item2 in x.GetComponentsInChildren<Text> ()) {
					if (item2.name == "Text")
						item2.text = String.Format (item.username);
				}
			}
			element.SetActive (false);
		}

		public void Intialise () {
			int i = 1;
			foreach (var x in elem1) {
				Destroy (x);
			}
			//misAjour ();
			element1.SetActive (true);

			//loadAccounts()
			foreach (var item in accounts) {
				var x = Instantiate (element1);
				x.transform.SetParent (elementHolder1.transform);
				x.transform.localScale = new Vector3 (1, 1, 1);
				elem1.Add (x);

				foreach (var item2 in x.GetComponentsInChildren<Text> ()) {
					if (item2.name == "Text")
						item2.text = String.Format (item.username);
					else if (item2.name == "num") {
						item2.text = String.Format ("" + i + "");
						i++;
					} else
						item2.text = String.Format (" " + item.getBestScores () + " ");
				}

			}
			element1.SetActive (false);
		}

	}
	// end of class AccountsManager
}
// end of namespace AssemblyCSharp