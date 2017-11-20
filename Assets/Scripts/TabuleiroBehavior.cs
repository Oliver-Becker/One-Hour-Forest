using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabuleiroBehavior : MonoBehaviour {
	public GameObject vazio;
	public GameObject arvore;
	public GameObject cidade;
	private bool selected = false;
	private int turn;
	private GameObject turnG;
	private Vector3 turnpos = new Vector3(-170, 67, 0);

	private int[,] tabuleiro = {
		{-1, -1, -1, -1, 1, 0, 0, 2},
		{-1, -1, -1, 0, 0, 0, 0, 0},
		{-1, -1, 0, 0, 0, 0, 0, 0},
		{-1, 1, 0, 0, 0, 0, 0, 2},
		{-1, 0, 0, 0, 0, 0, 0, -1},
		{-1, 0, 0, 0, 0, 0, -1, -1},
		{-1, 1, 0, 0, 2, -1, -1, -1},
		{-1, -1, -1, -1, -1, -1, -1, -1}
	};

	private GameObject[,] tabuleiroG = new GameObject[8,8];

	public void Start() {
		int x = -80, y = -96, z = 0;
		for (int i = 0; i < 8; i++) {
			y = -96 + 8*i;
			z = 0 + 1*i;
			for (int j = 0; j < 8; j++) {
				if (tabuleiro [i, j] == 0) {
					tabuleiroG [i, j] = Instantiate (vazio, new Vector3(x, y, z), Quaternion.identity);
				} else if (tabuleiro [i, j] == 1) {
					tabuleiroG [i, j] = Instantiate (arvore, new Vector3(x, y, z), Quaternion.identity);
				} else if (tabuleiro [i, j] == 2) {
					tabuleiroG [i, j] = Instantiate (cidade, new Vector3(x, y, z), Quaternion.identity);
				}

				y += 16;
				z += 2;
			}
			x += 20;
		}

		turn = (int) Mathf.Round(Random.value);
		if (turn == 0)
			turnG = Instantiate (arvore, turnpos, Quaternion.identity);
		else
			turnG = Instantiate (cidade, turnpos, Quaternion.identity);

		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if (tabuleiroG [i, j]) {
					tabuleiroG [i, j].GetComponent<TileBehavior> ().id = i * 8 + j;
					tabuleiroG [i, j].GetComponent<TileBehavior> ().tabuleiro = this;
					if (tabuleiro [i, j] == 1 && turn == 0)
						tabuleiroG [i, j].GetComponent<TileBehavior> ().behaviour = 1;
					else if (tabuleiro [i, j] == 2 && turn == 1) 
						tabuleiroG [i, j].GetComponent<TileBehavior> ().behaviour = 1;
					else
						tabuleiroG [i, j].GetComponent<TileBehavior> ().behaviour = 0;
				}
			}
		}
	}

	public void convertTiles (int id) {
		int isForest = turn;

		if ((id%8)+1 < 8 && tabuleiro[(id / 8), (id % 8) +1] > 0) {
			tabuleiro [(id / 8), (id % 8) + 1] = tabuleiro [(id / 8), (id % 8) ];
			if (isForest == 0) {
				GameObject tile = tabuleiroG [(id / 8), (id % 8)+1];
				tabuleiroG [(id / 8), (id % 8)+1] = Instantiate (arvore, tile.transform.position, tile.transform.rotation);
				tabuleiroG [(id / 8), (id % 8)+1].GetComponent<TileBehavior> ().id = (id / 8) *8 + (id % 8) +1;
				tabuleiroG [(id / 8), (id % 8)+1].GetComponent<TileBehavior> ().tabuleiro = this;
				Destroy (tile);
			} else {
				GameObject tile = tabuleiroG [(id / 8), (id % 8)+1];
				tabuleiroG [(id / 8), (id % 8)+1] = Instantiate (cidade, tile.transform.position, tile.transform.rotation);
				tabuleiroG [(id / 8), (id % 8)+1].GetComponent<TileBehavior> ().id = (id / 8) *8 + (id % 8)+1;
				tabuleiroG [(id / 8), (id % 8)+1].GetComponent<TileBehavior> ().tabuleiro = this;
				Destroy (tile);
			}
		}
		if ((id%8)-1 >= 0 && tabuleiro[(id / 8), (id % 8) -1] > 0) {
			tabuleiro [(id / 8), (id % 8) - 1] = tabuleiro [(id / 8), (id % 8) ];
			if (isForest == 0) {
				GameObject tile = tabuleiroG [(id / 8), (id % 8)-1];
				tabuleiroG [(id / 8), (id % 8)-1] = Instantiate (arvore, tile.transform.position, tile.transform.rotation);
				tabuleiroG [(id / 8), (id % 8)-1].GetComponent<TileBehavior> ().id = (id / 8) *8 + (id % 8)-1;
				tabuleiroG [(id / 8), (id % 8)-1].GetComponent<TileBehavior> ().tabuleiro = this;
				Destroy (tile);
			} else {
				GameObject tile = tabuleiroG [(id / 8), (id % 8)-1];
				tabuleiroG [(id / 8), (id % 8)-1] = Instantiate (cidade, tile.transform.position, tile.transform.rotation);
				tabuleiroG [(id / 8), (id % 8)-1].GetComponent<TileBehavior> ().id = (id / 8) *8 + (id % 8)-1;
				tabuleiroG [(id / 8), (id % 8)-1].GetComponent<TileBehavior> ().tabuleiro = this;
				Destroy (tile);
			}
		}
		if ((id/8)+1 < 8 && tabuleiro[(id / 8)+1, (id % 8)] > 0) {
			tabuleiro [(id / 8) + 1, (id % 8)] = tabuleiro [(id / 8), (id % 8) ];
			if (isForest == 0) {
				GameObject tile = tabuleiroG [(id / 8)+1, (id % 8)];
				tabuleiroG [(id / 8)+1, (id % 8)] = Instantiate (arvore, tile.transform.position, tile.transform.rotation);
				tabuleiroG [(id / 8)+1, (id % 8)].GetComponent<TileBehavior> ().id = ((id / 8)+1)*8 + (id % 8);
				tabuleiroG [(id / 8)+1, (id % 8)].GetComponent<TileBehavior> ().tabuleiro = this;
				Destroy (tile);
			} else {
				GameObject tile = tabuleiroG [(id / 8)+1, (id % 8)];
				tabuleiroG [(id / 8)+1, (id % 8)] = Instantiate (cidade, tile.transform.position, tile.transform.rotation);
				tabuleiroG [(id / 8)+1, (id % 8)].GetComponent<TileBehavior> ().id = ((id / 8)+1)*8 + (id % 8);
				tabuleiroG [(id / 8)+1, (id % 8)].GetComponent<TileBehavior> ().tabuleiro = this;
				Destroy (tile);
			}
		}
		if ((id/8)+1 < 8 && (id%8)-1 >= 0 && tabuleiro[(id / 8)+1, (id % 8)-1] > 0) {
			tabuleiro [(id / 8) + 1, (id % 8) - 1] = tabuleiro [(id / 8), (id % 8) ];
			if (isForest == 0) {
				GameObject tile = tabuleiroG [(id / 8)+1, (id % 8)-1];
				tabuleiroG [(id / 8)+1, (id % 8)-1] = Instantiate (arvore, tile.transform.position, tile.transform.rotation);
				tabuleiroG [(id / 8)+1, (id % 8)-1].GetComponent<TileBehavior> ().id = ((id / 8)+1)*8 + (id % 8)-1;
				tabuleiroG [(id / 8)+1, (id % 8)-1].GetComponent<TileBehavior> ().tabuleiro = this;
				Destroy (tile);
			} else {
				GameObject tile = tabuleiroG [(id / 8)+1, (id % 8)-1];
				tabuleiroG [(id / 8)+1, (id % 8)-1] = Instantiate (cidade, tile.transform.position, tile.transform.rotation);
				tabuleiroG [(id / 8)+1, (id % 8)-1].GetComponent<TileBehavior> ().id = ((id / 8)+1)*8 + (id % 8)-1;
				tabuleiroG [(id / 8)+1, (id % 8)-1].GetComponent<TileBehavior> ().tabuleiro = this;
				Destroy (tile);
			}
		}
		if ((id/8)-1 >= 0 && tabuleiro[(id / 8)-1, (id % 8)] > 0) {
			tabuleiro [(id / 8) - 1, (id % 8)] = tabuleiro [(id / 8), (id % 8) ];
			if (isForest == 0) {
				GameObject tile = tabuleiroG [(id / 8)-1, (id % 8)];
				tabuleiroG [(id / 8)-1, (id % 8)] = Instantiate (arvore, tile.transform.position, tile.transform.rotation);
				tabuleiroG [(id / 8)-1, (id % 8)].GetComponent<TileBehavior> ().id = ((id / 8)-1)*8 + (id % 8);
				tabuleiroG [(id / 8)-1, (id % 8)].GetComponent<TileBehavior> ().tabuleiro = this;
				Destroy (tile);
			} else {
				GameObject tile = tabuleiroG [(id / 8)-1, (id % 8)];
				tabuleiroG [(id / 8)-1, (id % 8)] = Instantiate (cidade, tile.transform.position, tile.transform.rotation);
				tabuleiroG [(id / 8)-1, (id % 8)].GetComponent<TileBehavior> ().id = ((id / 8)-1)*8 + (id % 8);
				tabuleiroG [(id / 8)-1, (id % 8)].GetComponent<TileBehavior> ().tabuleiro = this;
				Destroy (tile);
			}
		}
		if ((id/8)-1 >= 0 && (id%8)+1 < 8 && tabuleiro[(id / 8)-1, (id % 8)+1] > 0) {
			tabuleiro [(id / 8) - 1, (id % 8) + 1] = tabuleiro [(id / 8), (id % 8) ];
			if (isForest == 0) {
				GameObject tile = tabuleiroG [(id / 8)-1, (id % 8)+1];
				tabuleiroG [(id / 8)-1, (id % 8)+1] = Instantiate (arvore, tile.transform.position, tile.transform.rotation);
				tabuleiroG [(id / 8)-1, (id % 8)+1].GetComponent<TileBehavior> ().id = ((id / 8)-1)*8 + (id % 8)+1;
				tabuleiroG [(id / 8)-1, (id % 8)+1].GetComponent<TileBehavior> ().tabuleiro = this;
				Destroy (tile);
			} else {
				GameObject tile = tabuleiroG [(id / 8)-1, (id % 8)+1];
				tabuleiroG [(id / 8)-1, (id % 8)+1] = Instantiate (cidade, tile.transform.position, tile.transform.rotation);
				tabuleiroG [(id / 8)-1, (id % 8)+1].GetComponent<TileBehavior> ().id = ((id / 8)-1)*8 + (id % 8)+1;
				tabuleiroG [(id / 8)-1, (id % 8)+1].GetComponent<TileBehavior> ().tabuleiro = this;
				Destroy (tile);
			}
		}
	}

	public void changeTurn () {
		turn = (turn+1)%2;
		if (turn == 0) {
			Destroy (turnG);
			turnG = Instantiate (arvore, turnpos, Quaternion.identity);
		} else {
			Destroy (turnG);
			turnG = Instantiate (cidade, turnpos, Quaternion.identity);
		}
		for (int i = 0; i < 8; i++) {
			for (int j = 0; j < 8; j++) {
				if (tabuleiroG [i, j]) {
					if (tabuleiro [i, j] == 1 && turn == 0)
						tabuleiroG [i, j].GetComponent<TileBehavior> ().behaviour = 1;
					else if (tabuleiro [i, j] == 2 && turn == 1) 
						tabuleiroG [i, j].GetComponent<TileBehavior> ().behaviour = 1;
					else
						tabuleiroG [i, j].GetComponent<TileBehavior> ().behaviour = 0;
				}
			}
		}
	}

	public void unpaint (int id) {
		tabuleiroG [(id / 8), (id % 8)].GetComponent<TileBehavior> ().behaviour = 1;

		if ((id%8)+1 < 8 && tabuleiro[(id / 8), (id % 8) +1] == 0) {
			tabuleiroG [(id / 8), (id % 8)+1].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8), (id % 8)+1].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id%8)-1 >= 0 && tabuleiro[(id / 8), (id % 8) -1] == 0) {
			tabuleiroG [(id / 8), (id % 8)-1].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8), (id % 8)-1].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)+1 < 8 && tabuleiro[(id / 8)+1, (id % 8)] == 0) {
			tabuleiroG [(id / 8)+1, (id % 8)].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)+1, (id % 8)].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)+1 < 8 && (id%8)-1 >= 0 && tabuleiro[(id / 8)+1, (id % 8)-1] == 0) {
			tabuleiroG [(id / 8)+1, (id % 8)-1].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)+1, (id % 8)-1].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)-1 >= 0 && tabuleiro[(id / 8)-1, (id % 8)] == 0) {
			tabuleiroG [(id / 8)-1, (id % 8)].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)-1, (id % 8)].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)-1 >= 0 && (id%8)+1 < 8 && tabuleiro[(id / 8)-1, (id % 8)+1] == 0) {
			tabuleiroG [(id / 8)-1, (id % 8)+1].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)-1, (id % 8)+1].GetComponent<TileBehavior> ().behaviour = 0;
		}

		if ((id%8)+2 < 8 && tabuleiro[(id / 8), (id % 8) +2] == 0) {
			tabuleiroG [(id / 8), (id % 8)+2].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8), (id % 8)+2].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id%8)-2 >= 0 && tabuleiro[(id / 8), (id % 8) -2] == 0) {
			tabuleiroG [(id / 8), (id % 8)-2].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8), (id % 8)-2].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)+1 < 8 && (id%8)-2 >= 0 && tabuleiro[(id / 8)+1, (id % 8)-2] == 0) {
			tabuleiroG [(id / 8)+1, (id % 8)-2].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)+1, (id % 8)-2].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)+1 < 8 && (id%8)+1 < 8 && tabuleiro[(id / 8)+1, (id % 8)+1] == 0) {
			tabuleiroG [(id / 8)+1, (id % 8)+1].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)+1, (id % 8)+1].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)+2 < 8 && tabuleiro[(id / 8)+2, (id % 8)] == 0) {
			tabuleiroG [(id / 8)+2, (id % 8)].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)+2, (id % 8)].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)+2 < 8 && (id%8)-1 >= 0 && tabuleiro[(id / 8)+2, (id % 8)-1] == 0) {
			tabuleiroG [(id / 8)+2, (id % 8)-1].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)+2, (id % 8)-1].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)+2 < 8 && (id%8)-2 >= 0 && tabuleiro[(id / 8)+2, (id % 8)-2] == 0) {
			tabuleiroG [(id / 8)+2, (id % 8)-2].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)+2, (id % 8)-2].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)-1 >= 0 && (id%8)-1 >= 0 && tabuleiro[(id / 8)-1, (id % 8)-1] == 0) {
			tabuleiroG [(id / 8)-1, (id % 8)-1].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)-1, (id % 8)-1].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)-1 >= 0 && (id%8)+2 < 8 && tabuleiro[(id / 8)-1, (id % 8)+2] == 0) {
			tabuleiroG [(id / 8)-1, (id % 8)+2].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)-1, (id % 8)+2].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)-2 >= 0 && tabuleiro[(id / 8)-2, (id % 8)] == 0) {
			tabuleiroG [(id / 8)-2, (id % 8)].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)-2, (id % 8)].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)-2 >= 0 && (id%8)+1 < 8 && tabuleiro[(id / 8)-2, (id % 8)+1] == 0) {
			tabuleiroG [(id / 8)-2, (id % 8)+1].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)-2, (id % 8)+1].GetComponent<TileBehavior> ().behaviour = 0;
		}
		if ((id/8)-2 >= 0 && (id%8)+2 < 8 && tabuleiro[(id / 8)-2, (id % 8)+2] == 0) {
			tabuleiroG [(id / 8)-2, (id % 8)+2].GetComponent<SpriteRenderer> ().color = Color.white;
			tabuleiroG [(id / 8)-2, (id % 8)+2].GetComponent<TileBehavior> ().behaviour = 0;
		}

		selected = false;
	}

	public void selectH(int id) {
		if (!selected) {
			tabuleiroG [(id / 8), (id % 8)].GetComponent<TileBehavior> ().behaviour = 4;

			if ((id%8)+1 < 8 && tabuleiro[(id / 8), (id % 8) +1] == 0) {
				tabuleiroG [(id / 8), (id % 8)+1].GetComponent<SpriteRenderer> ().color = Color.yellow;
				tabuleiroG [(id / 8), (id % 8)+1].GetComponent<TileBehavior> ().behaviour = 2;
				tabuleiroG [(id / 8), (id % 8)+1].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id%8)-1 >= 0 && tabuleiro[(id / 8), (id % 8) -1] == 0) {
				tabuleiroG [(id / 8), (id % 8)-1].GetComponent<SpriteRenderer> ().color = Color.yellow;
				tabuleiroG [(id / 8), (id % 8)-1].GetComponent<TileBehavior> ().behaviour = 2;
				tabuleiroG [(id / 8), (id % 8)-1].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)+1 < 8 && tabuleiro[(id / 8)+1, (id % 8)] == 0) {
				tabuleiroG [(id / 8)+1, (id % 8)].GetComponent<SpriteRenderer> ().color = Color.yellow;
				tabuleiroG [(id / 8)+1, (id % 8)].GetComponent<TileBehavior> ().behaviour = 2;
				tabuleiroG [(id / 8)+1, (id % 8)].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)+1 < 8 && (id%8)-1 >= 0 && tabuleiro[(id / 8)+1, (id % 8)-1] == 0) {
				tabuleiroG [(id / 8)+1, (id % 8)-1].GetComponent<SpriteRenderer> ().color = Color.yellow;
				tabuleiroG [(id / 8)+1, (id % 8)-1].GetComponent<TileBehavior> ().behaviour = 2;
				tabuleiroG [(id / 8)+1, (id % 8)-1].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)-1 >= 0 && tabuleiro[(id / 8)-1, (id % 8)] == 0) {
				tabuleiroG [(id / 8)-1, (id % 8)].GetComponent<SpriteRenderer> ().color = Color.yellow;
				tabuleiroG [(id / 8)-1, (id % 8)].GetComponent<TileBehavior> ().behaviour = 2;
				tabuleiroG [(id / 8)-1, (id % 8)].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)-1 >= 0 && (id%8)+1 < 8 && tabuleiro[(id / 8)-1, (id % 8)+1] == 0) {
				tabuleiroG [(id / 8)-1, (id % 8)+1].GetComponent<SpriteRenderer> ().color = Color.yellow;
				tabuleiroG [(id / 8)-1, (id % 8)+1].GetComponent<TileBehavior> ().behaviour = 2;
				tabuleiroG [(id / 8)-1, (id % 8)+1].GetComponent<TileBehavior> ().pred = id;
			}

			if ((id%8)+2 < 8 && tabuleiro[(id / 8), (id % 8) +2] == 0) {
				tabuleiroG [(id / 8), (id % 8)+2].GetComponent<SpriteRenderer> ().color = Color.red;
				tabuleiroG [(id / 8), (id % 8)+2].GetComponent<TileBehavior> ().behaviour = 3;
				tabuleiroG [(id / 8), (id % 8)+2].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id%8)-2 >= 0 && tabuleiro[(id / 8), (id % 8) -2] == 0) {
				tabuleiroG [(id / 8), (id % 8)-2].GetComponent<SpriteRenderer> ().color = Color.red;
				tabuleiroG [(id / 8), (id % 8)-2].GetComponent<TileBehavior> ().behaviour = 3;
				tabuleiroG [(id / 8), (id % 8)-2].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)+1 < 8 && (id%8)-2 >= 0 && tabuleiro[(id / 8)+1, (id % 8)-2] == 0) {
				tabuleiroG [(id / 8)+1, (id % 8)-2].GetComponent<SpriteRenderer> ().color = Color.red;
				tabuleiroG [(id / 8)+1, (id % 8)-2].GetComponent<TileBehavior> ().behaviour = 3;
				tabuleiroG [(id / 8)+1, (id % 8)-2].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)+1 < 8 && (id%8)+1 < 8 && tabuleiro[(id / 8)+1, (id % 8)+1] == 0) {
				tabuleiroG [(id / 8)+1, (id % 8)+1].GetComponent<SpriteRenderer> ().color = Color.red;
				tabuleiroG [(id / 8)+1, (id % 8)+1].GetComponent<TileBehavior> ().behaviour = 3;
				tabuleiroG [(id / 8)+1, (id % 8)+1].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)+2 < 8 && tabuleiro[(id / 8)+2, (id % 8)] == 0) {
				tabuleiroG [(id / 8)+2, (id % 8)].GetComponent<SpriteRenderer> ().color = Color.red;
				tabuleiroG [(id / 8)+2, (id % 8)].GetComponent<TileBehavior> ().behaviour = 3;
				tabuleiroG [(id / 8)+2, (id % 8)].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)+2 < 8 && (id%8)-1 >= 0 && tabuleiro[(id / 8)+2, (id % 8)-1] == 0) {
				tabuleiroG [(id / 8)+2, (id % 8)-1].GetComponent<SpriteRenderer> ().color = Color.red;
				tabuleiroG [(id / 8)+2, (id % 8)-1].GetComponent<TileBehavior> ().behaviour = 3;
				tabuleiroG [(id / 8)+2, (id % 8)-1].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)+2 < 8 && (id%8)-2 >= 0 && tabuleiro[(id / 8)+2, (id % 8)-2] == 0) {
				tabuleiroG [(id / 8)+2, (id % 8)-2].GetComponent<SpriteRenderer> ().color = Color.red;
				tabuleiroG [(id / 8)+2, (id % 8)-2].GetComponent<TileBehavior> ().behaviour = 3;
				tabuleiroG [(id / 8)+2, (id % 8)-2].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)-1 >= 0 && (id%8)-1 >= 0 && tabuleiro[(id / 8)-1, (id % 8)-1] == 0) {
				tabuleiroG [(id / 8)-1, (id % 8)-1].GetComponent<SpriteRenderer> ().color = Color.red;
				tabuleiroG [(id / 8)-1, (id % 8)-1].GetComponent<TileBehavior> ().behaviour = 3;
				tabuleiroG [(id / 8)-1, (id % 8)-1].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)-1 >= 0 && (id%8)+2 < 8 && tabuleiro[(id / 8)-1, (id % 8)+2] == 0) {
				tabuleiroG [(id / 8)-1, (id % 8)+2].GetComponent<SpriteRenderer> ().color = Color.red;
				tabuleiroG [(id / 8)-1, (id % 8)+2].GetComponent<TileBehavior> ().behaviour = 3;
				tabuleiroG [(id / 8)-1, (id % 8)+2].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)-2 >= 0 && tabuleiro[(id / 8)-2, (id % 8)] == 0) {
				tabuleiroG [(id / 8)-2, (id % 8)].GetComponent<SpriteRenderer> ().color = Color.red;
				tabuleiroG [(id / 8)-2, (id % 8)].GetComponent<TileBehavior> ().behaviour = 3;
				tabuleiroG [(id / 8)-2, (id % 8)].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)-2 >= 0 && (id%8)+1 < 8 && tabuleiro[(id / 8)-2, (id % 8)+1] == 0) {
				tabuleiroG [(id / 8)-2, (id % 8)+1].GetComponent<SpriteRenderer> ().color = Color.red;
				tabuleiroG [(id / 8)-2, (id % 8)+1].GetComponent<TileBehavior> ().behaviour = 3;
				tabuleiroG [(id / 8)-2, (id % 8)+1].GetComponent<TileBehavior> ().pred = id;
			}
			if ((id/8)-2 >= 0 && (id%8)+2 < 8 && tabuleiro[(id / 8)-2, (id % 8)+2] == 0) {
				tabuleiroG [(id / 8)-2, (id % 8)+2].GetComponent<SpriteRenderer> ().color = Color.red;
				tabuleiroG [(id / 8)-2, (id % 8)+2].GetComponent<TileBehavior> ().behaviour = 3;
				tabuleiroG [(id / 8)-2, (id % 8)+2].GetComponent<TileBehavior> ().pred = id;
			}

			selected = true;
		}
	}

	public void multiplyH(int idnow, int idbefore) {
		unpaint (idbefore);

		tabuleiro [(idnow / 8), (idnow % 8)] = tabuleiro [(idbefore / 8), (idbefore % 8)];
		if (tabuleiro [(idnow / 8), (idnow % 8)] == 1) {
			GameObject tile = tabuleiroG [(idnow / 8), (idnow % 8)];
			tabuleiroG [(idnow / 8), (idnow % 8)] = Instantiate (arvore, tile.transform.position, tile.transform.rotation);
			tabuleiroG [(idnow / 8), (idnow % 8)].GetComponent<TileBehavior> ().id = idnow;
			tabuleiroG [(idnow / 8), (idnow % 8)].GetComponent<TileBehavior> ().tabuleiro = this;
			Destroy (tile);
		} else {
			GameObject tile = tabuleiroG [(idnow / 8), (idnow % 8)];
			tabuleiroG [(idnow / 8), (idnow % 8)] = Instantiate (cidade, tile.transform.position, tile.transform.rotation);
			tabuleiroG [(idnow / 8), (idnow % 8)].GetComponent<TileBehavior> ().id = idnow;
			tabuleiroG [(idnow / 8), (idnow % 8)].GetComponent<TileBehavior> ().tabuleiro = this;
			Destroy (tile);
		}

		convertTiles (idnow);
		changeTurn ();
	}

	public void jumpH(int idnow, int idbefore) {
		unpaint (idbefore);

		tabuleiro [(idnow / 8), (idnow % 8)] = tabuleiro [(idbefore / 8), (idbefore % 8)];
		if (tabuleiro [(idnow / 8), (idnow % 8)] == 1) {
			GameObject tile = tabuleiroG [(idnow / 8), (idnow % 8)];
			tabuleiroG [(idnow / 8), (idnow % 8)] = Instantiate (arvore, tile.transform.position, tile.transform.rotation);
			tabuleiroG [(idnow / 8), (idnow % 8)].GetComponent<TileBehavior> ().id = idnow;
			tabuleiroG [(idnow / 8), (idnow % 8)].GetComponent<TileBehavior> ().tabuleiro = this;
			Destroy (tile);

			tile = tabuleiroG [(idbefore / 8), (idbefore % 8)];
			tabuleiroG [(idbefore / 8), (idbefore % 8)] = Instantiate (vazio, tile.transform.position, tile.transform.rotation);
			tabuleiro [(idbefore / 8), (idbefore % 8)] = 0;
			tabuleiroG [(idbefore / 8), (idbefore % 8)].GetComponent<TileBehavior> ().id = idbefore;
			tabuleiroG [(idbefore / 8), (idbefore % 8)].GetComponent<TileBehavior> ().tabuleiro = this;
			Destroy (tile);
		} else {
			GameObject tile = tabuleiroG [(idnow / 8), (idnow % 8)];
			tabuleiroG [(idnow / 8), (idnow % 8)] = Instantiate (cidade, tile.transform.position, tile.transform.rotation);
			tabuleiroG [(idnow / 8), (idnow % 8)].GetComponent<TileBehavior> ().id = idnow;
			tabuleiroG [(idnow / 8), (idnow % 8)].GetComponent<TileBehavior> ().tabuleiro = this;
			Destroy (tile);

			tile = tabuleiroG [(idbefore / 8), (idbefore % 8)];
			tabuleiroG [(idbefore / 8), (idbefore % 8)] = Instantiate (vazio, tile.transform.position, tile.transform.rotation);
			tabuleiro [(idbefore / 8), (idbefore % 8)] = 0;
			tabuleiroG [(idbefore / 8), (idbefore % 8)].GetComponent<TileBehavior> ().id = idbefore;
			tabuleiroG [(idbefore / 8), (idbefore % 8)].GetComponent<TileBehavior> ().tabuleiro = this;
			Destroy (tile);
		}

		convertTiles (idnow);
		changeTurn ();
	}

	public void unselect (int id) {
		unpaint (id);
	}
}