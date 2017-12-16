using System.Collections;
using UnityEngine;

public class SetPlay : MonoBehaviour {

    StateHolder stateHolder;
    void Start(){
        this.stateHolder = GameObject.FindGameObjectWithTag("GameController").GetComponent<StateHolder>();
        StartCoroutine(setPlay(0.25f));
    }

    private IEnumerator setPlay(float delay){
        yield return new WaitForSeconds(delay);
        this.stateHolder.setPlaying();
    }
}