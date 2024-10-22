using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Cube.UI.Components
{
    public class TopScoreItem : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _lp, _userName, _score;
        [SerializeField]
        private Color _oddNumberColor, _evenNumberColor;
        [SerializeField]
        private Image _image;

        public void Set(int lp, string userName, int score)
        {
            _lp.text = lp + ".";
            _userName.text = userName;
            _score.text = score.ToString();

            _image.color = lp % 2 > 0 ? _oddNumberColor : _evenNumberColor;
        }
    }
}
