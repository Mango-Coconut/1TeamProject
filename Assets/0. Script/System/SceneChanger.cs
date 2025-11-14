using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager를 사용하려면 이 네임스페이스를 추가해야 합니다.

public class SceneChanger : MonoBehaviour
{
    // 버튼 클릭 또는 다른 이벤트에서 이 메서드를 호출하여 씬을 전환할 수 있습니다.
    public void LoadSceneByName(string sceneName)
    {
        // 씬 이름을 통해 씬을 로드
        SceneManager.LoadScene(sceneName);
    }

    // 씬의 인덱스를 통해 씬을 로드
    public void LoadSceneByIndex(int sceneIndex)
    {
        // 씬 인덱스를 통해 씬을 로드
        SceneManager.LoadScene(sceneIndex);
    }
}
