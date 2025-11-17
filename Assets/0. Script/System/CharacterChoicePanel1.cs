using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CharacterChoicePanel1 : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{


    [Header("캐릭터 ID")]
    public CharacterId characterId;

    [Header("이동 설정")]
    public float hoverScale = 1.1f;      // 얼마나 위로 올릴지 (anchoredPosition 기준)
    public float hoverDuration = 0.2f;  // 올라가는 시간
    public float backDuration = 0.15f;  // 내려가는 시간
    public Ease hoverEase = Ease.OutQuad;
    public Ease backEase = Ease.InQuad;

    [Header("이동할 씬 이름")]
    public string stageSceneName = "Stage1";

    RectTransform rect;
    Vector2 originalScale;
    Tween moveTween;

    void Awake()
    {
        rect = GetComponent<RectTransform>();
        if (rect == null)
        {
            Debug.LogError("CharacterSelectButton: RectTransform이 필요합니다.", this);
            return;
        }

        originalScale = rect.localScale;
    }

    void OnDisable()
    {
        KillTween();
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
        if(hoverScale == 1) return;

        moveTween = rect.DOScale(hoverScale, hoverDuration)
            .SetEase(hoverEase);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (rect == null) return;

        KillTween();
        moveTween = rect.DOScale(1, backDuration)
        .SetEase(backEase);
        
    }

    //버튼 연결 이벤트
    public void OnClick()
    {
        SelectedCharacter.CurCharacter = characterId;
        SceneManager.LoadScene(stageSceneName);
    }
}
