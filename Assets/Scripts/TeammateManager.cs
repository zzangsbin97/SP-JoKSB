using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TeammateManager : MonoBehaviour
{
    public static TeammateManager Instance { get; private set; }
    public List<Teammate> teammates = new List<Teammate>();



    public void AddTeammate(Teammate teammate)
    {
        if (!teammates.Contains(teammate))
        {
            teammates.Add(teammate);
            Debug.Log(teammate.teammateName + "이(가) 팀에 추가되었습니다.");
        }
        else
        {
            Debug.Log("현재 팀에 추가되어있지 않음");
        }

    }



    // Start is called before the first frame update
    void Start()
    {
        Teammate kimSubin = new GameObject("kimsubin").AddComponent<Teammate>();
        kimSubin.teammateName = "kimsubin";
        AddTeammate(kimSubin);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 유지
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
