using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageUI : MonoBehaviour
{
    [SerializeField] TMP_Text characterName;

    [SerializeField] GameObject escpanel;
    [SerializeField] GameObject settingPanel;

    void Awake()
    {
        HideEscPanel();
        HideSettingPanel();
        characterName.text = SelectedCharacter.CurCharacter.ToString();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //우선 설정창 켜져있으면 닫기
            if (settingPanel.activeSelf)
            {
                HideSettingPanel();
                return;
            }

            //아니면 일시정지 / 풀기
            if (!PauseManager.IsPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    public void Pause(){
        ShowEscPanel();
        PauseManager.Pause();
    }
    public void Resume(){
        HideEscPanel();
        PauseManager.Resume();
    }
    public void ShowEscPanel()
    {
        escpanel.gameObject.SetActive(true);
    }

    public void HideEscPanel()
    {
        escpanel.gameObject.SetActive(false);
        HideSettingPanel();
    }

    public void ShowSettingPanel()
    {
        settingPanel.gameObject.SetActive(true);
    }

    public void HideSettingPanel()
    {
        settingPanel.gameObject.SetActive(false);
    }

    //버튼 연결 이벤트
    public void GoToCharacterChoiceScene()
    {
        SelectedCharacter.CurCharacter = CharacterId.None;
        SceneManager.LoadScene("CharacterChoice");
    }
}
