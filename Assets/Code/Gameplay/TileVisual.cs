using NMGrid.Grid;
using TMPro;
using UnityEngine;

namespace NMGrid.Gameplay
{
    public class TileVisual : MonoBehaviour
    {
        [SerializeField] private Transform _innerTileVisual;
        [SerializeField] private TextMeshProUGUI _tileValueText;
        
        public int TileId {get; private set;}

        public void Initialize(Tile tile)
        {
            TileId = tile.ID;
            bool isEmpty = tile.Value == 0;
            
            _innerTileVisual.gameObject.SetActive(!isEmpty);
            
            if(!isEmpty)
                _tileValueText.SetText($"{tile.Value}");
        }
    }
}