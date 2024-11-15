using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadBattleScene()
    {
        SceneManager.LoadScene("BattleScene"); // 이름으로 씬 전환
    }

    public void LoadTilemapScene()
    {
        SceneManager.LoadScene("Tilemap"); // 이름으로 씬 전환
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex); // 인덱스로 씬 전환
    }
}
