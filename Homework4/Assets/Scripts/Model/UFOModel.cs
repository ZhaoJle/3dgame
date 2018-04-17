
using UnityEngine;

public class UFOModel {
    public float UFOSpeed;
    public void Reset(int round) {
        UFOSpeed = 0.1f;

        for (int i = 1; i < round; i++) {
			if( round<5)
            	UFOSpeed *= 1.02f;
        }
    }
}