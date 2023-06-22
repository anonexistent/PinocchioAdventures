//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using UnityEngine;

//public class ChunkPlacer : MonoBehaviour
//{
//    public Transform Player;
//    [SerializeField]
//    public SpawnObjs objsSpawner;
//    public Chunk defaultChunk;
//    public Chunk[] PrefChunks;
//    List<Chunk> CurrentChunks = new();
//    [SerializeField]
//    Transform chunkParent;

//    int curObjsCount = 3;

//    void Start()
//    {
//        CurrentChunks.Add(defaultChunk);
//    }

//    void Update()
//    {
//        // 80% curChunck
//        if(Player.position.x > (CurrentChunks[CurrentChunks.Count - 1].endChunk.position.x) * 0.8f)
//        {            
//            SpawnNewChunk();
//            for (int i = 0; i < curObjsCount; i++) objsSpawner.ReplaceObject(CurrentChunks[^1]);

//        }
//    }

//    void SpawnNewChunk()
//    {
//        var newC = Instantiate(PrefChunks[UnityEngine.Random.Range(0, PrefChunks.Length)], chunkParent.transform);
//        newC.transform.position = CurrentChunks[CurrentChunks.Count - 1].endChunk.position - newC.startChunk.localPosition;
//        CurrentChunks.Add(newC);

//        if (CurrentChunks.Count > 6) 
//        {
//            Destroy(CurrentChunks[0].gameObject);
//            CurrentChunks.RemoveAt(0);
//        }
//    }
//}

using System.Collections.Generic;
using UnityEngine;

public class ChunkPlacer : MonoBehaviour
{
    public Transform Player;
    [SerializeField]
    public SpawnObjs objsSpawner;
    public Chunk defaultChunk;
    public Chunk[] PrefChunks;
    List<Chunk> CurrentChunks = new();
    [SerializeField]
    Transform chunkParent;

    double temp = 0.001f;

    int curObjsCount = 3;

    void Start()
    {
        CurrentChunks.Add(defaultChunk);
    }

    void Update()
    {
        //// 90% curChunck
        //float endOfCurCh = CurrentChunks[CurrentChunks.Count - 1].endChunk.position.x;
        //if (Player.position.x > endOfCurCh * (0.8f + temp))
        //{            
        //    if (temp >= 0.09f) temp = 0;
        //    temp += 0.001f;
        //    SpawnNewChunk();
        //    Debug.Log("=======\nplayer :" + Player.position.x + $"\nchunk : {CurrentChunks[CurrentChunks.Count-1].endChunk.position.x}=======");
        //    for (int i = 0; i < curObjsCount; i++) objsSpawner.ReplaceObject(CurrentChunks[^1]);
        //}

        //if(Player.position.x > CurrentChunks[CurrentChunks.Count-1].transform)
        
    }

    void SpawnNewChunk()
    {
        var newC = Instantiate(PrefChunks[UnityEngine.Random.Range(0, PrefChunks.Length)], chunkParent.transform);
        newC.transform.position = CurrentChunks[CurrentChunks.Count - 1].endChunk.position - newC.startChunk.localPosition;
        CurrentChunks.Add(newC);

        if (CurrentChunks.Count > 10)
        {
            Destroy(CurrentChunks[0].gameObject);
            CurrentChunks.RemoveAt(0);
        }
    }
}
