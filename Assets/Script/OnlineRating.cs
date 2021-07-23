using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;
namespace AssemblyCSharp {
    public class OnlineRating : MonoBehaviour {
        public GameObject element;
        public GameObject elementHolder;
        private List<GameObject> elem1 = new List<GameObject> ();
        private string webAddr = @"http://app.creepily33.hasura-app.io/";

        // Use this for initialization
        void Start () {
            //AddHighScore("Salah",1500);
            GetScoreList ();
            Afficher ();

        }
        public void AddHighScore (string x, int y) {
            try {
                Debug.Log (y);
                var httpWebRequest = (HttpWebRequest) WebRequest.Create (webAddr + "add");
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";
                var HF = new Rating (x, y);
                using (var streamWriter = new StreamWriter (httpWebRequest.GetRequestStream ())) {
                    var json = JsonConvert.SerializeObject (HF);

                    streamWriter.Write (json);
                    streamWriter.Flush ();
                }
                var httpResponse = (HttpWebResponse) httpWebRequest.GetResponse ();
                using (var streamReader = new StreamReader (httpResponse.GetResponseStream ())) {
                    var responseText = streamReader.ReadToEnd ();
                    Debug.Log (responseText);

                    //Now you have your response.
                    //or false depending on information in the response     
                }
            } catch (WebException ex) {
                Debug.Log (ex.Message);
            }

        }
        List<Rating> GetScoreList () {
            System.Net.ServicePointManager.ServerCertificateValidationCallback +=
           delegate (object sender, System.Security.Cryptography.X509Certificates.X509Certificate certificate,
                                   System.Security.Cryptography.X509Certificates.X509Chain chain,
                                   System.Net.Security.SslPolicyErrors sslPolicyErrors)
           {
               return true; // **** Always accept
           };
            try {
                WebRequest request = WebRequest.Create (webAddr);
                WebResponse rep = request.GetResponse ();
                Stream t = rep.GetResponseStream ();
                StreamReader IO = new StreamReader (t);
                return JsonConvert.DeserializeObject<List<Rating>> (IO.ReadToEnd ());
            } catch (Exception ex) {
                Debug.Log(ex);
                GameManager.Ui.ShowConnexionFailed ();
                return null;

            }
        }
        // Update is called once per frame
        void Update () {

        }
        public void Afficher () {
            var list = GetScoreList ();
            Debug.Log(list);
            var lengths = (from element in list orderby element.score select element).OrderByDescending (c => c.score);;
            var i = 0;
            foreach (var x in elem1) {
                Destroy (x);
            }
            element.SetActive (true);
            foreach (var item in lengths) {
                var x = Instantiate (element);
                x.transform.SetParent (elementHolder.transform);
                x.transform.localScale = new Vector3 (1, 1, 1);
                elem1.Add (x);

                foreach (var item2 in x.GetComponentsInChildren<Text> ()) {
                    if (item2.name == "Text")
                        item2.text = item.name;
                    else if (item2.name == "num") {
                        item2.text = i.ToString ();
                        i++;
                    } else
                        item2.text = item.score.ToString ();
                }

            }
            element.SetActive (false);
        }
    }
    public class Rating {
        public string name;
        public int score;
        public Rating (string name, int score) {
            this.name = name;
            this.score = score;
        }
    }
}