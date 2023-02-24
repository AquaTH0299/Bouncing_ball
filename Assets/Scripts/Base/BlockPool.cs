using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockPool : MonoBehaviour
{
    [SerializeField] private GameObject blockPrefab;
    [SerializeField] private int poolSize;

    private Queue<GameObject> blockPool;

   public static BlockPool instance;
    private void Awake() 
    {
        instance = this; // gán instance bằng lớp hiện tại khi đối tượng được gọi
    }
    // Start is called before the first frame update
    void Start()
    {
        blockPool = new Queue<GameObject>();
        for (int i = 0; i < poolSize; i++)
        {
            GameObject block = Instantiate(blockPrefab, transform);
            block.SetActive(false);
            blockPool.Enqueue(block);   
        }
    }

    public GameObject GetBlockFromPool()
    {
        if(blockPool.Count > 0)
        {
            GameObject block = blockPool.Dequeue();
            block.SetActive(true);
            return block;
        }
        else
        {
            GameObject block = Instantiate(blockPrefab, transform);
            block.SetActive(true);
            return block;
        }
    }

    public void ReturnBlockToPool(GameObject block)
    {
        block.SetActive(false);
        blockPool.Enqueue(block);
    }
}
