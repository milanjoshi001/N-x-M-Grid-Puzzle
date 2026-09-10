using NMGrid.Grid;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NMGrid.Gameplay
{
    public class TileVisual : MonoBehaviour
    {
        [SerializeField] private Image _innerTileVisual;
        [SerializeField] private TextMeshProUGUI _tileValueText;
        
        public int TileId {get; private set;}

        public void Initialize(Tile tile)
        {
            if (tile == null) return;
            
            TileId = tile.ID;
            
            
            _innerTileVisual.gameObject.SetActive(true);
            _tileValueText.SetText($"{tile?.Value}");
            
            UpdateVisuals(tile.Value);
        }

        private void UpdateVisuals(int value)
        {
            switch (value)
            {
                case 2:
                    _innerTileVisual.color = new Color32(238, 228, 218, 255);
                    _tileValueText.color = new Color32(119, 110, 101, 255);
                    break;

                case 4:
                    _innerTileVisual.color = new Color32(237, 224, 200, 255);
                    _tileValueText.color = new Color32(119, 110, 101, 255);
                    break;

                case 8:
                    _innerTileVisual.color = new Color32(242, 177, 121, 255);
                    _tileValueText.color = Color.white;
                    break;

                case 16:
                    _innerTileVisual.color = new Color32(245, 149, 99, 255);
                    _tileValueText.color = Color.white;
                    break;

                case 32:
                    _innerTileVisual.color = new Color32(246, 124, 95, 255);
                    _tileValueText.color = Color.white;
                    break;

                case 64:
                    _innerTileVisual.color = new Color32(246, 94, 59, 255);
                    _tileValueText.color = Color.white;
                    break;

                default:
                    _innerTileVisual.color = new Color32(237, 207, 114, 255);
                    _tileValueText.color = Color.white;
                    break;
            }
        }
    }
}