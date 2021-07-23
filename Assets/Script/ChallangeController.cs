using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.UI;

namespace AssemblyCSharp
{

    public class ChallangeController : MonoBehaviour
    {

        // Use this for initialization
        public List<Challange> challanges;
        public static Challange currentone;
        public GameObject element;
        public GameObject elementHolder;
        public bool challagngeDone;
        void Start()
        {
            challanges = new List<Challange>();
            FirstRun();
            currentone = null;
            IntialiseCH();
            challagngeDone = false;

        }

        // Update is called once per frame
        void Update()
        {
            check();
        }
        public void IntialiseCH()
        {
            foreach (var item in challanges)
            {
                var x = Instantiate(element);
                x.transform.SetParent(elementHolder.transform);
                x.transform.localScale = new Vector3(1, 1, 1);
                (x.GetComponent<Button>()).onClick.AddListener(() => { StartCh(challanges.IndexOf(item)); });
                foreach (var item2 in x.GetComponentsInChildren<Text>())
                {
                    if (item2.name == "Text") item2.text = String.Format("Get {0} score in {1}s", item.Score, item.time);
                    else item2.text = String.Format("+{0}", item.reward);
                }

            }
            element.gameObject.SetActive(false);
        }
        void StartCh(int x)
        {
            MovementController.EndGame();
            GameManager.ChallangeMode = true;
            GameManager.GameMode = 1;
            currentone = challanges[x];
            ScoreManager.time = currentone.time;
            ScoreManager.TimeStopped = false;
            GameManager.Ui.StartNewGame();
            GameManager.Ui.CloseChallangeMenu();
        }
        void check()
        {
            if (GameManager.IsStarted)
                if (GameManager.ChallangeMode)
                    if (ScoreManager.Score >= currentone.Score && !challagngeDone)
                    {
                        GameManager.Ui.ChallangeDone(currentone.reward);
                        Debug.Log(GameManager.accountManager.currentPlayer.getNbrSilverJokers());
                        GameManager.accountManager.currentPlayer.setNbrSilverJoker(GameManager.accountManager.currentPlayer.getNbrSilverJokers() + currentone.reward);
                        Debug.Log(GameManager.accountManager.currentPlayer.getNbrSilverJokers());
                        ScoreManager.StopTimer();
                        challagngeDone = true;
                    }
                    else if (ScoreManager.time <= 0 && ScoreManager.Score < currentone.Score)
                    {
                        GameManager.Ui.ChallanageUndone();
                        ScoreManager.StopTimer();
                        GameManager.IsStarted = false;
                    }
                    else if (GameManager.GameIsOver)
                    {
                        GameManager.Ui.ChallanageUndone();
                        ScoreManager.StopTimer();
                        GameManager.IsStarted = false;
                    }

        }

        internal void GameEnded()
        {

        }

        internal void ChDone()
        {

        }
        public void Generate()
        {

        }
        public void SaveCh()
        {
            var firstTime = true;
            FileStream fs;
            if (firstTime)
            {
                fs = new FileStream(Application.persistentDataPath + "/Challanges.dat", FileMode.Create);
            }
            else fs = new FileStream(Application.persistentDataPath + "/Challanges.dat", FileMode.Open);
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(fs, challanges);
            fs.Close();
        }
        public void FirstRun()
        {
            challanges.Add(new Challange(45, 40, 1));
            challanges.Add(new Challange(45, 500, 1));
            challanges.Add(new Challange(60, 5000, 2));
            challanges.Add(new Challange(240, 10000, 3));
            challanges.Add(new Challange(90, 2000, 1));
            challanges.Add(new Challange(240, 7000, 3));
            challanges.Add(new Challange(60, 5000, 2));
            challanges.Add(new Challange(120, 3000, 2));
            challanges.Add(new Challange(360, 20000, 3));
            challanges.Add(new Challange(420, 50000, 4));
            challanges.Add(new Challange(120, 7000, 2));
            challanges.Add(new Challange(120, 2000, 2));
            //SaveCh ();
        }

    }

    [Serializable]
    public class Challange
    {
        public int reward;
        public int time;
        public int Score;
        public bool done;
        public Challange(int time, int score, int reward)
        {
            this.time = time;
            this.Score = score;
            this.reward = reward;
            this.done = false;
        }

    }

}