using UnityEngine;

namespace CandyCrush.Scripts
{
    public class GameManager : MonoBehaviour
    {
        private ScoreHUD _scoreHUD;

        private int _score = 0;

        private int _moves = 0;
        // Start is called before the first frame update
        void Start()
        {
            InitializeScoreHud();
        }

        private void InitializeScoreHud()
        {
            _scoreHUD = FindAnyObjectByType<ScoreHUD>();
            _scoreHUD.SetScoreText(_score);
            _scoreHUD.SetMovesText(_moves);
        }
    
    }
}
