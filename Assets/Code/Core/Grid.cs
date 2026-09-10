
using System.Collections.Generic;
using UnityEngine;

namespace NMGrid.Grid
{
    public class Grid
    {
        private int _width;
        private int _height;

        private int _nextTileID;

        public Grid(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public Board CreateBoard()
        {
            Tile[,] tiles = new Tile[_width, _height];
            Board  board = new Board(tiles, 0, 0);
            
            AddRandomTile(board);
            AddRandomTile(board);

            return board;
        }

        public void AddRandomTile(Board board)
        {
            List<Vector2Int> emptyCells = new List<Vector2Int>();

            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _height; y++)
                {
                    if (board.Tiles[x, y] == null)
                    {
                        emptyCells.Add(new Vector2Int(x, y));
                    }
                }
            }

            if (emptyCells.Count == 0)
                return;
            
            Vector2Int pos = emptyCells[Random.Range(0, emptyCells.Count)];

            int value = Random.value < 0.9f ? 2 : 4;

            board.Tiles[pos.x, pos.y] = new Tile(_nextTileID++, value);
        }

        public bool Move(Board board, MoveDirection moveDirection)
        {
            bool moved = false;

            switch (moveDirection)
            {
                case MoveDirection.Left:
                    moved = MoveLeft(board);
                    break;
                case MoveDirection.Right:
                    moved = MoveRight(board);
                    break;
                case MoveDirection.Up:
                    moved = MoveUp(board);
                    break;
                case MoveDirection.Down:
                    moved = MoveDown(board);
                    break;
            }

            if (moved)
                board.AddMoves();

            return moved;
        }

        private bool MoveLeft(Board board)
        {
            bool moved = false;

            for (int y = 0; y < _height; y++)
            {
                List<Tile> rows = GetRows(board, y);
                List<Tile> mergedTiles = MergeTiles(rows);

                for (int x = 0; x < _width; x++)
                {
                    Tile newTile = x < mergedTiles.Count ? mergedTiles[x] : null;

                    if (board.Tiles[x, y] != newTile)
                    {
                        moved = true;
                    }

                    board.Tiles[x, y] = newTile;
                }
            }

            return moved;
        }
        
        private bool MoveRight(Board board)
        {
            bool moved = false;

            for (int y = 0; y < _height; y++)
            {
                List<Tile> rows = GetRows(board, y);
                rows.Reverse();
                List<Tile> mergedTiles = MergeTiles(rows);

                for (int x = 0; x < _width; x++)
                {
                    Tile newTile = x >= _width - mergedTiles.Count
                        ? mergedTiles[x - (_width - mergedTiles.Count)]
                        : null;

                    if (board.Tiles[x, y] != newTile)
                    {
                        moved = true;
                    }

                    board.Tiles[x, y] = newTile;
                }
            }

            return moved;
        }
        
        private bool MoveUp(Board board)
        {
            bool moved = false;

            for (int x = 0; x < _width; x++)
            {
                List<Tile> columns = GetColumns(board, x);
                List<Tile> mergedTiles = MergeTiles(columns);

                for (int y = 0; y < _height; y++)
                {
                    Tile newTile = y < mergedTiles.Count ? mergedTiles[y] : null;

                    if (board.Tiles[y, y] != newTile)
                    {
                        moved = true;
                    }

                    board.Tiles[x, y] = newTile;
                }
            }

            return moved;
        }
        
        private bool MoveDown(Board board)
        {
            bool moved = false;

            for (int x = 0; x < _width; x++)
            {
                List<Tile> columns = GetColumns(board, x);
                columns.Reverse();
                List<Tile> mergedTiles = MergeTiles(columns);

                for (int y = 0; y < _height; y++)
                {
                    Tile newTile = y >= _height - mergedTiles.Count
                        ? mergedTiles[y - (_height - mergedTiles.Count)]
                        : null;

                    if (board.Tiles[y, y] != newTile)
                    {
                        moved = true;
                    }

                    board.Tiles[x, y] = newTile;
                }
            }

            return moved;
        }

        private List<Tile> GetRows(Board board, int y)
        {
            List<Tile> rows = new ();

            for (int x = 0; x < _width; x++)
            {
                if (board.Tiles[x, y] != null)
                {
                    rows.Add(board.Tiles[x, y]);
                }
            }

            return rows;
        }

        private List<Tile> GetColumns(Board board, int x)
        {
            List<Tile> columns = new ();

            for (int y = 0; y < _height; y++)
            {
                if (board.Tiles[x, y] != null)
                {
                    columns.Add(board.Tiles[x, y]);
                }
            }
            
            return columns;
        }

        private List<Tile> MergeTiles(List<Tile> tiles)
        {
            List<Tile> result = new();

            for (int i = 0; i < tiles.Count; i++)
            {
                Tile tile = tiles[i];

                if (i + 1 < tiles.Count && tile.Value == tiles[i + 1].Value)
                {
                    Tile nextTile = tiles[i + 1];

                    tile.SetValue(tile.Value * 2);
                    result.Add(tile);
                    i++;
                }
                else
                {
                    result.Add(tile);
                }
            }

            return result;
        }
    }
}