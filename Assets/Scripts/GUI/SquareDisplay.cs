using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.GUI
{
    public class SquareDisplay : MonoBehaviour
    {
        public TextMeshProUGUI Square;
        public static SquareDisplay Instance;

        private const string SQUARE_CAPTION = "Object square: ";
        
        private void Awake()
        {
            Instance = this;
        }

        private void OnEnable()
        {
            Square.text = SQUARE_CAPTION;
        }

        public void UpdateSquare(Vector3[] vertices)
        {
            if (vertices == null)
                throw new ArgumentNullException(nameof(vertices));
            if (vertices.Length < 3)
                throw new ArgumentOutOfRangeException(nameof(vertices));
            
            var square = Geometric.GetSquare(vertices);
            if (square <= 0) 
                throw new Exception("Square calculating error!");
            
            Square.text = SQUARE_CAPTION + square;
        }
    }
}