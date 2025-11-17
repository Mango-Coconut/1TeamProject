using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CharacterChoicePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        if (rect == null)
        {
            Debug.LogError("CharacterSelectButton: RectTransform이 필요합니다.", this);
            return;
        }

        originalAnchoredPos = rect.anchoredPosition;
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

        // 캐릭터 선택 저장
        SelectedCharacter.Current = characterId;

        // 필요하면 여기서 "선택 연출" 후 LoadScene을 코루틴으로 딜레이해도 됨
        SceneManager.LoadScene(stageSceneName);
    }
}
