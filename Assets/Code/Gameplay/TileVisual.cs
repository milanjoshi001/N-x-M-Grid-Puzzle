using System.Collections.Generic;
using NMGrid.Grid;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NMGrid.Gameplay
{
    public class TileVisual : MonoBehaviour
    {
        [SerializeField] private Image _innerTileVisual;
        //[SerializeField] private TextMeshProUGUI _tileValueText;
        [SerializeField] private List<Sprite> _sprites;
        
        public int TileId {get; private set;}

        public void Initialize(Tile tile)
        {
            if (tile == null) return;
            
            TileId = tile.ID;
            
            
            _innerTileVisual.gameObject.SetActive(true);
            //_tileValueText.SetText($"{tile?.Value}");
            
            UpdateVisuals(tile.Value);
        }

        private void UpdateVisuals(int value)
        {
            switch (value)
            {
                case 2:
                    //_innerTileVisual.color = new Color32(238, 228, 218, 255);
                    //_tileValueText.color = new Color32(119, 110, 101, 255);
                    _innerTileVisual.sprite = _sprites[0];
                    break;

                case 4:
                    //_innerTileVisual.color = new Color32(237, 224, 200, 255);
                    //_tileValueText.color = new Color32(119, 110, 101, 255);
                    _innerTileVisual.sprite = _sprites[1];
                    break;

                case 8:
                    //_innerTileVisual.color = new Color32(242, 177, 121, 255);
                    //_tileValueText.color = Color.white;
                    _innerTileVisual.sprite = _sprites[2];
                    break;

                case 16:
                    //_innerTileVisual.color = new Color32(245, 149, 99, 255);
                    //_tileValueText.color = Color.white;
                    _innerTileVisual.sprite = _sprites[3];
                    break;

                case 32:
                    //_innerTileVisual.color = new Color32(246, 124, 95, 255);
                    //_tileValueText.color = Color.white;
                    _innerTileVisual.sprite = _sprites[4];
                    break;

                case 64:
                    //_innerTileVisual.color = new Color32(246, 94, 59, 255);
                    //_tileValueText.color = Color.white;
                    _innerTileVisual.sprite = _sprites[5];
                    break;
                
                case 128:
                    //_innerTileVisual.color = new Color32(246, 94, 59, 255);
                    //_tileValueText.color = Color.white;
                    _innerTileVisual.sprite = _sprites[6];
                    break;
                
                case 256:
                    //_innerTileVisual.color = new Color32(246, 94, 59, 255);
                    //_tileValueText.color = Color.white;
                    _innerTileVisual.sprite = _sprites[7];
                    break;
                
                case 512:
                    //_innerTileVisual.color = new Color32(246, 94, 59, 255);
                    //_tileValueText.color = Color.white;
                    _innerTileVisual.sprite = _sprites[8];
                    break;
                
                case 1024:
                    //_innerTileVisual.color = new Color32(246, 94, 59, 255);
                    //_tileValueText.color = Color.white;
                    _innerTileVisual.sprite = _sprites[9];
                    break;
                
                case 2048:
                    //_innerTileVisual.color = new Color32(246, 94, 59, 255);
                    //_tileValueText.color = Color.white;
                    _innerTileVisual.sprite = _sprites[10];
                    break;

                default:
                    //_innerTileVisual.color = new Color32(237, 207, 114, 255);
                    //_tileValueText.color = Color.white;
                    break;
            }
        }
    }
}