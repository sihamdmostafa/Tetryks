using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
namespace AssemblyCSharp
{
    public class MovementController : MonoBehaviour
    {
        // Use this for initialization
        public int tailleX; //la taille X de la grille =10
        public int tailleY; //la taille Y de la grille =20
        public static GameObject Shape; // le tetriminos qu'on doit controler
        private Rigidbody2D rb2D; //le controlleur du mouvement 
        private float CurrentTime = 0.0f; // le temps actuel
        private float LastTime = 0.0f; //pour comparer le temps 
        private static Transform[,] GameGrid = new Transform[10, 22]; // la matrice du jeu;
        private static bool CanMove = true; // si le tetriminos peut descendre
        public ShapeSpawner SS; // le controlleur qui contient la liste du tetriminos
        private static float x = 1.0f;
        public Image goodimg;
        public Image excellencimg;
        public Image greatimg;
        public Image perfectimg;

        void Start()
        {
            // c'est la fonction de initialisation ( comme si un constructor )
            goodimg.enabled = false;
            excellencimg.enabled = false;
            greatimg.enabled = false;
            perfectimg.enabled = false;
        }
        public void StartNewGame()
        {
            for (int i = 0; i < 20; i++)
                for (int j = 0; j < 10; j++) GameGrid[j, i] = null; // initialiser la grille du jeu
            SS = GetComponent<ShapeSpawner>(); // recuperer le controlleur du tetriminos    
            SS.SpawnShape(); // pour lancer le premier tetriminos dans le partie actuel

            Shape = SS.CurrentObject; // pour lancer le premier tetriminos dans le partie actuel

            CanMove = true;
            rb2D = Shape.GetComponent<Rigidbody2D>(); // pour recupurer le controlleur du tetriminos
        }
        void MoveDown()
        {
            //desencdre le tetriminos un pas
            Vector2 start = Shape.transform.position; // la position actuelle de tetriminos
            Vector2 end = start + new Vector2(0, -1); // la position ou on doit deplacer tetriminos
            CurrentTime = Time.time; //pour recupurer le temps actuel
            if (CurrentTime - LastTime + 0.1f > ScoreManager.Speed)
            {
                SmoothMovement(end); // deplacer le tetriminos de Start vers end
                if (!MovementIsPossible()) // on verifie si la movement est juste
                { //si la mouvement n'est pas juste 
                    // on annule le deplacement
                    SmoothMovement(start);
                    CanMove = false; // le tetriminos ne peut pas bouger , on doit lancer un autre;
                }
                UpdateGameGrid(); // on mettre a jour le matrice du jeu
                LastTime = Time.time; //on mettre a jour le temps du dernier deplacement

            }
        }
        bool MovementIsPossible() // pour verifier si la movement est juste
        {
            var TT = Shape.GetComponentsInChildren<Transform>(); //on recupere les 4 block du tetriminos
            foreach (Transform child in TT)
            {
                if (!IsInBorders(child.transform)) { return false; } //si un block est hors la grille donc la movement n'est pas juste
                var x = (int)Mathf.Round(child.position.x);
                var y = (int)Mathf.Round(child.position.y) - 1;
                try
                {
                    //on verifie si la positon actuel est vide (aucun tetriminos a la meme position)
                    if (GameGrid[x, y] != null && (GameGrid[x, y].parent != Shape.transform)) { return false; }
                }
                catch (IndexOutOfRangeException)
                {
                    return false;
                }
            }
            return true;
        }
        void UpdateGameGrid()
        {
            //pour mettre a jour le matrice du jeu apres chaque deplacement
            var TT = Shape.GetComponentsInChildren<Transform>(); // pour recepurer chaque bloque du tetriminos
            for (int y = 0; y < 20; y++)
            {
                for (int x = 0; x < 10; x++)
                {
                    //pour supprimer la derniere position du tetriminos actuel
                    if ((GameGrid[x, y] != null) && (GameGrid[x, y].transform.parent == Shape.transform))
                    {
                        GameGrid[x, y] = null;
                    }
                }
            }

            foreach (Transform child in TT)
            {
                // on place le tetriminos dans la nouvelle positiion
                var x = (int)Mathf.Round(child.position.x);
                var y = (int)Mathf.Round(child.position.y);
                if (GameGrid[x, y] == null)
                    GameGrid[x, y] = child;
                else
                {
                    GameGrid[x, y] = child;
                }
            }
        }

