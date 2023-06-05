using System.Collections.Generic;
using UnityEngine;

public class SpawnObjs : MonoBehaviour
{
    [Tooltip("for example tempForChunks")]
    public GameObject plane;
    public List<GameObject> objs = new();
    public static List<GameObject> curObjs = new();
    [Range(0f, 1f)]
    public float enemyChance;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReplaceObject(Chunk ch)
    {
        if(curObjs.Count > 20) 
        {
            Destroy(curObjs[0]);
            curObjs.RemoveAt(0); 
        }

        // last current chunk
        var a = plane.transform.GetChild(plane.transform.childCount - 1).gameObject;
        var curObjParent = objs[Random.RandomRange(0, objs.Count)];

        var rndChance = Random.Range(0.00001f, 0.99999f);
        //Debug.Log(rndChance.ToString());
        if(rndChance <= enemyChance && curObjParent.tag == "enemy")
        {
            BirthObject(ch);
        }
        else if(curObjParent.tag != "enemy")
        {
            BirthObject(ch);
        }

    }

    void BirthObject(Chunk ch)
    {
        var curObj = Instantiate(objs[Random.RandomRange(0, objs.Count)]);
        var newObjPos = RandomPointBetween2Points(ch.startChunk.transform.position, ch.endChunk.transform.position);
        curObj.transform.position = newObjPos + new Vector3(0, Random.value, 0);
        curObjs.Add(curObj);
    }

    private static Vector3 RandomPointBetween2Points(Vector3 start, Vector3 end)
    {
        return (start + Random.Range(0f, 1f) * (end - start));
    }
    //Vector3 RandomPosition()
    //{
    //    // random index between 0 and total count of items in gridPositions
    //    int randomIndex = Random.Range(0, gridPositions.Count);
    //    // random position selected from our gridPosition using randomIndex
    //    Vector3 randomPosition = gridPositions[randomIndex]
    //    // remove the entry from gridPosition so it can't be re-used
    //    gridPositions.RemoveAt(randomIndex);
    //    return randomPosition;
    //}
}
