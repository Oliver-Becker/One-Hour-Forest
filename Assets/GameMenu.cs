using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour {

	public GameObject Menu;

	public void open_menu() {
		Menu.SetActive (true);
	}

	public void close_menu() {
		Menu.SetActive (false);
	}

	public void return_main_menu() {
		SceneManager.LoadScene ("Menu");
	}

}
