using TMPro;
using UnityEngine;

namespace CandyCrush.Scripts
{
    public class ScoreHUD : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreText;

        [SerializeField] private TextMeshProUGUI movesText;
        // Start is called before the first frame update

        public void SetScoreText(int score)
        {
            scoreText.text = $"Score: {score}";
        }

        public void SetMovesText(int moves)
        {
            movesText.text = $"Moves: {moves}";
        }
    
    }
}
