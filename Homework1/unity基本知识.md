1. 解释 游戏对象（GameObjects） 和 资源（Assets）的区别与联系。
    
    答:
        
        联系：有些资源可作为模板，可实例化成游戏中具体的对象。
    
        区别：对象一般直接出现在游戏的场景中，是资源整合的具体表现，是资源整合的具体表现；而资源是为对象服务而使用的，多个不同的对象可以享用共同的资源。

2. 下载几个游戏案例，分别总结资源、对象组织的结构（指资源的目录组织结构与游戏对象树的层次结构）
    
    答:
        
        组织结构：对象一般是人物，物体，比如玩家、敌人、环境和摄像机等；而资源一般为对象服务的，比如材质、场景、声音、预设、贴图、脚本、动作等。

3. 编写一个代码，使用 debug 语句来验证 MonoBehaviour 基本行为或事件触发的条件

    答：

        publicclassNewBehaviourScript : MonoBehaviour {

            void Awake() {

                Debug.Log(“Awake！”);

            }

            void Start() {

                Debug.Log(“Start！”);

            }

            void Update() {

                Debug.Log(“Update！”);

            }

            void OnGUI() {

                Debug.Log(“OnGUI！”);

            }

            void FixedUpdate() {

                Debug.Log(“FixedUpdate！”);

            }

            void LateUpdate() {

                Debug.Log(“LateUpdate！”);

            }

            void Reset() {

                Debug.Log(“Reset！”);

            }

            void OnDisable() {

                Debug.Log(“OnDisable！”);

            }

            void OnDestroy() {

                Debug.Log(“OnDestroy！”);

            }

        }
        Awake()在组件创建时调用
        Start()在脚本调用时开始调用
        Update()逐帧调用
        FixedUpdate()按时间调用
        lateUpdate()在Update()之后逐帧调用
        OnGui()在之前的函数调用完之后调用
        Reset()重置时调用
        OnDisable()组件不可用时调用
        OnDestroy()组件销毁时调用


 

4. 查找脚本手册，了解 GameObject，Transform，Component 对象

    * 分别翻译官方对三个对象的描述（Description）

        GameObject:代表任务，道具和场景的基础对象。

        Transform:决定场景中游戏对象的位置，大小和旋转关系.

        Component:游戏对象和其对应行为之间的枢纽。

    * 描述下图中 table 对象（实体）的属性、table 的 Transform 的属性、 table 的部件

        ![图片](https://pmlpml.github.io/unity3d-learning/images/ch02/ch02-homework.png)

        + 本题目要求是把可视化图形编程界面与 Unity API 对应起来，当你在 Inspector 面板上每一个内容，应该知道对应 API。

        + 例如：table 的对象是 GameObject，第一个选择框是 activeSelf 属性。

            GameObject

                layer : Default

                tag : Untagged

            Transform

                Position: (0, 0, 0)

                Rotation: (0, 0, 0)

                Scale : (1, 1, 1)

            Component

                Transform

                Mesh Renderer

                Box Collider
    
    * 用 UML 图描述 三者的关系（请使用 UMLet 14.1.1 stand-alone版本出图）
    
        ![uml](https://wx3.sinaimg.cn/mw690/ea3f6ec5gy1fprbe1v7r8j20fk05u3yl.jpg)
        


 

5. 整理官方和其他学习材料，介绍如何用程序（代码）管理一个场景的游戏对象树林。编写简单代码验证以下技术的实现：
    o   查找对象

    o   添加子对象

    o   遍历对象树

    o   清除所有子对象

    答： 通过名字查找：

        public GameObject.Find(“string testname”)

        通过标签查找单个对象：

        public GameObject.FindWithTag(“stringtagname”)

        通过标签查找一组对象：

        public GameObject.FindGameObjectWithTag(“stringtagname”)

        添加子对象：

        public GameObject.CreatePrimitive(PrimitiveType.name)

        遍历对象树：

        foreach(Transform child in transform) {

        Debug.Log(child.gameObject.name);

        }

        清除所有子对象：

        foreach(Transform child in transform) {

        Destroy(child.gameObject);

        }

6. 预设有什么好处？与对象克隆 (clone or copy or Instantiate of Unity Object) 关系？

    答：
        预设就是把一个制作好了的游戏对象模板化，用于批量的套用工作，产生的对象和模板本质属性相同，但生产的位置和角度以及生成后的一些属性是允许发生变化的，游戏的场景中需要大量相同的实例就可以用预设实现。而且预设发生变化，由此预设产生的所有实例都会跟着变化。而对象克隆出来的实例不会随本体变化而变化。

7. 解释组合模式。并编写验证程序，向组合对象中子对象 cast 消息， 验证组合模式的优点。

    答：
        组合模式，将对象组合成树形结构以表示“部分-整体”的层次结构，组合模式使得用户对单个对象和组合对象的使用具有一致性。有时候又叫做部分-整体模式，它使我们树型结构的问题中，模糊了简单元素和复杂元素的概念，客户程序可以像处理简单元素一样来处理复杂元素。

    子类对象（my_obj）方法：

        void TestFun() {

                  Debug.Log("Hello!");

        }

    父类对象（GameObject）方法：

        void Start () {

                  this.BroadcastMessage("TestFun");

        }

    结构：

        GameObject

        |----

        my_obj

        |----

        my_obj

 

    运行结果：

        Hello!

        Hello!