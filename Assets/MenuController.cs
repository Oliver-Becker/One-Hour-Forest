using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {

	public GameObject MainScreen;
	public GameObject CreditScreen;

	public void goto_main() {
		MainScreen.SetActive (true);
		CreditScreen.SetActive (false);
	}

	public void goto_credits() {
		MainScreen.SetActive (false);
		CreditScreen.SetActive (true);
	}

	public void play() {
		SceneManager.LoadScene ("Game");
	}

}
