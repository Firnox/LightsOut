using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
  [SerializeField] Tile tilePrefab;
  [SerializeField] Transform gameArea;

  private static readonly int numRows = 5;
  private static readonly int numCols = 5;
  private Tile[,] tiles = new Tile[numRows, numCols];

  enum GameMode {
    LevelSelect,
    Game,
    None
  }

  [SerializeField] private GameMode gameMode = GameMode.None;

  private int level = 0;

  private readonly List<(int, int)>[] levels = new List<(int, int)>[] {
    // Level 0: Cross
    new() { (0, 0), (1, 0), (0, 2), (1, 2), (0, 4), (1, 4) },
  };

  public void StartGame() {
    // Turn off all lights
    for (int row = 0; row < numRows; row++) {
      for (int col = 0; col < numCols; col++) {
        tiles[row, col].SetState(false);
      }
    }

    // Set the lights for the level.
    foreach ((int row, int col) in levels[level]) {
      tiles[row, col].SetState(true);
    }

    // Set the Game Mode to playing
    gameMode = GameMode.Game;
  }

  public void Toggle(int row, int col) {
    if (gameMode == GameMode.Game) {
      // Toggle the pressed tile.
      tiles[row, col].Toggle();

      // Toggle surrounding tiles.
      if (col > 0) { tiles[row, col - 1].Toggle(); }
      if (col + 1 < numCols) { tiles[row, col + 1].Toggle(); }
      if (row > 0) { tiles[row - 1, col].Toggle(); }
      if (row + 1 < numRows) { tiles[row + 1, col].Toggle(); }

    } else if (gameMode == GameMode.LevelSelect) {
      // Turn off all lights
      for (int r = 0; r < numRows; r++) {
        for (int c = 0; c < numCols; c++) {
          tiles[r, c].SetState(false);
        }
      }

      // Turn on selected light
      tiles[row, col].SetState(true);
    }
  }



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
        tiles[row, col].Init(this, row, col);
      }
    }
  }

}


