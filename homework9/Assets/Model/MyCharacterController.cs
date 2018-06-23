using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace myGame{
	public class MyCharacterController {
		public readonly float speed = 10;
		readonly GameObject character;
		readonly Moveable moveableScript;
		readonly UserGUI UserGUI;
		readonly int characterType;	
		public static IState state = new IState(0, 0, 3, 3, false, null);
		public static IState endState = new IState(3, 3, 0, 0, true, null);

		bool _isOnBoat;
		CoastController coastController;


		public MyCharacterController(string which_character) {

			if (which_character.Contains("priest")) {
				character = Object.Instantiate (Resources.Load ("Perfabs/Priest", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
				characterType = 0;
			} else {
				character = Object.Instantiate (Resources.Load ("Perfabs/Devil", typeof(GameObject)), Vector3.zero, Quaternion.identity, null) as GameObject;
				characterType = 1;
			}
			moveableScript = character.AddComponent (typeof(Moveable)) as Moveable;
			UserGUI = character.AddComponent (typeof(UserGUI)) as UserGUI;
			UserGUI.setController (this);

		}

		public void setName(string name) {
			character.name = name;
		}

		public void setPosition(Vector3 pos) {
			character.transform.position = pos;
		}

		public GameObject getCharacter()
		{
			return character;
		}

		public Vector3 getPosition()
		{
			return character.transform.position;
		}

		public void moveToPosition(Vector3 destination) {
			moveableScript.setDestination(destination);
		}

		public int getType() {	
			return characterType;
		}

		public string getName() {
			return character.name;
		}

		public void getOnBoat(BoatController boatCtrl) {
			coastController = null;
			character.transform.parent = boatCtrl.getGameobj().transform;
			_isOnBoat = true;
		}

		public void getOnCoast(CoastController coastCtrl) {
			coastController = coastCtrl;
			character.transform.parent = null;
			_isOnBoat = false;
		}

		public bool isOnBoat() {
			return _isOnBoat;
		}

		public CoastController getCoastController() {
			return coastController;
		}

		public void reset() {
			moveableScript.reset ();
			coastController = (Direct.getInstance ().currentSceneController as gameController).fromCoast;
			getOnCoast (coastController);
			setPosition (coastController.getEmptyPosition ());
			coastController.getOnCoast (this);
		}
	}

}