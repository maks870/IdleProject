using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [SerializeField] private Vector2 chunkSize;
    [SerializeField] private GameObject[] chunksLeft;
    [SerializeField] private GameObject[] chunksCenter;
    [SerializeField] private GameObject[] chunksRight;
    private GameObject[][] chunks = new GameObject[3][];
    private Camera camera;
    public Vector2Int vecint;
    void Start()
    {
        camera = Camera.main;
        chunks[0] = chunksLeft;
        chunks[1] = chunksCenter;
        chunks[2] = chunksRight;
    }
    //|0,0|1,0|2,0|
    //|0,1|1,1|2,1|
    //|0,2|1,2|2,2|
    void Update()
    {
        Vector2 vec = camera.transform.position - chunks[1][1].transform.position;
        vec = vec / chunkSize;
        vecint = Vector2Int.FloorToInt(vec);
        if (vecint != Vector2Int.zero)
        {
            NextChunk(vecint);
        }
    }

    private void NextChunk(Vector2Int vector)
    {
        Vector2Int numberChunk = new Vector2Int(1 - vector.x, vector.y + 1);
    
        if (vector.y != 0)
            for (int x = 0; x < chunks.Length; x++)
            {
                GameObject chunk = chunks[x][numberChunk.y];
                Vector3 newPos = new Vector3(0, chunkSize.y * vector.y * chunks.Length, 0);
                chunk.transform.position += newPos;

                for (int y = 0; y < chunks.Length - 1; y++)
                {
                    chunks[x][Mathf.Abs(numberChunk.y - y)] = chunks[x][Mathf.Abs(numberChunk.y - y - 1)];
                }

                chunks[x][chunks.Length - 1 - numberChunk.y] = chunk;
            }
        if (vector.x != 0)
            for (int y = 0; y < chunks.Length; y++)
            {
                GameObject chunk = chunks[numberChunk.x][y];
                Vector3 newPos = new Vector3(chunkSize.x * vector.x * chunks.Length, 0, 0);
                chunk.transform.position += newPos;

                for (int x = 0; x < chunks.Length - 1; x++)
                {
                    chunks[Mathf.Abs(numberChunk.x - x)][y] = chunks[Mathf.Abs(numberChunk.x - x - 1)][y];
                }

                chunks[chunks.Length - 1 - numberChunk.x][y] = chunk;
            }
    }
}



