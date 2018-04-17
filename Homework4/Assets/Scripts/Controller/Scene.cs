

using System.Collections.Generic;
using UnityEngine;

public class Scene : MonoBehaviour {
    public int Round { get; set; }
    public int UFONum { get; private set; }
    private UFOModel ufoModel = new UFOModel();
    
    List<GameObject> inUseUFOs;

    public void Reset(int round) {
        Round = round;
        UFONum = round;
        ufoModel.Reset(round);
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
			float RanX = Random.Range(-5f, 5f);
			print (RanX);
			Vector3 startPos = new Vector3 (RanX, 3f, -15f);
			Vector3 startDirection =new Vector3 (-RanX, 8f, 3f);
            usingUFOs[i].transform.position = new Vector3(startPos.x, startPos.y + i, startPos.z);

            Rigidbody rigibody;
            rigibody = usingUFOs[i].GetComponent<Rigidbody>();
            rigibody.WakeUp();
            rigibody.useGravity = true;
            rigibody.AddForce(startDirection * Random.Range(ufoModel.UFOSpeed * 5, ufoModel.UFOSpeed * 8) / 5, 
                ForceMode.Impulse);

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
        Reset(Round);
    }

    private void Update() {
		if (Round == 10)
			FirstSceneControllerBase.GetFirstSceneControllerBase ().SetGameStatus (GameStatus.end);
        if (inUseUFOs != null) {
            for (int i = 0; i < inUseUFOs.Count; i++) {
				if (inUseUFOs[i].transform.position.y <= 1f) {
   
                    FirstSceneControllerBase.GetFirstSceneControllerBase().DestroyUFO(inUseUFOs[i]);
                }
            }
            if (inUseUFOs.Count == 0) {
                FirstSceneControllerBase.GetFirstSceneControllerBase().SetSceneStatus(SceneStatus.Waiting);
            }
        }
    }
}