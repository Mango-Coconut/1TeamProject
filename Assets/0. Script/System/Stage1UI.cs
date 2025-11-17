using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Stage1UI : MonoBehaviour
{
    [SerializeField] TMP_Text characterName;

    void Awake()
    {
        characterName.text =SelectedCharacter.CurCharacter.ToString();
    }
        //버튼 연결 이벤트
    public void GoToCharacterChoiceScene()
    {
        SelectedCharacter.CurCharacter = CharacterId.None;
        SceneManager.LoadScene("CharacterChoice");
    }
}
