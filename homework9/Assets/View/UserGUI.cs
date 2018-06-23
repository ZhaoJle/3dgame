using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;

public class UserGUI : MonoBehaviour {
	private UserAction action;
	public int status = 0;
	MyCharacterController characterController;
	public static gameController controller;
	GUIStyle style;
	private GUIStyle hintStyle;
	GUIStyle buttonStyle;
	GUIStyle button_Style;
	bool isShow;
	public static IState state = new IState(0, 0, 3, 3, false, null);
	public static IState endState = new IState(3, 3, 0, 0, true, null);
	private string hint = "";


	public void setController(MyCharacterController characterCtrl) {
		characterController = characterCtrl;
	}

	void Start() {
		action = Direct.getInstance ().currentSceneController as UserAction;
		controller = Direct.getInstance().currentSceneController as gameController;
		style = new GUIStyle();
		style.fontSize = 40;
		style.alignment = TextAnchor.MiddleCenter;
		hintStyle = new GUIStyle ();
		hintStyle.fontSize = 15;

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
		GUI.Label(new Rect(Screen.width / 2 - 500, Screen.height / 2 - 210, 100, 50),
			hint, hintStyle);
		if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 - 150, 100, 50), "Tips", buttonStyle)) {
			IState temp = IState.bfs(state, endState);
			//Debug.Log("NextRight: " + temp.rightDevils + " " + temp.rightPriests);
			//Debug.Log("NextLeft: " + temp.leftDevils + " " + temp.leftPriests);
			hint = "提示:\n下一步之后\n" + "Right:  Devils: " + temp.rightDevils + "   Priests: " + temp.rightPriests +
				"\nLeft:  Devils: " + temp.leftDevils + "   Priests: " + temp.leftPriests;
			//int priestsOffset = temp.leftPriests - state.leftPriests;
			//int devilsOffset = temp.leftDevils - state.leftDevils;
			//Debug.Log("offset: " + priestsOffset + " " + devilsOffset);
			//controller.AIMove(priestsOffset, devilsOffset);
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
	void OnMouseDown() {
		if (status != 1) {
			if (gameObject.name == "boat") {
				action.moveBoat ();
				int rightPriest = controller.fromCoast.getCharacterNum()[0];
				int rightDevil = controller.fromCoast.getCharacterNum()[1];
				int leftPriest = controller.toCoast.getCharacterNum()[0];
				int leftDevil = controller.toCoast.getCharacterNum()[1];
				bool location = controller.boat.get_to_or_from() == -1 ? true : false;
				int pcount = controller.boat.getCharacterNum()[0];
				int dcount = controller.boat.getCharacterNum()[1];
				if (location) {
					leftPriest += pcount;
					leftDevil += dcount;
				} else {
					rightPriest += pcount;
					rightDevil += dcount;
				}
				state = new IState(leftPriest, leftDevil, rightPriest, rightDevil, location , null);
			} else {
				action.characterIsClicked (characterController);
			}
		}
	}
}