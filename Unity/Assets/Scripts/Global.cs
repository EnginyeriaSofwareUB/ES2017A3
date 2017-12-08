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


        public static string FIRST_PLAYER = "FirstPlayer";
        public static string SECOND_PLAYER = "SecondPlayer";
        public static string TOTEM_FIRST_PLAYER = "TotemFirstPlayer";
        public static string TOTEM_SECOND_PLAYER = "TotemSecondPlayer";
        public static string TOTEM_FIRST_PLAYER_MODULE = "TotemFirstPlayerModule";
        public static string TOTEM_SECOND_PLAYER_MODULE = "TotemSecondPlayerModule";
        public static string WEAPON = "Weapon";

        public static class TIPO_OBJETOS
        {
            public const int objetoVitamina = 1;
            public const int objetoBotiquin = 2;
            public const int objetoCohete =3;
            public const int objetoTeletransporte = 4;
            public const int objetoRayo = 5;
            public const int objetoIglu = 6;
            public const int objetoEscudoSimple = 7;
            public const int objetoAngel = 8;
            public const int objetoEscudoDoble = 9;

        }

        public static class MAX_RONDA_ITEM
        {
            public const int ANGEL = 1;
            public const int IGLU = 5;
            public const int ESCUT = 5;
            public const int ESCUTDOBLE = 5;
        }

        public static class MAX_USO_ITEM
        {
            public const int ANGEL = 1;
            public const int IGLU = 1;
            public const int ESCUT = 3;
            public const int ESCUTDOBLE = 2;
        }
        public static bool IsTerrainGenerated { get; set; }

        public static Vector3 TerrainNullPoint { get; set; }

        public static Vector3 TerrainEndPoint { get; set; }
    }
}
