
public class GameModel : IScore {

    public int Score { get; private set; }
    public int Round { get; private set; }
    private static GameModel gameModel;

    private GameModel() {
        Round = 1;
    }

    public static GameModel GetGameModel() {
        return gameModel ?? (gameModel = new GameModel());
    }

	public void AddScore() {
		Score += 10;       
    }
		

 

    public int GetScore() {
        return GameModel.GetGameModel().Score;
    }
}