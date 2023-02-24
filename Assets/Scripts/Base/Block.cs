using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    private Rigidbody2D rb;
    private GameObject gO;
    private ColorChanger colorChanger;
    private Color material;
    private void Start()
    {
        gO = this.gameObject;
        colorChanger = gO.GetComponent<ColorChanger>();
        rb = GetComponent<Rigidbody2D>();
        material = GameManager.instance.GetRandomBlockMaterial();
        gO.transform.GetComponent<SpriteRenderer>().color = material;
    }

    private void Update() 
    {
        // cập nhật vị trí của prefab
        Vector2 pos = rb.position;
        pos += Vector2.left * moveSpeed * Time.deltaTime;
        rb.MovePosition(pos);

        // xoay ngược chiều kim đồng hộ
        transform.Rotate(Vector3.back, -rotationSpeed * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        // xử lí va chạm giữa prefabs và collider khác
        if(other.gameObject.tag == "Player")
        {
            // sử dụng tính năng của class ColorChanger để thay đổi màu sắc các đối tượng
                colorChanger.ChangeMaterial(material);
            
            // khi va chạm với Player thì prefabs sẽ được trả lại blockpool
            BlockPool.instance.ReturnBlockToPool(gameObject);
        }
    }
}
