using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AssemblyCSharp {
    public class UIManager : MonoBehaviour {

        // Cette classe est la classe qui controle les elements visuels du jeu ( les scenes du jeu - les menus..)
        // et les interactions entre eux 
        public GameObject block;
        public Text Score;
        public Text Level;
        public GameObject BlockHolder;
        public Button PauseButton;
        public Button[] SoundButton;
        public Button[] MusicButton;
        public RawImage BackgroundPic;
        public Texture2D[] BGS;
        public Canvas ThemeMenu;
        public Canvas HelpMenu;
        public Canvas MainMenu;
        public Canvas PauseMenu;
        public Canvas TutorielWindow;
        public Canvas GameOverMenu;
        public Sprite[] SoundICons;
        public Sprite[] MusicICons;
        private bool isSet = false;
        private Image layer;
        private Button next;
        public Text TutoText;
        public Canvas Cloud_nbJockers;
        public Canvas NewJocker;

        public Canvas UseJokerMenu;
        public Text NumberOfJockers;
        public Text NumberOfJockers_PopUP;
        public Canvas InGame;
        public Image RC;
        public Image LC;
        public Image S;
        public Image LP;
        public Canvas Settings;
        public Canvas ChallangeDonemenu;
        public Canvas ChallanageMenu;
        public Canvas ProfileMenu;
        public Canvas auto;
        public Canvas manuelle;
        public Canvas Joystick;
        public Canvas AboutUS;
        public Canvas signup;
        public Canvas login;
        public Canvas backexiste;
        public Canvas InfoPlayer;
        public Canvas RatingLocal;
        public Canvas RatingGlobal;
        public Canvas Rating;
        public Image LocalSelected;
        public Image OnlineSelected;
        public Canvas ConnexionFailed;
        public Text NameText;
        public Text ScoreText;
        public Text LevelText;
        public Text JokerText;
        public Canvas BlankName;
        public void ShowBlankName () {
            BlankName.enabled = true;
        }
        public void HideBlankName () {
            BlankName.enabled = true;
        }
        public void Start () {
            // initialisation des composants visuels du jeu 
            CloseJoystick ();
            InGame.enabled = false;
            PauseMenu.enabled = false;
            GameOverMenu.enabled = false;
            Settings.enabled = false;
            layer = TutorielWindow.GetComponentInChildren<Image> ();
            next = TutorielWindow.GetComponentInChildren<Button> ();
            MainMenu.enabled = true;
            TutorielWindow.enabled = false;
            HelpMenu.enabled = false;
            ThemeMenu.enabled = false;
            Score.enabled = true;
            Level.enabled = true;
            ChallangeDonemenu.enabled = false;
            signup.enabled = false;
            InfoPlayer.enabled = false;
            Rating.enabled = false;
            RatingLocal.enabled = false;
            RatingGlobal.enabled = false;
            ConnexionFailed.enabled = false;
        }

        public void ChallanageUndone () {
            ChallangeDonemenu.enabled = true;
            var x = ChallangeDonemenu.GetComponentsInChildren<Canvas> ();
            foreach (Canvas y in x) {
                if (y.name == "Failed") {
                    y.enabled = true;

                } else {
                    if (y.name == "Done")
                        y.enabled = false;
                }

            }
        }

        public void StartTuto () { // pour demarrer le tutoriel
            TutorielWindow.enabled = true;

        }

        // Update is called once per frame
        void Update () {
            UpdateStats (); // pour mettre a jour le score et le niveau
            if (GameManager.isOnTuto) Tuto (); // si le mode tutoriel est activé on avance letape du tuto
            NumberOfJockers.text = GameManager.accountManager.currentPlayer.nbrSilverJokers.ToString (); // on recupere le nombre du joker pour lafficher
            /* if (InGame.enabled) Cloud_nbJockers.enabled = false;
             else Cloud_nbJockers.enabled = true;*/
            NameText.text = GameManager.accountManager.currentPlayer.username;
            ScoreText.text = String.Format (" " + GameManager.accountManager.currentPlayer.getBestScores () + " ");
            LevelText.text = String.Format (" " + GameManager.accountManager.currentPlayer.getBestLevel () + " ");
            JokerText.text = String.Format (" " + GameManager.accountManager.currentPlayer.getNbrSilverJokers () + " ");
        }

        private void UpdateStats () {
            // pour mettre a jour le score et le niveau du jeu
            Score.text = ScoreManager.Score.ToString ("0000");
            switch (GameManager.GameMode) {
                case 0:
                    Level.text = ScoreManager.Level.ToString ("00");
                    break;
                case 1:
                    Level.text = String.Format ("{0:00}", (int) ScoreManager.time / 60) + " : " + String.Format ("{0:00}", (int) ScoreManager.time % 60);
                    break;
            }

        }

        public void StartNewGame () {
            // pour demarer un nouveau partie
            GameManager.IsStarted = true; // on declare que une partie est en cours 
            if (GameManager.isOnTuto) StartTuto (); // pour demarer le tuto
            if (!isSet) { // si la scene du jeu n'est pas prete
                for (int i = 0; i < 18; i++) {
                    for (int j = 0; j < 10; j++) {
                        //on intialise la grille d'arriere plan
                        var x = Instantiate (block, new Vector3 (j, i, 0), Quaternion.identity);
                        x.transform.parent = BlockHolder.transform;
                        x.SetActive (true);
                        x.name = string.Format ("block[{0},{1}]", j, i);
                    }
                }
                isSet = true; // lintialisation est terminé
            }
            GameManager.GameIsOver = false;
            GameManager.firstuse = true;
            PauseMenu.enabled = false; // on desactive les autres elements 
            InGame.enabled = true; // on active les composant visuels necessaires pour le jeu
            BlockHolder.gameObject.SetActive (true);
            GameManager.movementController.enabled = true;
            GameOverMenu.enabled = false;
            GameManager.movementController.StartNewGame ();
            MovementController.Shape.SetActive (true);
            MainMenu.enabled = false;
            PauseButton.gameObject.SetActive (true);
            Score.enabled = true;
            Level.enabled = true;
        }

        public void UseJoker (bool x) {
            // pour afficher le menu du joker si lutilisateur demande
            UseJokerMenu.enabled = x;
        }

        public void PauseGame () {
            // pour mettre le jeu en pause
            if (GameManager.IsStarted) { // on verifie d'abord si une partie est en cours
                if (GameManager.IsPaused) { // si elle est pausé deja ou pas ( pour eviter les conflits)
                    PauseMenu.enabled = false;
                    PauseButton.gameObject.SetActive (true);
                    GameManager.movementController.enabled = true;
                    GameManager.IsPaused = false;
                    ShapeSpawner.NextObject.SetActive (true);
                    Score.enabled = true;
                    Level.enabled = true;

                } else {
                    PauseButton.gameObject.SetActive (false);
                    PauseMenu.enabled = true;
                    GameManager.movementController.enabled = false;
                    GameManager.IsPaused = true;
                    ShapeSpawner.NextObject.SetActive (false);
                    Score.enabled = false;
                    Level.enabled = false;

                }
            }
        }
        public void goHome () {
            // pour revenir vers le menu principale
            CloseJoystick ();
            BlockHolder.gameObject.SetActive (false); //block
            block.SetActive (false);
            //MovementController.Shape.SetActive (false);
            //MovementController.EndGame ();
            GameManager.movementController.enabled = false;
            //  MovementController.Shape.SetActive (false);
            Score.enabled = false;
            Level.enabled = false;
            MainMenu.enabled = true;
            PauseMenu.enabled = false;
            PauseButton.gameObject.SetActive (false);
            GameManager.IsStarted = false;
            GameManager.IsPaused = false;
            GameOverMenu.enabled = false;
            ScoreManager.ResetTime ();
        }
        public void Sound () {
            // controller le son du jeu
            Debug.Log ("Sound is on 1 : " + GameManager.SoundIsOn);
            if (GameManager.SoundIsOn) {
                foreach (var item in SoundButton) {
                    var x = item.GetComponent<Image> (); // pour mettre a jour les icones vers l'icone de pas du son
                    x.sprite = SoundICons[0];
                }
                GameManager.SoundIsOn = false;
            } else {
                foreach (var item in SoundButton) {
                    var x = item.GetComponent<Image> ();
                    x.sprite = SoundICons[1];
                }
                GameManager.SoundIsOn = true;
            }
            Debug.Log ("Sound is on 2 : " + GameManager.SoundIsOn);
        }

        public void Music () {
            // pour controller la musique du jeu
            if (GameManager.MusicIsOn) {
                foreach (var item in MusicButton) {
                    var x = item.GetComponent<Image> ();
                    x.sprite = MusicICons[0];
                }
                GameManager.MusicIsOn = false;

                SoundManager.SetMusic ();
            } else {
                foreach (var item in MusicButton) {
                    var x = item.GetComponent<Image> ();
                    x.sprite = MusicICons[1];
                }
                GameManager.MusicIsOn = true;

                SoundManager.SetMusic ();
            }

        }
        public void Tuto () {
            // le tutoriel guidé du jeu 
            var s1 = "Tap on the Left of the Screen to Move the shape to the left."; // les etapes du tuto
            var we = "Well done: ";
            var s2 = "Tap on the right of the Screen to Move the shape to the right.";
            var s3 = "Now Swipe to any directions to rotate the shape";
            var s4 = "to Speed up click and hold on the screen";
            var wx = "You are ready now!";
            var jo = "There are Jokers to help you, you can use them to continue playing";
            RC.enabled = false;
            LC.enabled = false;
            S.enabled = false;
            Debug.Log (GameManager.tutoStep);
            switch (GameManager.tutoStep) { // pour derouler le tuto
                case "Left":
                    TutoText.text = s1;
                    layer.enabled = true;
                    Debug.Log ("herrre");
                    GameManager.canTouch = true;
                    next.gameObject.SetActive (false);
                    layer.rectTransform.anchoredPosition = new Vector2 (-270, 0);
                    LC.enabled = true;
                    break;
                case "Right":
                    TutoText.text = s2;
                    layer.enabled = true;
                    GameManager.canTouch = true;
                    next.gameObject.SetActive (false);
                    layer.rectTransform.anchoredPosition = new Vector2 (270, 0);
                    RC.enabled = true;
                    break;
                case "Rotate":
                    TutoText.text = s3;
                    layer.rectTransform.sizeDelta = new Vector2 (1200, 2078);
                    layer.rectTransform.anchoredPosition = new Vector2 (0, 0);
                    layer.enabled = true;
                    GameManager.canTouch = true;
                    next.gameObject.SetActive (false);
                    S.enabled = true;
                    break;
                case "Speed":
                    TutoText.text = s4;
                    layer.rectTransform.sizeDelta = new Vector2 (1200, 2078);
                    layer.rectTransform.anchoredPosition = new Vector2 (0, 0);
                    layer.enabled = true;
                    GameManager.canTouch = true;
                    next.gameObject.SetActive (false);
                    break;
                case "DoneStep":
                    next.gameObject.SetActive (true);
                    GameManager.canTouch = false;
                    TutoText.text = we + GameManager.accountManager.currentPlayer.getUsername ();
                    layer.enabled = false;
                    break;
                case "Joker":
                    next.gameObject.SetActive (true);
                    GameManager.canTouch = false;
                    TutoText.text = jo;
                    layer.enabled = true;
                    break;
                case "END":
                    next.gameObject.SetActive (true);
                    GameManager.canTouch = true;
                    TutoText.text = wx;
                    layer.enabled = false;
                    GameManager.isOnTuto=false;
                    break;
                case "OnSpeed":
                    TutoText.text = "Now Release your finger";
                    layer.enabled = false;
                    break;
            }
        }

        public void NextButtonClick () { // pour avancer le tuto vers letape suivante
            if (GameManager.tutoStep != "END") // si le tuto n'a pas arrivé vers la fin
                GameManager.tutoStep = GameManager.NextStep; // on avance
            else { //sinon
                GameManager.isOnTuto = false; // le tuto est terminé    
                TutorielWindow.enabled = false; // on ferme la fenetre du tuto

            }
        }
        public void GameOver () {
            // pour terminer le jeu 
            GameOverMenu.enabled = true; // afficher le menu du gameOver
            foreach (var item in GameOverMenu.GetComponentsInChildren<Text> ()) {
                if (item.name == "Score") item.text = ScoreManager.Score.ToString (); // afficher le score et le niveau 
                else if (item.name == "Level") item.text = ScoreManager.Level.ToString ();
            }
            MovementController.EndGame ();
            GameManager.movementController.enabled = false;
        }
        // les methodes qui sont en dessus sont excutés lorsque un button est cliqué
        public void GoHomeOverButton () { // button pour revenir vers le menu principal
            goHome ();
        }

        public void ShowSettings () { //button pour afficher la fenetre des parametres
            Settings.enabled = true;
        }

        //---------------- Jocker Partie ------
        public void OpenNewJoker (int reward) {
            //button pour afficher la fenetre du attribution d'un nouveau joker
            NewJocker.enabled = true;
            var x = GameObject.FindGameObjectWithTag ("JokerNumber").GetComponent<Text> ();
            x.text = "+" + reward.ToString ();
        }
        public void OpenNewJoker () {
            //button pour afficher la fenetre du nouveau joker
            NewJocker.enabled = true;
            var x = GameObject.FindGameObjectWithTag ("JokerNumber").GetComponent<Text> ();
            x.text = "+1";

        }
        public void CloseNewJocker () {
            //button pour fermer la fenetre du newjoker
            NewJocker.enabled = false;
        }
        public void OpenUseJocker () {
            //button pour afficher la fenetre dutilisation du joker
            UseJokerMenu.enabled = true;
            NumberOfJockers_PopUP.text = GameManager.accountManager.currentPlayer.getNbrSilverJokers ().ToString ();

        }
        public void CloseUseJocker () {
            //button pour fermer la fenetre du joker 
            UseJokerMenu.enabled = false;
        }

        public void HideSettings () {
            //button pour fermer la fenetre des parameteres
            Settings.enabled = false;

        }
        //---------------------------Help-----------------------------------//
        //--------------- Afficher et fermer le menu d'aide-----------------//
        public void OpenHelpMenu () {
            HelpMenu.enabled = true;
        }
        public void HideHelpMenu () {
            HelpMenu.enabled = false;
        }
        //-----------------------themcanvas-------------------------------------------//
        //--------------- Afficher et fermer le menu des themes-----------------//
        public void OpenThemeMenu () {
            ThemeMenu.enabled = true;
        }
        public void HideThemeMenu () {

            ThemeMenu.enabled = false;
            auto.enabled = false;
            manuelle.enabled = false;
        }
        //--------------- les challanges-----------------//

        public void ChallangeDone (int reward) {
            OpenRewardMenu (reward);
            GameManager.movementController.enabled = false;
        }

        public void OpenRewardMenu (int reward) {
            ChallangeDonemenu.enabled = true;
            var x = ChallangeDonemenu.GetComponentsInChildren<Canvas> ();
            foreach (Canvas y in x) {
                if (y.name == "Done") {
                    y.enabled = true;

                    var texts = y.GetComponentsInChildren<Text> ();
                    foreach (Text text in texts) {
                        if (text.name == "Reward") {
                            text.text = String.Format ("+{0}", ChallangeController.currentone.reward);
                        }
                    }
                } else {
                    if (y.name == "Failed") y.enabled = false;
                }

            }
        }

        public void CloseDoneChallange () {
            ChallangeDonemenu.enabled = false;
            GameManager.challangeController.ChDone ();
            OpenChallangeMenu ();
        }

        public void OpenChallangeMenu () {
            ChallanageMenu.enabled = true;
        }
        public void CloseChallangeMenu () {
            ChallanageMenu.enabled = false;
        }

        public void OpenJoystick () {
            Joystick.enabled = true;
        }
        public void CloseJoystick () {
            Joystick.enabled = false;
        }
        public void ControleJoystick () {
            if ((!Joystick.isActiveAndEnabled) && (InGame.isActiveAndEnabled)) OpenJoystick ();
            else CloseJoystick ();
        }
        public void CloseAboutUs () {
            AboutUS.enabled = false;
        }
        public void OpenAboutUs () {
            AboutUS.enabled = true;
        }
        public void disablechalnge () {
            ChallangeDonemenu.enabled = false;
        }

        public void Getsignup () {
            signup.enabled = true;
        }
        public void BackSignup () {
            signup.enabled = false;

        }
        public void Getlogin () {
            login.enabled = true;
        }
        public void Backlogin () {
            login.enabled = false;

        }
        public void backexist () {
            backexiste.enabled = false;

        }
        public void getprofil () {
            InfoPlayer.enabled = true;
        }
        public void backprofil () {
            InfoPlayer.enabled = false;
        }
        public void getrating () {
            Rating.enabled = true;
            ShowLocalRating ();
        }
        public void backrating () {
            Rating.enabled = false;

        }
        public void ShowLocalRating () {
            RatingLocal.enabled = true;
            OnlineSelected.enabled = false;
            LocalSelected.enabled = true;
            GameManager.accountManager.Intialise ();

        }
        public void HideLocalRating () {
            RatingLocal.enabled = false;
        }
        public void ShowOnlineRating () {
            RatingGlobal.enabled = true;
            RatingLocal.enabled = false;
            OnlineSelected.enabled = true;
            LocalSelected.enabled = false;
            GameManager.onlineRating.Afficher ();
        }
        public void backglobalrating () {
            RatingGlobal.enabled = false;
        }
        public void HideFailedConnexion () {
            ConnexionFailed.enabled = false;
            ShowLocalRating ();
        }
        public void ShowConnexionFailed () {
            ConnexionFailed.enabled = true;
        }

    }
}