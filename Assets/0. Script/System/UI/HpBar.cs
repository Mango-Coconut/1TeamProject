using UnityEngine;

public class HPBar : MonoBehaviour
{
    // HP 수동 지정시 자동 바인드. 런타임 지정시 반드시 수동 Bind 하기
    [SerializeField] HP targetHP;
    ProgressBar progressBar;

    // Monster등 움직이는 객체 따라 다니게
    public RectTransform rect;
    public Transform anchor;

    void Awake() 
    {
        progressBar = GetComponent<ProgressBar>();
        if(targetHP != null) Bind(targetHP);
    }


    void OnDisable()
    {
        Clear();
    }

    public void Bind(HP hp)
    {
        targetHP = hp;
        if (targetHP == null) return;

        //이벤트 구독
        targetHP.OnHPChanged += HandleHPChanged;
        //초기값 반영
        progressBar.SetValue(targetHP.CurrentHP, targetHP.MaxHP);
    }

    public void Bind(HP hp, Transform anchor)
    {
        Bind(hp);
        this.anchor = anchor;
    }

    public void Clear(){
        if (targetHP == null) return;

        targetHP.OnHPChanged -= HandleHPChanged;
        targetHP = null;
    }

    void HandleHPChanged(float current, float max)
    {
        if (progressBar == null) return;

        progressBar.SetValue(current, max);
    }
}
