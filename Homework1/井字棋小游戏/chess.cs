using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chess : MonoBehaviour {
	private int[,] BoxStatus = new int[3, 3]; //用于保存棋盘状态
	private int gameStatus = 0; //记录游戏是否开始
	private int turn= 1; //记录当前轮到哪个玩家
	private float xpos = Screen.width*0.5f-150;
	private float ypos = Screen.height*0.5f-150;
	private int Winner = 0;//记录赢的玩家

	// Use this for in itialization
	void Start () {
		initialGame ();
	}
		
	//初始化函数
	void initialGame () {
		gameStatus = 0;
		Winner = 0;
		turn = 1;
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++)
				BoxStatus[i,j] = 0;
	}

	void OnGUI () {
		GUIStyle textStyle = new GUIStyle ();//文本字体
		textStyle.normal.textColor = Color.black;
		textStyle.fontSize = 50;
		GUI.Label(new Rect(xpos,ypos-100,300,100), "Chess Game", textStyle);
		if (gameStatus == 0) {
			int result = 0;
			if (Winner == 0) {
				result = check ();
				if (result != 0) {
					Winner = result;
					gameStatus = 1;
				}
			}
			for (int i = 0; i < 3; i++)//每帧棋盘内容更新
				for (int j = 0; j < 3; j++) {
					if (BoxStatus [i, j] == 1)
						GUI.Button (new Rect (xpos + i * 100, ypos + j * 100, 100, 100), "O");
					if (BoxStatus [i, j] == 2)
						GUI.Button (new Rect (xpos + i * 100, ypos + j * 100, 100, 100), "X");
					if (GUI.Button (new Rect (xpos + i * 100, ypos + j * 100, 100, 100), "")) {  
						if (result == 0) {  
							BoxStatus [i, j] = turn;  
							turn = (turn== 2) ? 1 : 2;
						}  
					}  
				}
		} 
		else {
			if (Winner == 3) {
				GUI.Label (new Rect (xpos, ypos, 100, 50), "No one wins!", textStyle);
			} else if (Winner == 1) {
				GUI.Label (new Rect (xpos+60, ypos, 100, 50), "O wins!", textStyle);
			} else if (Winner == 2) {
				GUI.Label (new Rect (xpos+60, ypos, 100, 50), "X wins!", textStyle);
			}
			if (GUI.Button (new Rect (xpos+90, ypos+150, 120, 50), "Play again!"))
				initialGame ();
		}
	}

	//判断游戏是否结束
	int check () {
		for (int i = 0; i < 3; i++) {
			if (BoxStatus [0, i] != 0 && BoxStatus [0, i] == BoxStatus [1, i] && BoxStatus [1, i] == BoxStatus [2, i])
				return BoxStatus [0, i];
			if (BoxStatus [i, 0] != 0 && BoxStatus [i, 0] == BoxStatus [i, 1] && BoxStatus [i, 1] == BoxStatus [i, 2])
				return BoxStatus [i, 0];
		}
		if (BoxStatus [1, 1] != 0 && BoxStatus [0, 0] == BoxStatus [1, 1] && BoxStatus [1, 1] == BoxStatus [2, 2])
			return BoxStatus [1, 1];
		if (BoxStatus [1, 1] != 0 && BoxStatus [0, 2] == BoxStatus [1, 1] && BoxStatus [1, 1] == BoxStatus [2, 0])
			return BoxStatus [1, 1];
		for (int i = 0; i < 3; i++)
			for (int j = 0; j < 3; j++) {
				if (BoxStatus[i,j] == 0) {
					return 0;
				}
			}
		return 3;
	}
}