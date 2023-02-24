using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private float spawnIntervalMin;
    [SerializeField] private float spawnIntervalMax;
    [SerializeField] private float cubeLifeTime;

    private bool isSpawn = false;
    private float nextSpawnTime;
    private GameObject currentBlock;
    
    // Start is called before the first frame update
    void Start()
    {
        nextSpawnTime = Time.time + Random.Range(spawnIntervalMin, spawnIntervalMax);
    }

    private void Update() 
    {
        if(isSpawn && Time.time >= nextSpawnTime )
        {
            // kiểm tra nếu block prefab đã tồn tại trên scene thì chờ cho đến khi nó được huỷ
            if(currentBlock && currentBlock.activeInHierarchy)
            {
                return;
            } 
            // lấy màu sắc ngẫu nhiên từ danh sách material
            Color material = GameManager.instance.GetRandomBlockMaterial();

            currentBlock = BlockPool.instance.GetBlockFromPool();
            currentBlock.transform.position = transform.position;
            currentBlock.transform.rotation = Quaternion.identity;
            currentBlock.GetComponent<Renderer>().material.color = material;

            // thiết lập thời gian spawn kế tiếp
            nextSpawnTime = Time.time + Random.Range( spawnIntervalMin, spawnIntervalMax);

            // tắt đối tượng block sau 1 khoảng thời gian
            StartCoroutine(ReturnBlockToPoolAfterDelay(currentBlock, cubeLifeTime));
        }
    }

    public void StartSpawn()
    {
        isSpawn = true;
    }

    public void StopSpawn()
    {
        isSpawn = false;
    
    }
    private IEnumerator ReturnBlockToPoolAfterDelay(GameObject block, float delay)
    {
        yield return new WaitForSeconds(delay);
        BlockPool.instance.ReturnBlockToPool(block);
    }
}