        public void CancelSpeedUp()
        {
            // cette methode est utlisé pour remettre la vitesse du tetriminos a letat normal
            // lorsque lutlisisateur release son doigt du lecran
            GameManager.tutoStep = "Joker"; // pour controler letat du tutoriel
            GameManager.NextStep = "END";
            ScoreManager.Speed = ScoreManager.SpeedBydefault; // lindicateur de vitesse 
        }

        void SmoothMovement(Vector3 end)
        {

            // si le temps ente le temps actuel et le temps du dernier deplacement est superieur de le temps du deplacement
            Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, 1f); //on calcule la nouvelle position 
            rb2D.MovePosition(newPostion); // on applique le mouveement sur le controlleur

        }
        bool IsInBorders(Transform t)
        {
            //on verifie si le tetriminos est dans la grille du jeu
            if ((int)t.position.x >= 0)
            {
                if (t.position.x < 10.0f)
                {
                    if (t.position.y > 0.0f)
                    {
                        if (t.position.y < tailleY)
                        {
                            return true;
                        }
                    }
                }
                return false;
            }
            else
            {
                return false;
            }
        }

        void Move(int xDir)
        {
            //pour deplacer le tetriminos vers la gauche ou la droite 
            var start = rb2D.position;
            Shape.transform.position = start + new Vector2(xDir, 0); // on met le tetriminos dans la position ou on doit le deplacer
            if (!MovementIsPossible()) // si la mouvement n'est pas possible
            {
                Shape.transform.position = start; // on met le tetriminos dans la position precedente
            }
            UpdateGameGrid(); // on mettre a jour la grille

        }
        void AttemptMove()
        {
            if (GameManager.IsStarted)
            {
                if (CanMove)
                {
                    MoveDown();
                }
                else
                {
                    // si le tetriminos ne peut pas deplacer
                    // Draw ();
                    CheckRows(); //on verifie si on a des ligne completes
                    GameManager.GameIsOver = isGameOver();
                    if (!GameManager.GameIsOver && !CanMove)
                    {
                        ScoreManager.LevelCheck();
                        SS.SpawnShape();
                        Debug.Log("eee");
                        Shape = SS.CurrentObject;
                        rb2D = SS.rb;
                        CanMove = true;
                    }
                }
            }
        }
        void Draw()
        {
            // fonction pour deboger le jeu permettre de dessiner la grille du jeu dans le console
            for (int y = 5; y >= 0; y--)
            {
                string s = "";
                for (int x = 0; x < 10; x++)
                {
                    s += GameGrid[x, y] == null ? "| " : "|*";
                }
                Debug.Log(s + "|");
            }
            Debug.Log("end");
        }
        void Update()
        {
            //cette fonction va etre excuté 30 fois dans 1 seconde (30fps)
            if (!GameManager.isOnTuto) AttemptMove(); // dans le tutoriel la movement est controllé selon le guide
            else if (GameManager.tutoStep == "DoneStep") AttemptMove();
            else if (GameManager.tutoStep == "OnSpeed") AttemptMove();
        }

        void CheckRows()
        {
            // pour vider les lignes qui sont completes
            var x = 0;
            for (int y = 0; y < 20; y++)
            {
                if (IsRowFull(y))
                { // si une ligne est complete
                    DeleteRow(y); // on supprime cette ligne
                    DecreaseRowsAbove(y + 1); // on decale les lignes qui sont au dessus de cette ligne
                    --y; // pour decaler les lignes du haut vers la bas
                    x++; // pour connaitre le nombre du ligne suprrimer a la fois
                }
            }
            ScoreManager.IncreaseScoree(x); // pour incremementer le score selon le nombre du ligne supprimées
            if (x == 1)
            {
                goodimg.enabled = true;
                goodimg.CrossFadeColor(Color.clear, 5f, true, true);
            }
            else if (x == 2)
            {
                excellencimg.enabled = true;
                excellencimg.CrossFadeColor(Color.clear, 5f, true, true);

            }
            else if (x == 3)
            {
                greatimg.enabled = true;
                greatimg.CrossFadeColor(Color.clear, 5f, true, true);
            }
            else if (x == 4)
            {
                perfectimg.enabled = true;
                perfectimg.CrossFadeColor(Color.clear, 5f, true, true);
            }
            else
            {
                goodimg.CrossFadeColor(Color.white, 0.1f, true, true);
                excellencimg.CrossFadeColor(Color.white, 0.1f, true, true);
                greatimg.CrossFadeColor(Color.white, 0.1f, true, true);
                perfectimg.CrossFadeColor(Color.white, 0.1f, true, true);
                goodimg.enabled = false;
                excellencimg.enabled = false;
                greatimg.enabled = false;
                perfectimg.enabled = false;

            }
        }

