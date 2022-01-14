using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    float speed = 5;    //とりあえずデフォルト値を5としておく
    public float Speed
    {
        set
        {
            speed = value;
        }
    }
    void Update()
    {
        transform.position = new Vector2(transform.position.x - speed * Time.deltaTime, transform.position.y);
    }
    //サイズと位置を調整
    public void SetWall(Vector2 size)
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.size = size;
        transform.position = new Vector2(transform.position.x, transform.position.y + size.y / 2);
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Debug.Log("ぶつかったよ！ゲームオーバー！");
            Destroy(gameObject);
        }
    }

    //画面外に出たら破棄（※テストプレイ時にシーンビューに映っていると破棄されないので注意）
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}