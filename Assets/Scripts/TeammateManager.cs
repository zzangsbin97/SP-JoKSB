using System.Collections.Generic;
using UnityEngine;

public class TeammateManager : MonoBehaviour
{
    public static TeammateManager Instance { get; private set; }
    public List<Teammate> teammates = new List<Teammate>(); // ���� ���� �ִ� ���� ���

    void Start()
    {
        // ���ο� Teammate ����
        GameObject teammateObject = new GameObject("�����");
        Teammate Teammate = teammateObject.AddComponent<Teammate>();

        // Teammate �ʱ�ȭ
        Teammate.InitializeTeammate("�����");
        DontDestroyOnLoad(Teammate);

        // �� ��Ͽ� �߰�
        AddTeammate(Teammate);

        

        teammateObject = new GameObject("������");
        Teammate = teammateObject.AddComponent<Teammate>();
        Teammate.InitializeTeammate("������");
        AddTeammate(Teammate);
        DontDestroyOnLoad(Teammate);
      
        
        teammateObject = new GameObject("������");
        Teammate = teammateObject.AddComponent<Teammate>();
        Teammate.InitializeTeammate("������");
        AddTeammate(Teammate);
        DontDestroyOnLoad(Teammate);
        
        /*
        teammateObject = new GameObject("�����");
        Teammate = teammateObject.AddComponent<Teammate>();
        Teammate.InitializeTeammate("�����");
        AddTeammate(Teammate);
        DontDestroyOnLoad(Teammate);

        teammateObject = new GameObject("�ֵ��� & ����");
        Teammate = teammateObject.AddComponent<Teammate>();
        Teammate.InitializeTeammate("�ֵ��� & ����");
        AddTeammate(Teammate);
        */
        
        DontDestroyOnLoad(Teammate);

    }
    public void UpdateTeammates(List<Teammate> updatedTeammates)
    {
        if (updatedTeammates == null || updatedTeammates.Count == 0)
        {
            Debug.LogWarning("TeammateManager: ���޵� ������ ��� �ֽ��ϴ�.");
            return;
        }

        teammates.Clear();
        teammates.AddRange(updatedTeammates);

        Debug.Log($"TeammateManager: {updatedTeammates.Count}���� ���� �����͸� ������Ʈ�߽��ϴ�.");
    }

    void Update()
     {
            

     }
     void Awake()
     {


        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }


    }
    public void AddTeammate(Teammate teammate)
    {
        // �̹� ���� �߰��� �������� Ȯ��
        if (teammates.Exists(t => t.teammateName == teammate.teammateName))
        {
            Debug.LogWarning($"{teammate.teammateName}��(��) �̹� ���� �߰��Ǿ� �ֽ��ϴ�.");
            return;
        }

        // �� ��Ͽ� �߰�
        teammates.Add(teammate);

        Debug.Log($"{teammate.teammateName}��(��) ���� �߰��Ǿ����ϴ�.");
    }
}
    

