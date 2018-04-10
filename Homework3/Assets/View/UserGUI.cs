using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;

public class UserGUI : MonoBehaviour {
	private UserAction action;
	public int status = 0;
	GUIStyle style;
	GUIStyle buttonStyle;
	GUIStyle button_Style;
	bool isShow;


	void Start() {
		action = Direct.getInstance ().currentSceneController as UserAction;

		style = new GUIStyle();
		style.fontSize = 40;
		style.alignment = TextAnchor.MiddleCenter;

		buttonStyle = new GUIStyle("button");
		buttonStyle.fontSize = 30;
		button_Style=new GUIStyle("button");
		button_Style.fontSize = 10;
	}
	void OnGUI() {
		if (GUI.Button(new Rect(10, 10, 80, 30), "Rule", buttonStyle))
		{
			if (isShow)
				isShow = false;
			else
				isShow = true;
		}
		if (isShow) {
			GUI.Label (new Rect (Screen.width / 2 - 120, 10, 300, 50), "全部牧师和恶魔都渡河后即可获胜");
			GUI.Label (new Rect (Screen.width / 2 - 120, 30, 300, 50), "每一边恶魔数量都不能多于牧师数量");
			GUI.Label (new Rect (Screen.width / 2 - 120, 50, 300, 50), "绿色是牧师，红色是恶魔");
		}
		if (status == 1) {
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-85, 100, 50), "Gameover!", style);
			if (GUI.Button(new Rect(Screen.width/2-70, Screen.height/2, 140, 70), "Restart", button_Style)) {
				status = 0;
				action.restart ();
			}
		} else if(status == 2) {
			GUI.Label(new Rect(Screen.width/2-50, Screen.height/2-85, 100, 50), "You win!", style);
			if (GUI.Button(new Rect(Screen.width/2-70, Screen.height/2, 140, 70), "Restart", buttonStyle)) {
				status = 0;
				action.restart ();
			}
		}
	}
}