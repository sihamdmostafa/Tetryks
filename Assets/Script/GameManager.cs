using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AssemblyCSharp {

    public class GameManager : MonoBehaviour {
        // le Game Manager c'est lelement le plus important car il est le noyeau du jeu
        private UserInput userInput;
        private SoundManager soundManager;
        public static MovementController movementController;
        public static ChallangeController challangeController;
        public static AccountManager accountManager;
        public static OnlineRating onlineRating;
        public static Colors colors;
        public static Joker jocker;
        public static UIManager Ui;
        private ShapeSpawner shapeSpawner;
        // ces booleen sont utilisé pour indiquer l'etat actuelle du jeu
        public static bool GameIsOver; // si le partie est terminé ou non
        public static bool IsPaused; // si le jeu est en pause
        public static bool IsStarted; // si une partie est en cours
        public static bool SoundIsOn; // si le son est activé
        public static bool MusicIsOn; // si le son n'est pas activé
        public static bool isOnTuto; // si on est en mode tutoriel
        public static string tutoStep; // letat du tutoriel
        public static string NextStep; // letat suivante du tuto 
        public static bool canTouch; // si on doit capter les gestes
        public static int jock_nb = 5; // le nombre du joker
        public static int GameMode = 0; // le mode du jeu
        public static bool ChallangeMode; // si on est dans le mode du challange
        public static bool firstuse;
        public static Stack<Canvas> OpenedWindow;
        void Start () {
            // initialisation du composants
            movementController = GetComponent<MovementController> ();
            challangeController = GetComponent<ChallangeController> ();
            accountManager = GetComponent<AccountManager> ();
            colors = GetComponent<Colors> ();
            jocker = GetComponent<Joker> ();
            Ui = GetComponent<UIManager> ();
            onlineRating = GetComponent<OnlineRating> ();
            // pour demmarer le jeu dans les parameteres par defaut
            movementController.enabled = false;
            GameIsOver = false;
            IsPaused = false;
            IsStarted = false;
            SoundIsOn = true;
            MusicIsOn = true;
            isOnTuto = false;
            canTouch = true;
            tutoStep = "Left";
            ChallangeMode = false;
            firstuse = true;
            firstRun ();
        }

        // Update is called once per frame
        void Update () {
            if (!ChallangeMode) isGameOver (); // on verifier chaque fois si le jeu est termnié ou non 
            else CheckChallange (); // pour verifier l'etat du challange actuelle
            updateHighScore ();
        }

        void CheckChallange () {
            if (GameIsOver) {
                //  challangeController.check();
            }
        }
        public void StartNewGame () {
            firstuse = true;
            GameMode = 0;
            MovementController.EndGame ();
            Ui.StartNewGame ();
            ChallangeMode = false;
           // loading.SetcurrentAmount();

        }

        public void ChangeGameMode () {
            // pour changer le mode du jeu 
            // 0 cest le mode classique
            // 1 c'est le mode time attack
            GameMode = 1;
            MovementController.EndGame ();
            Ui.StartNewGame (); // demarrer un nouveau partie du jeu
            ScoreManager.TimeStopped = false;
            ChallangeMode = false;
        }
        void isGameOver () {
            /* 
                        // si le jeu est terminé
                        if (IsStarted) {
                            if (GameIsOver) {
                                if (GameMode == 0) {
                                    if (!Ui.UseJokerMenu.isActiveAndEnabled) { //si l'utilisateur veut utiliser un joker
                                        Ui.OpenUseJocker ();
                                        if (!(jocker.UsedJoker) && (GameIsOver)) { // si lutilisateur ne veut pas utiliser un joker
                                            movementController.enabled = false;
                                        } else {
                                            jocker.UsedJoker = false;
                                            GameIsOver = false;
                                        }
                                    }
                                } else { // sinon on termine le jeu et on affiche le menu de game over
                                    Debug.Log ("lik lik");
                                    Ui.GameOver ();
                                    GameIsOver = false;
                                    IsStarted = false;
                                    GameIsOver = false;
                                }
                            }
                        }
                        */
            if ((GameIsOver) && (firstuse)) {
                if ((!Ui.UseJokerMenu.isActiveAndEnabled) && (firstuse)) {
                    Ui.OpenUseJocker ();
                    if (!(jocker.UsedJoker) && (GameIsOver)) {
                        movementController.enabled = false;
                    } else {
                        jocker.UsedJoker = false;
                        GameIsOver = false;
                    }
                    firstuse = false;
                }
            }
            if ((GameIsOver) && (!firstuse) && (!Ui.UseJokerMenu.isActiveAndEnabled)) {
                GameIsOver = false;
                movementController.enabled = false;
                Ui.InGame.enabled = false;
                Ui.UseJokerMenu.enabled = false;
                Ui.GameOver ();
            }
        }

        public void updateHighScore () {
            if (GameIsOver) {
                accountManager.currentPlayer.setScore (ScoreManager.Score);
            }
        }

        public static void CloseWindow () {
            Application.Quit ();
        }
        public void firstRun () {
            int hasPlayed = PlayerPrefs.GetInt ("FirstTimeT");

            if (hasPlayed == 0) {
                isOnTuto = true;
                PlayerPrefs.SetInt ("FirstTimeT", 1);
            }
        }
    }
}