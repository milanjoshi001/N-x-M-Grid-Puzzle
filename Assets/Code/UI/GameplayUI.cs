using System;
using NMGrid.Gameplay;
using TMPro;
using UnityEngine;

namespace NMGrid.UI
{
    public class GameplayUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _movesText;
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private GameObject _gameOverPanel;
        public static GameplayUI Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }
        
        public void UpdateMoves(int value) =>  _movesText.SetText(value.ToString()); 
        public void UpdateScore(int value) =>  _scoreText.SetText(value.ToString());
        
        public void GameOver(bool value) => _gameOverPanel.SetActive(value);

        public void UndoLastAction() => Controller.Instance.Undo();

        public void RestartGame()
        {
            _gameOverPanel.SetActive(false);
            Controller.Instance.Restart();
        }
    }
}