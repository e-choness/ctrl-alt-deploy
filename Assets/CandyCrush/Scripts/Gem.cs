using UnityEngine;

namespace CandyCrush.Scripts
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class Gem : MonoBehaviour
    {
        public GemType type;

        public void SetType(GemType type)
        {
            this.type = type;
            GetComponent<SpriteRenderer>().sprite = type.Sprite;
        }

        public GemType GemType() => type;
    }
}