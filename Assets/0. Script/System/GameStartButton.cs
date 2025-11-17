using UnityEngine;
using UnityEngine.SceneManagement; // SceneManager를 사용하려면 이 네임스페이스를 추가해야 합니다.

public class GameStartButton : MonoBehaviour
{
    public void ToPlayerChoiceScene()
    {
        SceneManager.LoadScene("CharacterChoice");
        //SceneManager.LoadScene(sceneIndex); 인덱스 통해서도 가능
    }
}
