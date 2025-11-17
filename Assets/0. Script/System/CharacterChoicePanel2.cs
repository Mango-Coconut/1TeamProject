using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CharacterChoicePanel2 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{


    [Header("캐릭터 ID")]
    public CharacterId characterId;

    [Header("이동 설정")]
    public float hoverMoveY = 20f;      // 얼마나 위로 올릴지 (anchoredPosition 기준)
    public float hoverDuration = 0.2f;  // 올라가는 시간
    public float backDuration = 0.15f;  // 내려가는 시간
    public Ease hoverEase = Ease.OutQuad;
    public Ease backEase = Ease.InQuad;

    [Header("씬 이름")]
    public string stageSceneName = "Stage1";

    RectTransform rect;
    Vector2 originalAnchoredPos;
    Tween moveTween;

    [SerializeField] Transform selectPanel;
    [SerializeField] TMP_Text characterNameText;
    

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        if (rect == null)
        {
            Debug.LogError("CharacterSelectButton: RectTransform이 필요합니다.", this);
            return;
        }

        originalAnchoredPos = rect.anchoredPosition;
        selectPanel.gameObject.SetActive(false);
    }

    void OnDisable()
    {
        KillTween();
        if (rect != null)
        {
            rect.anchoredPosition = originalAnchoredPos;
        }
    }

    void KillTween()
    {
        if (moveTween != null && moveTween.IsActive())
        {
            moveTween.Kill();
            moveTween = null;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (rect == null) return;

        KillTween();
        if(hoverMoveY == 0) return;

        Vector2 targetPos = originalAnchoredPos + new Vector2(0f, hoverMoveY);
        moveTween = rect.DOAnchorPos(targetPos, hoverDuration)
            .SetEase(hoverEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (rect == null) return;

        KillTween();

        moveTween = rect.DOAnchorPos(originalAnchoredPos, backDuration)
            .SetEase(backEase);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // 좌클릭만 받으려면 체크
        if (eventData.button != PointerEventData.InputButton.Left) return;

        ShowSelectPanel();
    }

    //버튼 연결 이벤트
    public void ShowSelectPanel()
    {
        SelectedCharacter.CurCharacter = characterId;
        characterNameText.text = SelectedCharacter.CurCharacter.ToString();

        selectPanel.gameObject.SetActive(true);
    }
    public void HideSelectPanel()
    {
        selectPanel.gameObject.SetActive(false);
    }
    public void OnStartButtonClick()
    {
        SceneManager.LoadScene(stageSceneName);
    }
}
