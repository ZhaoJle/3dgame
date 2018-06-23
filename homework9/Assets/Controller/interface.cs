using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace myGame{

		public interface SceneController {
			void loadResources ();
		}

		public interface UserAction {
			void moveBoat();
			void characterIsClicked(MyCharacterController characterCtrl);
			void restart();
		}

}
