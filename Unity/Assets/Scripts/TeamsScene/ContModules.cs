using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContModules : MonoBehaviour {

	private int contModules;
	private int[] modules;

	// Use this for initialization
	void Start () {
		contModules = 0;
		modules = new int[] {0,0,0,0};
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int getContModules(){
		return contModules;
	}

	public void incrementContModules(){
		contModules++;
	}

	public void initContModules(){
		contModules = 0;
	}

	public bool hasMaxModules(){
		return contModules >= 4;
	}

	public void setModules(int[] newModules){
		modules = newModules;
	}

	public int[] getModules(){
		return modules;
	}
		

}
