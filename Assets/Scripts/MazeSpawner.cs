using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeSpawner : MonoBehaviour
{
    public Cell CellPrefab;
    public Vector3 CellSize = new Vector3(1,1,0);

    [SerializeField] private GameObject cubePlus, cubeMinus, cubeDiv, cubeMulti;
    //public HintRenderer HintRenderer;

    public Maze maze;

    private void Start()
    {
        MazeGenerator generator = new MazeGenerator();
        if (SceneManager.GetActiveScene().name == "Labyrinth lvl1" ||
            SceneManager.GetActiveScene().name == "Labyrinth lvl2")
        {
            generator.Height = 10;
            generator.Width = 10;
        }
        else
        {
            generator.Height = 10;
            generator.Width = 13;
        }
        maze = generator.GenerateMaze(cubePlus, cubeMinus, cubeDiv, cubeMulti);

        for (int x = 0; x < maze.cells.GetLength(0); x++)
        {
            for (int y = 0; y < maze.cells.GetLength(1); y++)
            {
                Cell c = Instantiate(CellPrefab, new Vector3(x * CellSize.x, y * CellSize.y, y * CellSize.z), Quaternion.identity);

                c.WallLeft.SetActive(maze.cells[x, y].WallLeft);
                c.WallBottom.SetActive(maze.cells[x, y].WallBottom);
            }
        }

        //HintRenderer.DrawPath();
    }
}
