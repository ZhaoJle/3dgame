using System.Collections.Generic;
using UnityEngine;

public enum GameStatus {
    Play, end
}

public enum SceneStatus {
    Waiting, Shooting
}

public interface IUserInterface {
    void SendUFO();
    void DestroyUFO(GameObject ufo);
}

public interface IScore {
	void AddScore();
    int GetScore();
}

public interface IQueryStatus {
    GameStatus QueryGameStatus();
    SceneStatus QuerySceneStatus();
}

public interface ISetStatus {
    void SetGameStatus(GameStatus gameStatus);
    void SetSceneStatus(SceneStatus scenceStatus);
}

public interface IActionController {
	int GetRound();
	int GetUFONum();
	void SendUFO(List<GameObject> usingUFOs);
	void DestroyUFO(GameObject UFO);
	void SceneUpdate();
}