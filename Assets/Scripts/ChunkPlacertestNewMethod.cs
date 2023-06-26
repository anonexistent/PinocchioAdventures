using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ChunkPlacertestNewMethod : MonoBehaviour
{
    public Transform Player;
    public Chunk DefaultChunk;
    public Chunk[] PrefsChs;
    public List<Chunk> CursChs = new();

    void Start()
    {
        CursChs.Add(DefaultChunk);
    }

    void Update()
    {
        CheckPosition();
    }

    public void CheckPosition()
    {
        var pP = Math.Round(Player.position.x, 1);
        var cP = Math.Round(CursChs[CursChs.Count-1].endChunk.transform.position.x * 0.9f, 1) ;
        //Debug.Log("player : " + pP + "chunk end : " + cP);
        if (pP == cP)
        {
            SpawnPlease();
            Debug.Log("new chunk");

        }
    }

    private void SpawnPlease()
    {
        var a = Instantiate(PrefsChs[UnityEngine.Random.Range(0, PrefsChs.Length)], GameObject.Find("plane").transform);
        //newC.transform.position = CurrentChunks[CurrentChunks.Count - 1].endChunk.position - newC.startChunk.localPosition;
        a.transform.position = CursChs[CursChs.Count - 1].endChunk.position - a.startChunk.localPosition;
        CursChs.Add(a);
        Debug.Log("new chunk position" + a.transform.position + " end of new chunk (90%) / player position" +a.endChunk.position.x * 0.9f + "\\" + Player.position.x);

        if (CursChs.Count > 20)
        {
            Destroy(CursChs[0].gameObject);
            CursChs.Remove(CursChs[0]);
        }
    }
}
