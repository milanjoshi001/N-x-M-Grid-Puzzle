using System;
using NMGrid.Grid;
using UnityEngine;

namespace NMGrid.Gameplay
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] private GridVisual _gridVisual;
        [SerializeField] private Input _input;

        private Grid.Grid _grid;
        private Board _board;
        private Board _prevBoard;
        
        private void Awake()
        {
            _grid = new Grid.Grid(4, 4);
            _board = _grid.CreateBoard();
        }

        private void Start()
        {
            _gridVisual.CreateBoardVisuals(_board.Tiles.GetLength(0), _board.Tiles.GetLength(1));
            _gridVisual.Render(_board);
        }

        private void OnEnable()
        {
            _input.OnMove += HandleMove;
        }

        private void HandleMove(MoveDirection direction)
        {
            Board prevBoard = _board.CloneBoard();
            bool moved = _grid.Move(_board, direction);
            
            if(!moved)
                return;
            
            _prevBoard = prevBoard;
            _grid.AddRandomTile(_board);
            _gridVisual.Render(_board);
        }

        public void Undo()
        {
            if (_prevBoard == null)
                return;
            
            _board = _prevBoard;
            _prevBoard = null;
            _gridVisual.Render(_board);
        }
    }
}