using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AssemblyCSharp {
    public class ShapeSpawner : MonoBehaviour {
        public GameObject ShapesHolder;
        public GameObject[] Shapes;
        public GameObject whereToSpawn;
        public GameObject whereToNext;
        public static GameObject NextObject = null;
        [HideInInspector]
        public GameObject CurrentObject = null;
        int NextShapeIndex = 0;
        int ShapeIndex = 0;

        [HideInInspector]
        public Rigidbody2D rb;
        // private int shapeIndex = 0; //l'indice du sh

        public GameObject SpawnShape () {
            /*
                        int nextShapeIndex = Random.Range (0, Shapes.Length);
                        if (NextObject != null) {
                            foreach (var item in NextObject.GetComponentsInChildren<SpriteRenderer> ()) {
                                item.color = Colors.CurrentTheme;
                            }
                            CurrentObject = NextObject;
                        } else {
                            CurrentObject = Instantiate (Shapes[Random.Range (0, Shapes.Length)], whereToSpawn.transform.position, Quaternion.identity);
                            foreach (var item in CurrentObject.GetComponentsInChildren<SpriteRenderer> ()) {
                                item.color = Colors.CurrentTheme;
                            }
                        }
                        rb = CurrentObject.GetComponent<Rigidbody2D> ();
                        CurrentObject.transform.position = whereToSpawn.transform.position;
                        CurrentObject.transform.SetParent(  ShapesHolder.transform);
                        NextObject = Instantiate (Shapes[nextShapeIndex], whereToNext.transform.position, Quaternion.identity);
                        Debug.Log("here2");
                        foreach (var item in NextObject.GetComponentsInChildren<SpriteRenderer> ()) {
                            item.color = Colors.CurrentTheme;
                        }
                        NextObject.transform.SetParent(ShapesHolder.transform);
                        return CurrentObject;*/
            int nextShapeIndex = Random.Range (0, Shapes.Length);
            if (NextObject != null)
                CurrentObject = NextObject;
            else
                CurrentObject = Instantiate (Shapes[Random.Range (0, Shapes.Length)], whereToSpawn.transform.position, Quaternion.identity);
            rb = CurrentObject.GetComponent<Rigidbody2D> ();
            CurrentObject.transform.position = whereToSpawn.transform.position;
            CurrentObject.transform.parent = ShapesHolder.transform;
            NextObject = Instantiate (Shapes[nextShapeIndex], whereToNext.transform.position, Quaternion.identity);
            NextObject.transform.SetParent (ShapesHolder.transform);
            return CurrentObject;
        }

        // Use this for initialization
        void Start () {
            if (GameManager.isOnTuto) {
                whereToSpawn.transform.position = new Vector2 (5, 13);
            }
        }

        // Update is called once per frame
        void Update () {
            if (GameManager.isOnTuto) {
                whereToSpawn.transform.position = new Vector2 (5, 13);
            } else {
                whereToSpawn.transform.position = new Vector2 (5, 18);
            }

        }
        public void SetUpBoard () {
            var x = ShapesHolder.GetComponentsInChildren<GameObject> ();
            foreach (var item in x) {
                foreach (var itemm in item.GetComponentsInChildren<GameObject> ())
                    Destroy (itemm);
                Destroy (item);
            }
        }

    }
}