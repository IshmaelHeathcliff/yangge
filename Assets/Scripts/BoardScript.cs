using UnityEngine;

public class BoardScript : MonoBehaviour
{
    public int columns = 15;
    public int rows = 8;
    public GameObject wall;
    public GameObject cloud;
    public GameObject backgroud;
    private Transform boardHolder;

    public void BGSetup()
    {
        boardHolder = new GameObject("board").transform;
        for (int x = -1; x < columns + 2; x++)
        {
            for (int y = -1; y < rows + 2; y++)
            {
                GameObject toInstantiate = backgroud;
                if (y == -1 || x == -1 || x == columns + 1) toInstantiate = wall;
                if (y == rows + 1) toInstantiate = cloud;
                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0), Quaternion.identity) as GameObject;
                instance.transform.SetParent(boardHolder);
            }
        }
    }
}
