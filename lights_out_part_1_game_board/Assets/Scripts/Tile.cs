using UnityEngine;

public class Tile : MonoBehaviour {
  [SerializeField] private SpriteRenderer spriteRenderer;

  [Header("Tile properties")]
  [SerializeField] private Color offColour;
  [SerializeField] private Color onColour;

  private bool isOn = false;

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
    Toggle();
  }

}

