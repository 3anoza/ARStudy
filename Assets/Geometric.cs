using UnityEngine;
using System;
namespace Assets
{
    public class Geometric {
        public static float CalculateSquare(Vector3 dot0, Vector3 dot1, Vector3 dot2, Vector3 dot3){
            var cosAB = Cos(dot0 / 2, dot1 / 2) * Cos(dot2 / 2, dot3 / 2) - Sin(dot0 / 2, dot1 / 2) * Sin(dot2 / 2, dot3 / 2);
            var a = VectorLength(dot0, dot1);
            var b = VectorLength(dot1, dot2);
            var c = VectorLength(dot2, dot3);
            var d = VectorLength(dot0, dot3);
            var p = (a + b + c + d) / 2;
            return Mathf.Sqrt((p - a) * (p - b) * (p - c) * (p - d) - ( a * b * c * d ) * Mathf.Pow(cosAB, 2));
        }
        public static float CalculateSquare(Vector3 dot0, Vector3 dot1, Vector3 dot2){
            return 0.5f * VectorLength(dot0, dot1) * VectorLength(dot1, dot2) * Sin(dot0, dot2);
        }

        public static float VectorLength(Vector3 dot0, Vector3 dot1){
            return Mathf.Sqrt( Mathf.Pow(dot1.x - dot0.x, 2) + Mathf.Pow(dot1.y - dot0.y, 2) + Mathf.Pow(dot1.z - dot0.z, 2) );
        }
        public static float VectorLength(Vector3 dot0){
            return Mathf.Sqrt( Mathf.Pow(dot0.x, 2) + Mathf.Pow(dot0.y, 2) + Mathf.Pow(dot0.z, 2) );
        }

        public static float Sin(Vector3 dot0, Vector3 dot1){
            return Mathf.Sqrt( 1 - Mathf.Pow(Cos(dot0, dot1), 2) );
        }

        public static float Cos(Vector3 dot0, Vector3 dot1){
            return ScalarProduct(dot0, dot1) / ( VectorLength(dot0) * VectorLength(dot1) );
        }

        public static float ScalarProduct(Vector3 dot0, Vector3 dot1){
            return ( dot0.x * dot1.x ) + ( dot0.y * dot1.y ) + ( dot0.z * dot1.z );
        }

    }
}