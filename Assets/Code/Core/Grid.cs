
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
                    moved = MoveHorizontal(board, false);
                    break;
                case MoveDirection.Right:
                    moved = MoveHorizontal(board, true);
                    break;
                case MoveDirection.Up:
                    moved = MoveVerticel(board, true);
                    break;
                case MoveDirection.Down:
                    moved = MoveVerticel(board, false);
                    break;
            }

            if (moved)
                board.AddMoves();

            return moved;
        }

        private bool MoveHorizontal(Board board, bool reverse)
        {
            bool boardChanged = false;

            for (int y = 0; y < _height; y++)
            {
                List<Tile> line = new List<Tile>();

                if (!reverse)
                {
                    for (int x = 0; x < _width; x++)
                    {
                        if(board.Tiles[x, y] != null)
                            line.Add(board.Tiles[x, y]);
                    }
                }
                else
                {
                    for (int x = _width - 1; x >= 0; x--)
                    {
                        if(board.Tiles[x, y] != null)
                            line.Add(board.Tiles[x, y]);
                    }
                }
                
                List<Tile>  mergedTiles = MergeTiles(board, line);

                for (int i = 0; i < _width; i++)
                {
                    Tile newTile = i < mergedTiles.Count ? mergedTiles[i] : null;
                    int x = reverse ? _width - 1 - i : i;

                    if (board.Tiles[x, y] != newTile)
                        boardChanged = true;

                    board.Tiles[x, y] = newTile;
                }
            }
            
            return boardChanged;
        }
        
        private bool MoveVerticel(Board board, bool reverse)
        {
            bool boardChanged = false;

            for (int x = 0; x < _width; x++)
            {
                List<Tile> line = new List<Tile>();

                if (reverse)
                {
                    for (int y = _height - 1; y >= 0; y--)
                    {
                        if(board.Tiles[x, y] != null)
                            line.Add(board.Tiles[x, y]);
                    }
                }
                else
                {
                    for (int y = 0; y < _height; y++)
                    {
                        if(board.Tiles[x, y] != null)
                            line.Add(board.Tiles[x, y]);
                    }
                }
                
                List<Tile>  mergedTiles = MergeTiles(board, line);

                for (int i = 0; i < _height; i++)
                {
                    Tile newTile = i < mergedTiles.Count ? mergedTiles[i] : null;
                    int y = reverse ? _height - 1 - i : i;

                    if (board.Tiles[x, y] != newTile)
                        boardChanged = true;

                    board.Tiles[x, y] = newTile;
                }
            }
            
            return boardChanged;
        }

        private List<Tile> MergeTiles(Board board, List<Tile> tiles)
        {
            List<Tile> result = new();

            for (int i = 0; i < tiles.Count; i++)
            {
                Tile tile = tiles[i];

                if (i + 1 < tiles.Count && tile.Value == tiles[i + 1].Value)
                {
                    Tile nextTile = tiles[i + 1];

                    tile.SetValue(tile.Value * 2);
                    board.AddScore(tile.Value);
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