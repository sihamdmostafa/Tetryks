using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace AssemblyCSharp
{
public class AnimationController : MonoBehaviour {
    public GameObject Current;
    // Use this for initialization
    void Start () {
        Current.GetComponent<Animation>().Play();
        /*Current.GetComponent<Animation>().AddClip();
        AnimationClip animationclip = new AnimationClip();
        animationclip.AddEvent();
        AnimationEvent anim = new AnimationEvent();
        anim.*/
         
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
}