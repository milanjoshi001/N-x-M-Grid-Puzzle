using System;
using NMGrid.Grid;
using UnityEngine;

namespace NMGrid.Gameplay
{
    public class Controller : MonoBehaviour
    {
        [SerializeField] GridVisual _gridVisual;

        private Grid.Grid _grid;
        private Board _board;
        
        private void Awake()
        {
            _grid = new Grid.Grid(4, 4);
            _board = _grid.CreateBoard();
        }

        private void Start()
        {
            _gridVisual.Render(_board);
        }
    }
}