using System.Collections;
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
    new() { (0, 0), (1, 0), (0, 2), (1, 2), (0, 4), (1, 4) }, // 3
    new() { (0, 0), (1, 0), (3, 0), (4, 0), (0, 2), (1, 2), (3, 2), (4, 2), (0, 4), (1, 4), (3, 4), (4, 4) }, // 6
    new() { (4, 2), (4, 4), (2, 0), (2, 4), (1, 0), (1, 2), (0, 3) }, // 6
    new() { (4, 2), (3, 0), (3, 1), (3, 3), (2, 2), (2, 3), (1, 2), (1, 3), (1, 4), (0, 1) }, // 6
    new() { (4, 0), (4, 3), (4, 4), (3, 1), (3, 3), (2, 2), (1, 1), (1, 4), (0, 0) }, // 7
    
    new() { (4, 1), (4, 3), (4, 4), (3, 3), (2, 1), (2, 4), (1, 4), (0, 0), (0, 1), (0, 2), (0, 3) }, // 7
    new() { (4, 4), (3, 0), (3, 2), (2, 0), (2, 1), (2, 4), (1, 0), (0, 0), (0, 2), (0, 3) }, // 7
    new() { (4, 1), (4, 3), (3, 0), (3, 1), (3, 3), (2, 0), (2, 3), (1, 2), (1, 4), (0, 1), (0, 4) }, // 8
    new() { (4, 4), (3, 0), (3, 3), (3, 4), (2, 1), (2, 2), (1, 1), (1, 3), (0, 0), (0, 3) }, // 8
    new() { (4, 3), (4, 4), (3, 1), (3, 3), (3, 4), (2, 3), (1, 2), (1, 4), (0, 0), (0, 2) }, // 8

    new() { (4, 0), (4, 1), (3, 0), (3, 2), (3, 4), (2, 2), (1, 0), (1, 4) }, // 9 
    new() { (4, 0), (4, 1), (4, 2), (3, 3), (2, 2), (2, 3), (2, 4), (1, 1), (1, 4), (0, 0), (0, 3) }, // 9
    new() { (3, 1), (3, 2), (3, 4), (2, 0), (2, 1), (2, 3), (1, 1), (0, 0), (0, 3), (0, 4) }, // 9
    new() { (4, 2), (4, 3), (3, 0), (3, 2), (3, 4), (2, 0), (0, 0), (0, 1), (0, 3), (0, 4) }, // 10
    new() { (4, 0), (4, 1), (4, 2), (4, 4), (3, 0), (3, 3), (2, 0), (2, 2), (1, 3), (0, 0), (0, 1), (0, 3), (0, 4) }, // 10

    new() { (4, 4), (3, 0), (2, 4), (1, 0), (1, 2) }, // 11
    new() { (3, 0), (3, 1), (3, 3), (3, 4), (2, 0), (2, 1), (2, 3), (1, 0), (1, 2), (1, 4), (0, 2), (0, 3) }, // 11
    new() { (4, 0), (3, 2), (3, 4), (2, 3), (1, 1), (0, 1), (0, 4) }, // 12
    new() { (4, 3), (3, 1), (3, 2), (3, 4), (2, 2), (2, 3), (1, 0), (1, 3), (0, 3), (0, 4) }, // 12
    new() { (4, 3), (3, 0), (3, 1), (3, 2), (2, 1), (2, 3), (0, 0), (0, 3), (0, 4) }, // 12

    new() { (4, 0), (4, 2), (4, 3), (3, 2), (3, 3), (3, 4), (2, 1), (2, 4), (1, 0), (1, 2), (1, 3), (0, 0), (0, 4) }, // 13
    new() { (4, 0), (4, 1), (3, 1), (3, 3), (2, 1), (1, 0), (1, 2), (0, 4) }, // 14
    new() { (4, 0), (4, 1), (4, 3), (2, 2), (2, 4), (1, 1), (1, 3), (0, 0), (0, 2), (0, 4) }, // 14
    new() { (4, 2), (3, 3), (1, 1), (0, 3) }, // 15
    new() { (4, 2), (4, 4), (3, 2), (2, 0), (2, 4), (1, 2), (0, 0), (0, 2) }, // 15
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

      // Check for the win condition
      bool lightsOn = false;
      for (int r = 0; r < numRows; r++) {
        for (int c = 0; c < numCols; c++) {
          lightsOn |= tiles[r, c].IsLightOn();
        }
      }
      if (!lightsOn) {
        gameMode = GameMode.None;
        StartCoroutine(ShowWinAnimation());
      }

    } else if (gameMode == GameMode.LevelSelect) {
      // Turn off all lights
      for (int r = 0; r < numRows; r++) {
        for (int c = 0; c < numCols; c++) {
          tiles[r, c].SetState(false);
        }
      }

      // Turn on selected light
      tiles[row, col].SetState(true);

      // Set the level
      level = ((4 - row) * 5) + col;
    }
  }

  private IEnumerator ShowWinAnimation() {
    // Light pattern to show on win (smiley face!)
    List<(int, int)> winLights = new() {
      (3, 1), (3, 3), (1, 0), (1, 4), (0, 1), (0, 2), (0, 3)
    };

    // Flash the smiley face three times.
    for (int i = 0; i < 3; i++) {
      foreach ((int row, int col) in winLights) {
        tiles[row, col].SetState(true);
      }
      yield return new WaitForSeconds(1f);
      // Turn off all lights
      for (int row = 0; row < numRows; row++) {
        for (int col = 0; col < numCols; col++) {
          tiles[row, col].SetState(false);
        }
      }
      yield return new WaitForSeconds(1f);
    }

    // End by returning back to level select mode.
    gameMode = GameMode.LevelSelect;
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


