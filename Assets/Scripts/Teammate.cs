using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teammate : MonoBehaviour
{
    public string teammateName;
    public bool IsInMyTeam = false;
    private TeammateManager teammateManager;

    void OnTriggerEnter2D(Collider2D Other) {
        Debug.Log("일단충돌함...");
        Player player = Other.GetComponent<Player>();

        if (player != null) {
            Debug.Log(teammateName);
            IsInMyTeam = true;
            teammateManager.AddTeammate(this);
            }
        }
    
    // Start is called before the first frame update
    void Start()
    {
        teammateManager = FindObjectOfType<TeammateManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
