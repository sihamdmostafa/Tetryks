using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
namespace AssemblyCSharp {
	public class TestAccountManager : MonoBehaviour {

		public static AccountManager accountManager;

		void Start () {
			accountManager = GetComponent<AccountManager> ();
			Debug.Log (accountManager);
			accountManager.loadAccounts();
			//accountManager.display ();
			//accountManager.newAccount("test");
			accountManager.login("test");
			//accountManager.DisplayCurrentPlayer();

		} // end of Start method
		void Update () {

		}
	} // end of TestAccountManager class

} // end of namespace AssemblyCSharp