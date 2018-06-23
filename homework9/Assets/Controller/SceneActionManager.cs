using System.Collections.Generic;
using UnityEngine;
using myGame;

public class SceneActionManager : ActionManager {
	public void MoveBoat(BoatController boatController) {
		SSMoveToAction action = SSMoveToAction.GetSSMoveToAction(boatController.getDestination(), boatController.speed);
		AddAction(boatController.getBoat(), action, this);
	}

	public void MoveCharacter(myGame.MyCharacterController characterCtrl, Vector3 destination) {
		Vector3 currentPos = characterCtrl.getPosition();
		Vector3 middlePos = currentPos;
		if (destination.y > currentPos.y) middlePos.y = destination.y;
		else {
			middlePos.x = destination.x;
		}
		SSAction action1 = SSMoveToAction.GetSSMoveToAction(middlePos, characterCtrl.speed);
		SSAction action2 = SSMoveToAction.GetSSMoveToAction(destination, characterCtrl.speed);
		SSAction seqAction = SequenceAction.GetSequenceAction(1, 0, new List<SSAction> { action1, action2 });
		AddAction(characterCtrl.getCharacter(), seqAction, this);
	}
}