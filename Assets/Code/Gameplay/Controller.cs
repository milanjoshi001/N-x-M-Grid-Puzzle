using System;
using NMGrid.Grid;
using NMGrid.UI;
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
        
        public static Controller Instance { get; private set; }
        
        private void Awake()
        {
            if (Instance == null)
                Instance = this;
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
            GameplayUI.Instance.UpdateMoves(_board.Moves);
            GameplayUI.Instance.UpdateScore(_board.Score);
        }

        public void Undo()
        {
            if (_prevBoard == null)
                return;
            
            _board = _prevBoard;
            _prevBoard = null;
            _gridVisual.Render(_board);
        }

        public void Restart()
        {
            _gridVisual.ClearBoard();
            _grid.ClearBoard();
            _board = _grid.CreateBoard();
            _gridVisual.Render(_board);
        }
    }
}