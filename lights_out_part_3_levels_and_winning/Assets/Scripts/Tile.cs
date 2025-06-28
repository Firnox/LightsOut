using UnityEngine;

public class Tile : MonoBehaviour {
  [SerializeField] private SpriteRenderer spriteRenderer;

  [Header("Tile properties")]
  [SerializeField] private Color offColour;
  [SerializeField] private Color onColour;

  private bool isOn = false;

  private GameManager gameManager;
  private int row;
  private int col;

  public void Init(GameManager gameManager, int row, int col) {
    this.gameManager = gameManager;
    this.row = row;
    this.col = col;
  }

  // Set the colour based on the state of the tile.
  void UpdateDisplayedState() {
    spriteRenderer.color = isOn ? onColour : offColour;
  }

  // Start is called once before the first execution of Update after the MonoBehaviour is created
  void Start() {
    UpdateDisplayedState();
  }

  // Toggle the state between off and on.
  public void Toggle() {
    isOn = !isOn;
    UpdateDisplayedState();
  }

  // Set the state to a specific value of off or on.
  public void SetState(bool isOn) {
    this.isOn = isOn;
    UpdateDisplayedState();
  }
  public bool IsLightOn() { return isOn; }

  // Called when user clicks on the tile.
  private void OnMouseDown() {
    gameManager.Toggle(row, col);
  }

}

