using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TileBehavior : MonoBehaviour, IPointerClickHandler {
	public int id;
	public int pred;
	public TabuleiroBehavior tabuleiro;
	public int behaviour;

	public void OnPointerClick(PointerEventData pointerEventData) {
		if (behaviour == 1)
			tabuleiro.selectH (id);
		else if (behaviour == 2)
			tabuleiro.multiplyH (id, pred);
		else if (behaviour == 3)
			tabuleiro.jumpH (id, pred);
		else if (behaviour == 4)
			tabuleiro.unselect (id);
	}
}
