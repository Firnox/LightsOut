using UnityEngine;

public class GameManager : MonoBehaviour {
  [SerializeField] Tile tilePrefab;
  [SerializeField] Transform gameArea;

  private static readonly int numRows = 5;
  private static readonly int numCols = 5;
  private Tile[,] tiles = new Tile[numRows, numCols];

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    // Instantiate tiles in the game area.
    for (int row = 0; row < numRows; row++) {
      for (int col = 0; col < numCols; col++) {
        // We center the grid at 0,0 in the parent.
        Vector3 pos = new(col - (numCols / 2), row - (numRows / 2));
        tiles[row, col] = Instantiate(tilePrefab, gameArea);
        tiles[row, col].name = $"Tile({row},{col})";
        tiles[row, col].transform.localPosition = pos;
      }
    }
  }
}