        public bool IsRowFull(int y)
        {
            //on verifie si une ligne est pleine
            for (int x = 0; x < 10; x++)
                if (GameGrid[x, y] == null) // si il y a une emplacement vide alors la ligne n'est pas complete
                    return false;
            return true;
        }

        public void DeleteRow(int y)
        {
            // on supprime la y eme ligne
            for (int x = 0; x < 10; x++)
            {
                //FadeIn (GameGrid[x, y].gameObject.GetComponent<SpriteRenderer> ());
                Destroy(GameGrid[x, y].gameObject);
                GameGrid[x, y] = null;
            }
        }

        public void DecreaseRowsAbove(int y)
        {
            //on decale les lignes superiuere a la y eme ligne
            for (int i = y; i < 20; i++)
                DecreaseRow(i);
        }

        public void DecreaseRow(int y)
        {
            //pour decaler la ligne superieure
            for (int x = 0; x < 10; x++)
            {
                if (GameGrid[x, y] != null)
                {
                    // Move one towards bottom
                    GameGrid[x, y - 1] = GameGrid[x, y];
                    GameGrid[x, y] = null;
                    // Update Block position
                    GameGrid[x, y - 1].position += new Vector3(0, -1, 0);
                }
            }
        }
        public void MoveLeft()
        {
            // pour deplacer la tetreminios vers la gauche
            if (!GameManager.isOnTuto)
            {
                if (CanMove) Move(-1); // si le movement est possible
            }
            else
            {
                if (GameManager.isOnTuto && GameManager.tutoStep == "Left")
                { // le tutoriel limite les deplacements selon le guide
                    Move(-1);
                    GameManager.tutoStep = "DoneStep";
                    GameManager.NextStep = "Right";
                }
            }
        }
        public void MoveRight()
        {
            // pour deplacer la tetreminios vers la droite
            if (!GameManager.isOnTuto)
            {
                if (CanMove) Move(1);
            }
            else
            {
                if (GameManager.isOnTuto && GameManager.tutoStep == "Right")
                {
                    Move(1);
                    GameManager.tutoStep = "DoneStep";
                    GameManager.NextStep = "Rotate";
                    GameManager.NextStep = "Rotate";
                }
            }
        }

        public void RotateShape()
        {
            // pour la rotation du tetrominos
            if (!(Shape.name.Contains("TetrominosO")))
            { // si le tetriminos nest pas le tetrominos carre on le fais tourner
                if (!GameManager.isOnTuto)
                {
                    Shape.transform.Rotate(0, 0, 90); // on tourne le tetriminos 
                    if (!MovementIsPossible())
                    { // si la rotation n'est pas valide
                        Shape.transform.Rotate(0, 0, -90); // on n'annule
                    }
                    else
                    {
                        UpdateGameGrid(); // on mettre a jour la grille;
                    }
                }
                else
                {
                    if (GameManager.isOnTuto && GameManager.tutoStep == "Rotate")
                    {
                        Shape.transform.Rotate(0, 0, 90); // on tourne le tetriminos 
                        if (!MovementIsPossible()) // si la rotation n'est pas valide
                        {
                            Shape.transform.Rotate(0, 0, -90); // on annule
                        }
                        else
                        {
                            UpdateGameGrid(); // on mettre a jour la grille;
                        }
                        GameManager.tutoStep = "DoneStep"; // pour avancer le tutoriel
                        GameManager.NextStep = "Speed";

                    }
                }
            }
            else
            {
                GameManager.tutoStep = "DoneStep"; // pour avancer le tutoriel
                GameManager.NextStep = "Speed";
            }
        }
        public void SpeedUp()
        {
            // pour changer la vitesse du tetrominos si lutilisateur demande
            GameManager.tutoStep = "OnSpeed";
            ScoreManager.Speed = 0.16f; // la vitesse s'augmente de x1.5 
        }

        bool isGameOver()
        {
            // si la grille est pleine alors le jeu est terminé
            for (int x = 0; x < tailleX; x++)
            {
                if (GameGrid[x, 16] != null)
                    return true;
            }
            return false;
        }
        public static void EndGame()
        {
            // pour terminer le jeu en vidant la grille du jeu 
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (GameGrid[j, i] != null) Destroy(GameGrid[j, i].gameObject);
                }
            }
            ScoreManager.ResetTime();
            ScoreManager.ResetScore();
        }
        public static void UseJokerGame()
        {
            // pour terminer le jeu en vidant la grille du jeu 
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (GameGrid[j, i] != null) Destroy(GameGrid[j, i].gameObject);
                }
            }
            Destroy(Shape);
        }

    }
}