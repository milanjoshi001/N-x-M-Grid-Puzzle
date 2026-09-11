using NMGrid.Grid;
using UnityEngine;

namespace NMGrid.Gameplay
{
    public class GridVisual : MonoBehaviour
    {
        [SerializeField] private TileVisual _tile;
        [SerializeField] private Transform _tileTransform;
        
        [SerializeField] private GameObject _cellPrefab;
        [SerializeField] private Transform _cellTransform;
        
        [SerializeField] private Vector2 _tileSize;

        public void CreateBoardVisuals(int width, int height)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var tile = Instantiate(_cellPrefab, _cellTransform);
                    tile.transform.localPosition = new Vector3(x * _tileSize.x, y * _tileSize.y, 0);
                }
            }
        }
        
        public void Render(Board board)
        {
            foreach (Transform child in _tileTransform)
            {
                child.gameObject.SetActive(false);
                Destroy(child.gameObject);
            }
            
            var width = board.Tiles.GetLength(0);
            var height = board.Tiles.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var tile = board.Tiles[x, y];

                    if (tile == null) continue;

                    var tileVisual = Instantiate(_tile, _tileTransform);
                    tileVisual.Initialize(tile);

                    tileVisual.transform.localPosition = new Vector3(x * _tileSize.x, y * _tileSize.x, 0);
                }
            }
        }

        public void ClearBoard()
        {
            foreach (Transform child in _tileTransform)
            {
                Destroy(child.gameObject);
            }
        }
    }
}