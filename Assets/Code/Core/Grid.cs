
using UnityEngine;

namespace NMGrid.Grid
{
    public class Grid
    {
        private int _width;
        private int _height;

        public Grid(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public Board CreateBoard()
        {
            var tiles = new Tile[_width, _height];
            int id = 0;

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    tiles[x, y] = new Tile(id, 0);
                    id++;
                }
            }
            
            AddTile(tiles);
            AddTile(tiles);

            return new Board(tiles, 0, 0);
        }

        private void AddTile(Tile[,] tiles)
        {
            int width = tiles.GetLength(0);
            int height = tiles.GetLength(1);

            int x;
            int y;

            do
            {
                x = Random.Range(0, width);
                y = Random.Range(0, height);
            } 
            while (tiles[x, y].Value != 0);
            
            tiles[x,y].SetValue(2);
        }
    }
}