using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

namespace cakeslice
{
    public class OutlineAnimation : MonoBehaviour
    {
        bool totemPrimerJugador = false;
        bool totemContrincante = false;

        // Use this for initialization
        void Start()
        {

        }

        private void outlineTotemPrimerJugador()
        {
            Color colorPrimerCanal = GetComponent<OutlineEffect>().lineColor0;
            if (totemPrimerJugador)
            {
                colorPrimerCanal.a += Time.deltaTime;

                if (colorPrimerCanal.a >= 1)
                    totemPrimerJugador = false;
            }
            else
            {
                colorPrimerCanal.a -= Time.deltaTime;

                if (colorPrimerCanal.a <= 0)
                    totemPrimerJugador = true;
            }

            colorPrimerCanal.a = Mathf.Clamp01(colorPrimerCanal.a);
            GetComponent<OutlineEffect>().lineColor0 = colorPrimerCanal;
            GetComponent<OutlineEffect>().UpdateMaterialsPublicProperties();
        }


        private void outlineTotemContrincante()
        {
            Color colorSegundoCanal = GetComponent<OutlineEffect>().lineColor1;
            if (totemContrincante)
            {
                colorSegundoCanal.a += Time.deltaTime;

                if (colorSegundoCanal.a >= 1)
                    totemContrincante = false;
            }
            else
            {
                colorSegundoCanal.a -= Time.deltaTime;

                if (colorSegundoCanal.a <= 0)
                    totemContrincante = true;
            }

            colorSegundoCanal.a = Mathf.Clamp01(colorSegundoCanal.a);
            GetComponent<OutlineEffect>().lineColor1 = colorSegundoCanal;
            GetComponent<OutlineEffect>().UpdateMaterialsPublicProperties();
        }


        // Update is called once per frame
        void Update()
        {
            outlineTotemPrimerJugador();
            outlineTotemContrincante();
        }
    }
}