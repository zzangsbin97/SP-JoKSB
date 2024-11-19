using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeammateDialogueManager : MonoBehaviour {
    
    public Text talkScript;
    public GameObject talkingTeammate;

    public void Talk(GameObject talkingTeammate) {
        this.talkingTeammate = talkingTeammate;
        talkScript.text = "말하는 동료의 이름은 " + talkingTeammate.name;
    }



}
