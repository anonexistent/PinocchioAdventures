using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacertestNewMethod : MonoBehaviour
{
    public Transform Player;
    public Chunk DefaultChunk;

    void Start()
    {

    }

    void Update()
    {
        CheckPosition();
    }

    public void CheckPosition()
    {
        var pP = Math.Round(Player.position.x, 1);
        var cP = Math.Round(DefaultChunk.endChunk.transform.position.x * 0.8f, 1) ;
        Debug.Log(pP + "—" + cP);
        if (pP == cP)
        {
            SpawnPlease();
            Debug.Log("new chunk");
        }
    }

    private void SpawnPlease()
    {
        //var newC = Instantiate(PrefChunks[UnityEngine.Random.Range(0, PrefChunks.Length)], chunkParent.transform);
        //newC.transform.position = CurrentChunks[CurrentChunks.Count - 1].endChunk.position - newC.startChunk.localPosition;
        //CurrentChunks.Add(newC);

        //if (CurrentChunks.Count > 10)
        //{
        //    Destroy(CurrentChunks[0].gameObject);
        //    CurrentChunks.RemoveAt(0);
        //}
    }
}
