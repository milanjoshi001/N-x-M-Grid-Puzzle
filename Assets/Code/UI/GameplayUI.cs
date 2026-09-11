using System;
using NMGrid.Gameplay;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace NMGrid.UI
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _movesText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private Button _undoButton;
        [SerializeField] private Button _restartButton;
        
        public static GameplayUI Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            _undoButton.onClick.AddListener(UndoLastAction);
            _restartButton.onClick.AddListener(RestartGame);
        }
        
        public void UpdateMoves(int value) =>  _movesText.SetText(value.ToString()); 
        public void UpdateScore(int value) =>  _scoreText.SetText(value.ToString());

        private void UndoLastAction() => Controller.Instance.Undo();
        
        private void RestartGame() => Controller.Instance.Restart();
    }
}