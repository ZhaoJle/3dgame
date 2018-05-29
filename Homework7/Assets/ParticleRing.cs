using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleData {
  public float angle;                                       // 粒子初始角度
  public float radius;                                      // 当前粒子半径
  public float beforeRadius;                                // 粒子收缩前半径
  public float shringRadius;                                // 粒子收缩后半径

  public ParticleData(float angle, float radius, float beforeRadius, float shringRadius) {
    this.angle = angle;
    this.radius = radius;
    this.beforeRadius = beforeRadius;
    this.shringRadius = shringRadius;
  }
}

public class ParticleRing : MonoBehaviour {
  public Camera camera;                                     //摄像机
  public ParticleSystem particleRing;                       //粒子系统

  private int particleNum = 10000;                          //粒子数目
  private ParticleSystem.Particle[] particles;              //粒子
  private ParticleData[] particleDatas;                     //粒子数据

  private float minRadius = 8.0f;                           //最小半径
  private float maxRadius = 16.0f;                          //最大半径

  private int isShring;                            //是否收缩

  private int level = 5;
  private float speed = 0.2f;
  private float shringSpeed = 2f;
  private int count = 0;						//计数，每隔一段时间变色

	void OnGUI() {
		if (GUI.Button(new Rect(Screen.width/2-50,Screen.height/2-15, 100, 30), "Shring")) {
			isShring = (isShring==-1)? 0: 1-isShring;
		}
	}
		

  // Use this for initialization
  void Start() {
    particleRing.maxParticles = particleNum;                //设置最大粒子数
    particles = new ParticleSystem.Particle[particleNum];   //新建粒子数组
    particleDatas = new ParticleData[particleNum];          //新建粒子数据数组
    particleRing.Emit(particleNum);                         //粒子系统发射粒子
    particleRing.GetParticles(particles);
	isShring = -1;

    //初始化粒子位置
    for (int i = 0; i < particleNum; ++i) {
      float middleRadius = (maxRadius + minRadius) / 2;
      float upperbound = Random.Range(middleRadius, maxRadius);
      float lowerbound = Random.Range(minRadius, middleRadius);
      float radius = Random.Range(lowerbound, upperbound);
      float angle = Random.Range(0.0f, 360.0f);
      particleDatas[i] = new ParticleData(angle, radius, radius, radius - 4f * (radius / minRadius));
      // 随机收缩半径下界，避免收缩后成为圆线
      if (particleDatas[i].shringRadius < minRadius + 0.5f) {
        float temp = Random.Range(minRadius, minRadius + 0.25f);
        particleDatas[i].shringRadius = Random.Range(temp, minRadius + 0.5f);
      }
    }
  }

  // Update is called once per frame
  void Update() {
		float r = Random.Range (0f, 1f);
		float g = Random.Range (0f, 1f);
		float b = Random.Range (0f, 1f);
		Color t_color = new Color (r, g, b);
		count++;
    for (int i = 0; i < particleNum; ++i) {
      //判断是否收缩
      if (isShring==0) {
				if (count % 60 == 0) {
					particles [i].startColor = t_color;
				}
				if (i % 2 == 0) {
					//根据奇偶数判定收缩的粒子
					if (particleDatas [i].radius > particleDatas [i].shringRadius) {
						particleDatas [i].radius -= shringSpeed * (particleDatas [i].radius / particleDatas [i].shringRadius) * Time.deltaTime;
					}
				}
			} else if(isShring==1){
				particles [i].startColor = Color.white;		
        if (particleDatas[i].radius < particleDatas[i].beforeRadius) {
          particleDatas[i].radius +=
            shringSpeed * (particleDatas[i].beforeRadius / particleDatas[i].radius) * Time.deltaTime;
        } else {
          particleDatas[i].radius = particleDatas[i].beforeRadius;
        }
      }
      //根据奇偶数判定顺时针或逆时针运动
      if (i % 2 == 0) {
        particleDatas[i].angle += (i % level + 1) * speed;
      } else {
        particleDatas[i].angle -= (i % level + 1) * speed;
      }
      particleDatas[i].angle %= 360;
      float rad = particleDatas[i].angle / 180 * Mathf.PI;
      particles[i].position =
        new Vector3(particleDatas[i].radius * Mathf.Cos(rad), particleDatas[i].radius * Mathf.Sin(rad), 0);
    }
    particleRing.SetParticles(particles, particleNum);
    //收缩射线判断
  }

}
