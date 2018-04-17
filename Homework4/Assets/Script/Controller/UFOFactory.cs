using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOFactory : MonoBehaviour {

	public GameObject UFOPrefab;

	List<GameObject> inUseUFO = new List<GameObject>();
	List<GameObject> notUseUFO = new List<GameObject>();

	void Awake(){
		
	}


	public List<GameObject> PrepareUFO(int UFOnum) {
		for (int i = 0; i < UFOnum; i++) {
			if (notUseUFO.Count == 0) {
				GameObject disk = Object.Instantiate(UFOPrefab);
				inUseUFO.Add(disk);
			} else {
				GameObject disk = notUseUFO[0];
				notUseUFO.RemoveAt(0);
				inUseUFO.Add(disk);
			}
		}
		return inUseUFO;
	}

	public void RecycleUFO(GameObject UFO) {
		int index = inUseUFO.FindIndex(x => x == UFO);
		notUseUFO.Add(UFO);
		inUseUFO.RemoveAt(index);
	}
}

