using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEnemy : MonoBehaviour
{
    public GameObject PrefabEnemy; // 生成するプレハブ格納用

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // 30フレーム毎にシーンにプレハブを生成
        if(Time.frameCount % Random.Range (95, 100)  == 0)
        {
            Vector3 pos = new Vector3(10, -2);
 
            // プレハブを生成
            Instantiate(PrefabEnemy, pos, Quaternion.identity);
        }    
    }
}
