using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AssemblyCSharp {
    public class UserInput : MonoBehaviour {

        public float timeDelayThreshold; // Pour determiner la durée de longue tap sur l'ecran

        private float timePressed; // pour sauvgarder le temps de touche 
        private float timeLastPress; // pour sauvgarder le dernier temps de touche 
        private Vector2 fp; // pour sauvgarder la position du touche
        private Vector2 lp;
        private MovementController movementController;
        private bool longpress; // pour indiquer si on a une touche longue durée sur lecran
        void Start () {
            // intialisation des variables
            this.timePressed = 0.0f;
            this.timeLastPress = 0.0f;
            this.fp = Vector2.zero; // Vector2.Zero == (0,0);
            this.lp = Vector2.zero;
            movementController = GetComponent<MovementController> (); // pour recupurer le controlleur du jeu
            longpress = false;
        }

        void Update () {
            if (!GameManager.IsPaused)
                if (GameManager.canTouch) InputDetection (); // si le jeu est en cours alors on capte les gestes
        }

        void InputDetection () {
            Touch touch;
            float dragDistance = Screen.height * 15 / 100;
            // pour savoir la taille d'ecran pour determiner la taille de swipe 
            if (Input.touchCount > 0) { //si on a une touche sur l'écran
                touch = Input.GetTouch (0); // on recupere cette touch
                if (touch.phase == TouchPhase.Began) { // si on a une touche (debut du touche)
                    timeLastPress = Time.time; // on sauvgarde le temps de debut de touche
                    fp = touch.position; // on sauvgarde la position du touche
                    lp = touch.position;
                } else if (touch.phase == TouchPhase.Moved) // si le doigt est bougé sur l'ecran
                {
                    lp = touch.position; // mise a jour la position de doigt
                } else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) // si la touche est terminée
                {
                    if (!longpress) {
                        // si on n'a pas une appui longue 
                        lp = touch.position; //on sauvgarde la derniere position du doigt sur l'ecran
                        if (Mathf.Abs (lp.x - fp.x) > dragDistance || Mathf.Abs (lp.y - fp.y) > dragDistance) //on verifie si on a une glissement sur l'ecran
                        {
                            // on a une glissement sur l'ecran
                            movementController.RotateShape ();

                        } else { // // on a une touche courte sur l'ecran
                            if (touch.position.x > (Screen.width / 2)) // on verifie l'emplacement du touche 
                            {
                                // une touche sur le cote droit de l'ecran
                                movementController.MoveRight ();
                            } else {
                                // une touche sur le cote gauche de l'ecran
                                movementController.MoveLeft ();
                            }

                        }
                    } else {
                        longpress = false;
                        movementController.CancelSpeedUp (); // lutilisateur a laché son doigt de lecran 
                    }
                } else {
                    if (touch.phase == TouchPhase.Stationary) { // si lutilisateur n'a pas bouger son doigt du l'ecran
                        timePressed = Time.time - timeLastPress; // on calcule le temps du touche
                        if (timePressed > timeDelayThreshold) // si le temps du touche est superieur a le temps du longue touche
                        {
                            Debug.Log ("This is a long Tap"); // on une appui longue sur l' ecran
                            longpress = true;
                            movementController.SpeedUp ();
                        }
                    }

                }
            } else if (Input.GetKeyDown (KeyCode.Escape)) {
                GameManager.CloseWindow();
            } else {
                // pour les controles pour la version desktop et Web
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    movementController.MoveLeft();
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    movementController.MoveRight();
                }
                else if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    movementController.RotateShape();
                }
                   else if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    movementController.SpeedUp();
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    movementController.CancelSpeedUp();
                }
                
                
            }
        }
    }
}