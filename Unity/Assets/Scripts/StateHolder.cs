using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class StateHolder {

	private const int PLAYING = 0;
	private const int PAUSE = 1;
	private const int MENU = 2;

	public int state;

    private static StateHolder stateHolder;

    private StateHolder() { this.state = 2; }

    public static StateHolder Instance
    {
        get
        {
            if (stateHolder == null)
            {
                stateHolder = new StateHolder();
            }
            return stateHolder;
        }
    }

    // Use this for initialization
    void Start () {
		this.state = 2;
	}

	public bool isPlaying() {
		return this.state == PLAYING;
	}

	public bool isPause() {
		return this.state == PAUSE;
	}

	public bool isMenu() {
		return this.state == MENU;
	}

	public bool setPlaying() {
		switch (this.state) {
		case(PLAYING):
		case(MENU):
		case(PAUSE):
			this.state = PLAYING;
			return true;
		default:
			return false;
		}
	}

	public bool setMenu(){
		switch (this.state) {
		case(MENU):
		case(PAUSE):
			this.state = MENU;
			return true;
		default:
			return false;
		}
	}

	public bool setPause(){
		switch (this.state) {
		case(PAUSE):
		case(PLAYING):
			this.state = PAUSE;
			return true;
		default:
			return false;
		}
	}
}
