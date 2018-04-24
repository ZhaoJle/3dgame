using System.Collections.Generic;
using UnityEngine;

public class CCActionManager : MonoBehaviour, IActionController {
	public int Round { get; set; }
	public int UFONum { get; private set; }
	private UFOModel ufoModel = new UFOModel();
	private Vector3 target;

	List<GameObject> inUseUFOs;

	public void Reset(int round) {
		Round = round;
		UFONum = round;
		ufoModel.Reset(round);
	}

	public int GetRound() {
		return Round;
	}

	public int GetUFONum() {
		return UFONum;
	}

	public void SendUFO(List<GameObject> usingUFOs) {
		inUseUFOs = usingUFOs;
		Reset(Round);
		Color UFOColor;
		for (int i = 0; i < usingUFOs.Count; i++) {
			int Ran = Random.Range (0, 3);
			if (Ran < 1)
				UFOColor = Color.red;
			else if (Ran < 2)
				UFOColor = Color.blue;
			else
				UFOColor = Color.green;
			usingUFOs [i].GetComponent<Renderer> ().material.color = UFOColor;
			Vector3 startPos;
			if (Round % 2 == 1) {
				startPos = new Vector3 (5f, 3f, -15f);
			} else {
				startPos =new Vector3 (-5f, 3f, -15f);
			}
			usingUFOs [i].transform.position = startPos;
			usingUFOs[i].transform.position = startPos;

			FirstSceneControllerBase.GetFirstSceneControllerBase().SetSceneStatus(SceneStatus.Shooting);
		}
	}

	public void DestroyUFO(GameObject UFO) {
		UFO.GetComponent<Rigidbody>().Sleep();
		UFO.GetComponent<Rigidbody>().useGravity = false;
		UFO.transform.position = new Vector3(0f, -99f, 0f);
	}

	public void SceneUpdate() {
		Round++;
		Reset(Round);
	}

	private void Start() {
		Round = 1;
		target = new Vector3(-3f, 10f, -2f);
		Reset(Round);
	}

	private void Update() {
		if (inUseUFOs != null) {
			for (int i = 0; i < inUseUFOs.Count; i++) {
				//Debug.Log(inUseUFOs[i].transform.localScale.y);
				if (inUseUFOs[i].transform.position == target) {
					//Debug.Log(inUseUFOs[i].transform.position.y);
					FirstSceneControllerBase.GetFirstSceneControllerBase().DestroyUFO(inUseUFOs[i]);
				} else {
					inUseUFOs[i].transform.position = Vector3.MoveTowards(inUseUFOs[i].transform.position, target, 5 * Time.deltaTime);
				}
			}
			if (inUseUFOs.Count == 0) {
				FirstSceneControllerBase.GetFirstSceneControllerBase().SetSceneStatus(SceneStatus.Waiting);
			}
		}
	}
}