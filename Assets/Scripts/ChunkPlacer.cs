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

using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data.Common;
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
        // 80% curChunck
        if (Player.position.x > (CurrentChunks[CurrentChunks.Count - 1].endChunk.position.x) * (0.8f + temp))
        {
            //if (temp >= 0.9f) temp = 0;
            temp += 0.001f;
            SpawnNewChunk();
            for (int i = 0; i < curObjsCount; i++) objsSpawner.ReplaceObject(CurrentChunks[^1]);
        }
    }

    public class DBConnection
    {
        private DBConnection()
        {
        }

        public string Server { get; set; }
        public string DatabaseName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        private MySqlConnection Connection { get; set; }

        private static DBConnection _instance = null;
        public static DBConnection Instance()
        {
            if (_instance == null)
                _instance = new DBConnection();
            return _instance;
        }

        public bool IsConnect()
        {
            if (Connection == null)
            {
                if (String.IsNullOrEmpty(DatabaseName))
                    return false;
                string connstring = string.Format("Server={0}; database={1}; UID={2}; password={3}", Server, DatabaseName, UserName, Password);
                Connection = new MySqlConnection(connstring);
                Connection.Open();
            }

            return true;
        }

        public void Close()
        {
            Connection.Close();
        }
    }

    void SpawnNewChunk()
    {
        var newC = Instantiate(PrefChunks[UnityEngine.Random.Range(0, PrefChunks.Length)], chunkParent.transform);
        newC.transform.position = CurrentChunks[CurrentChunks.Count - 1].endChunk.position - newC.startChunk.localPosition;
        CurrentChunks.Add(newC);

        if (CurrentChunks.Count > 15)
        {
            Destroy(CurrentChunks[0].gameObject);
            CurrentChunks.RemoveAt(0);
        }
    }
}
