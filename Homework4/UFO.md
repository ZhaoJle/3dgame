+ 编写一个简单的鼠标打飞碟（Hit UFO）游戏

    - 游戏内容要求：
        游戏有 n 个 round，每个 round 都包括10 次 trial；
        每个 trial 的飞碟的色彩、大小、发射位置、速度、角度、同时出现的个数都可能不同。它们由该 round 的 ruler 控制；
        每个 trial 的飞碟有随机性，总体难度随 round 上升；
        鼠标点中得分，得分规则按色彩、大小、速度不同计算，规则可自由设定。
    - 游戏的要求：
        使用带缓存的工厂模式管理不同飞碟的生产与回收，该工厂必须是场景单实例的！具体实现见参考资源 Singleton 模板类
        近可能使用前面 MVC 结构实现人机交互与游戏模型分离
        如果你的使用工厂有疑问，参考：弹药和敌人：减少，重用和再利用
+ 游戏视频地址：https://www.bilibili.com/video/av22236111/

+ 游戏主要分为三个部分：飞碟工厂，动作，场景。下面是主要部分：

    - UFO，包括UFO的属性以及UFO工厂，负责UFO的准备与回收。

        UFOModel:
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

        UFOFactory:
                public GameObject UFOPrefab;
                private static UFOFactoryBase ufoFactory;
                List<GameObject> inUseUFO;
                List<GameObject> notUseUFO;

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

        Controller，包括UFO的发送，销毁，分数管理，游戏状态的设置。

                private GameStatus gameStatus;
                private SceneStatus sceneStatus;
                public Scene scene;
                private UFOFactoryBase ufoFactory = UFOFactoryBase.GetFactory();

                public void SendUFO() {
                int UFOCount = scene.UFONum;
                    var UFOList = ufoFactory.PrepareUFO(UFOCount);
                    scene.SendUFO(UFOList);
                }

                public void DestroyUFO(GameObject UFO) {
                    scene.DestroyUFO(UFO);
                    ufoFactory.RecycleUFO(UFO);
                }

                public GameStatus QueryGameStatus() {
                    return gameStatus;
                }
                public SceneStatus QuerySceneStatus() {
                    return sceneStatus;
                }

                public void SetGameStatus(GameStatus _gameStatus) {
                    gameStatus = _gameStatus;
                }
                public void SetSceneStatus(SceneStatus _sceneStatus) {
                    sceneStatus = _sceneStatus;
                }

                public void AddScore() {
                    GameModel.GetGameModel().AddScore();
                }
                public int GetScore() {
                    return GameModel.GetGameModel().Score;
                }

                public void Update() {
                    scene.SceneUpdate();
                }

    - Action，这次没有做动作分离，所以这是对用户鼠标键盘动作的响应：

            using UnityEngine;

            public class UserAction : MonoBehaviour {
                public GameObject planePrefab;
                
                GameStatus gameStatus;
                SceneStatus SceneStatus;

                IUserInterface uerInterface;
                IQueryStatus queryStatus;
                IScore changeScore;

                // Use this for initialization
                void Start() {
                    GameObject plane = Instantiate(planePrefab);
                    plane.transform.position = new Vector3(0f, 0f, 70f);

                    gameStatus = GameStatus.Play;
                    SceneStatus = SceneStatus.Waiting;
                    uerInterface = FirstSceneControllerBase.GetFirstSceneControllerBase() as IUserInterface;
                    queryStatus = FirstSceneControllerBase.GetFirstSceneControllerBase() as IQueryStatus;
                    changeScore = FirstSceneControllerBase.GetFirstSceneControllerBase() as IScore;
                }

                // Update is called once per frame
                void Update() {
                    gameStatus = queryStatus.QueryGameStatus();
                    SceneStatus = queryStatus.QuerySceneStatus();

                    if (gameStatus == GameStatus.Play) {
                        if (SceneStatus == SceneStatus.Waiting && Input.GetKeyDown("space")) {
                            uerInterface.SendUFO();
                            FirstSceneControllerBase.GetFirstSceneControllerBase().Update();
                        }
                        if (SceneStatus == SceneStatus.Shooting && Input.GetMouseButtonDown(0)) {
                            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                            RaycastHit hit;
                            if (Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "UFO") {
                                uerInterface.DestroyUFO (hit.collider.gameObject);
                                changeScore.AddScore();
                            }
                        }
                    }
                }
            }

    - 场景，飞碟的动作包含在了里面

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
        
        - UI，主体界面

                using System;
                using UnityEngine;
                using UnityEngine.UI;

                public class UI : MonoBehaviour {
                    GameObject scoreText;
                    GameObject gameStatuText;
                    IScore score = FirstSceneControllerBase.GetFirstSceneControllerBase() as IScore;
                    IQueryStatus gameStatu = FirstSceneControllerBase.GetFirstSceneControllerBase() as IQueryStatus;

                    // Use this for initialization
                    void Start() {
                        scoreText = GameObject.Find("Score");
                        gameStatuText = GameObject.Find("GameStatu");
                    }

                    // Update is called once per frame
                    void Update() {
                        string score = Convert.ToString(this.score.GetScore());
                        if (gameStatu.QueryGameStatus() == GameStatus.end)
                            gameStatuText.GetComponent<Text>().text = "Your Score:" + score;
                        scoreText.GetComponent<Text>().text = "score:" + score;
                    }
                }
 
