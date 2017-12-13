using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateHolder : MonoBehaviour
{
    private enum ESTADO_JUEGO{
        PLAYING,PAUSE,INVENTARY,MENU
    }

    //private const int PLAYING = 0;
    //private const int PAUSE = 1;
   // private const int MENU = 2;

    private ESTADO_JUEGO state;

    // Use this for initialization
    void Start()
    {
        this.state = ESTADO_JUEGO.PLAYING;
    }

    public bool isPlaying()
    {
        return this.state == ESTADO_JUEGO.PLAYING;
    }

    public bool isPause()
    {
        return this.state == ESTADO_JUEGO.PAUSE;
    }

    public bool isMenu()
    {
        return this.state == ESTADO_JUEGO.MENU;
    }

    public bool isInventary(){
        return this.state == ESTADO_JUEGO.INVENTARY;
    }

    public bool setPlaying()
    {
        switch (this.state)
        {
            case (ESTADO_JUEGO.MENU):
            case (ESTADO_JUEGO.PAUSE):
            case (ESTADO_JUEGO.INVENTARY):
                this.state = ESTADO_JUEGO.PLAYING;
                return true;
            default:
                return false;
        }
    }

    public bool setInventary(){
        switch (this.state)
        {
            case (ESTADO_JUEGO.PLAYING):
            case (ESTADO_JUEGO.PAUSE):
                this.state = ESTADO_JUEGO.INVENTARY;
                return true;
            default:
                return false;
        }
    }

    public bool setMenu()
    {
        switch (this.state)
        {
            case (ESTADO_JUEGO.PAUSE):
            case (ESTADO_JUEGO.PLAYING):
                this.state = ESTADO_JUEGO.MENU;
                return true;
            default:
                return false;
        }
    }

    public bool setPause()
    {
        switch (this.state)
        {
            case (ESTADO_JUEGO.INVENTARY):
            case ESTADO_JUEGO.PLAYING:
                this.state = ESTADO_JUEGO.PAUSE;
                return true;
            default:
                return false;
        }
    }
}
