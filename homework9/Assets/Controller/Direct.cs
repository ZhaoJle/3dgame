using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using myGame;

namespace myGame{
	public class Direct : System.Object {
		private static Direct _instance;
		public SceneController currentSceneController { get; set; }

		public static Direct getInstance() {
			if (_instance == null) {
				_instance = new Direct ();
			}
			return _instance;
		}
	}
		
}
