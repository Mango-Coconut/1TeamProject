using UnityEngine;

public class HPBar : MonoBehaviour
{
    [SerializeField] HP targetHP;
    [SerializeField] ProgressBar progressBar;

    void OnEnable()
    {
        if (targetHP != null)
        {
            targetHP.OnHPChanged += HandleHPChanged;
        }

        // 초기 값 반영
        if (targetHP != null && progressBar != null)
        {
            progressBar.SetValue(targetHP.CurrentHP, targetHP.MaxHP);
        }
    }

    void OnDisable()
    {
        if (targetHP != null)
        {
            targetHP.OnHPChanged -= HandleHPChanged;
        }
    }

    void HandleHPChanged(float current, float max)
    {
        if (progressBar == null)
        {
            return;
        }

        progressBar.SetValue(current, max);
    }
}
