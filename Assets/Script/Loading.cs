using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace AssemblyCSharp {
	public class Loading : MonoBehaviour {
		public Transform loadingbar;
		[SerializeField] private float currentAmount;
		[SerializeField] private float speed;
		public bool amount;
		// Update is called once per frame
        public void SetcurrentAmount()
        {
            currentAmount = 0;
        }
		private void Start () {
			amount = true;
			currentAmount = 0;
		}
		void Update () {
            if ((GameManager.GameIsOver) && (GameManager.Ui.UseJokerMenu.isActiveAndEnabled))
            {
                if (currentAmount < 100)
                {
                    currentAmount += speed * Time.deltaTime;
                }
                else
                {
                    currentAmount = 0;
                    GameManager.GameIsOver = false;
                    GameManager.Ui.UseJokerMenu.enabled = false;
                    GameManager.Ui.GameOver();
                }
                loadingbar.GetComponent<Image>().fillAmount = currentAmount / 100;
            }
            else SetcurrentAmount();
		}
	}
    
}