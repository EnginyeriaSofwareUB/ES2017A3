using UnityEngine;

namespace Assets.Scripts.Environment
{

    public class Global
    {
        public static class Capas
        {
            // Capa que identifica los totems del primer jugador
            public const int totemsPrimerJugador = 8;
            // Capa que identifica los totems del segundo jugador
            public const int totemsSegundoJugador = 9;
            // Capa que identifica una bala lanzada por el primer jugador
            public const int balaPrimerJugador = 10;
            // Capa que identifica una bala lanzada por el segundo jugador
            public const int balaSegundoJugador = 11;
        }

        public static bool IsTerrainGenerated { get; set; }

        public static Vector3 TerrainNullPoint { get; set; }

        public static Vector3 TerrainEndPoint { get; set; }
    }
}
