using UnityEngine;
using TMPro; 


public class RulesDisplay : MonoBehaviour
{
    // public TextMeshProUGUI  rule1Text;
    public TextMeshProUGUI  rule2Text;
    public TextMeshProUGUI  rule3Text;

    void OnEnable()
    {
        if (RuleManager.Instance != null)
        {
            RuleManager.Instance.OnRuleChanged += OnRuleChanged;
        }
    }

    void OnDisable()
    {
        if (RuleManager.Instance != null)
        {
            RuleManager.Instance.OnRuleChanged -= OnRuleChanged;
        }
    }

    private void Start() {
        UpdateInitialRules();
    }

    private void UpdateInitialRules() {
        if (RuleManager.Instance != null) {
            // SetRuleText(rule1Text, RuleCategory.Camera, RuleManager.Instance.GetRule(RuleCategory.Camera));
            SetRuleText(rule2Text, RuleCategory.Controls, RuleManager.Instance.GetRule(RuleCategory.Controls));
            SetRuleText(rule3Text, RuleCategory.Visibility, RuleManager.Instance.GetRule(RuleCategory.Visibility));
        }
    }
    private void OnRuleChanged(RuleCategory category, Rule rule) {
        switch (category)
        {
            // case RuleCategory.Camera:
            //     SetRuleText(rule1Text, category, rule);
            //     break;
            case RuleCategory.Controls:
                SetRuleText(rule2Text, category, rule);
                break;
            case RuleCategory.Visibility:
                SetRuleText(rule3Text, category, rule);
                break;
        }
    }
    
    private void SetRuleText(TMPro.TMP_Text targetText, RuleCategory category, Rule rule) {
        if (targetText != null)
            targetText.text = $"{rule.ruleName}";
    }
}