

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

            return new Board(tiles, 0, 0);
        }
    }
}