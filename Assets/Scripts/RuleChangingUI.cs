using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RuleChangingUI : MonoBehaviour
{
    public TextMeshProUGUI ruleText;
    public TextMeshProUGUI timerText;

    private float _countdownDuration = 3f;
    private float _countdownTimer = 0f;
    private bool _isCountingDown = false;

    
    private void Awake() {
        if (RuleManager.Instance != null) {
            RuleManager.Instance.OnRuleChangingWarning += OnRuleChangedWarning;
        }
    }

    private void OnDestroy() {
        if (RuleManager.Instance != null) {
            RuleManager.Instance.OnRuleChangingWarning -= OnRuleChangedWarning;
        }
    }
    

    private void OnRuleChangedWarning(RuleCategory category, Rule newRule)
    {
        
        ruleText.text = $"{category}: {newRule.ruleName}";

        
        StopAllCoroutines();
        StartCoroutine(CountdownCoroutine());
    }

    private IEnumerator CountdownCoroutine()
    {
        _countdownTimer = _countdownDuration;
        _isCountingDown = true;

        while (_countdownTimer > 0)
        {
            timerText.text = Mathf.Ceil(_countdownTimer).ToString();
            yield return null;
            _countdownTimer -= Time.deltaTime;
        }
        
        timerText.text = "";
        ruleText.text = "";

        _isCountingDown = false;
    }
}