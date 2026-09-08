using NMGrid.Grid;
using UnityEngine;

namespace NMGrid.Gameplay
{
    public class GridVisual : MonoBehaviour
    {
        [SerializeField] private TileVisual _tile;
        [SerializeField] private Transform _tileTransform;
        [SerializeField] private Vector2 _tileSize;

        public void Render(Board board)
        {
            var width = board.Tiles.GetLength(0);
            var height = board.Tiles.GetLength(1);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var tile = board.Tiles[x, y];

                    var tileVisual = Instantiate(_tile, _tileTransform);
                    tileVisual.Initialize(tile);

                    tileVisual.transform.localPosition = new Vector3(x * _tileSize.x, y * _tileSize.x, 0);
                }
            }
        }
    }
}