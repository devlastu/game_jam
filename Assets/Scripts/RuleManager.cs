using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleManager : MonoBehaviour {

    public static RuleManager Instance { get; private set; }

    private Dictionary<RuleCategory, List<Rule>> _rulesByCategory = new();
    private Dictionary<RuleCategory, Rule> _activeRules = new();
    private List<RuleCategory> _swappableCategories;

    public float swapInterval = 10f;
    public float warningDuration = 3f;

    public IReadOnlyDictionary<RuleCategory, Rule> ActiveRules => _activeRules;
    
    public event Action<RuleCategory, Rule> OnRuleChanged;

    
    public event Action<RuleCategory, Rule> OnRuleChangingWarning;

    void Awake() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
        } else {
            Instance = this;
        }
    }

    void Start() {
        AddRule(new Rule("Follow", RuleCategory.Camera,
            () => CameraController.Instance.SetCameraModeWithTransition(CameraMode.FollowBehind),
            () => {}));

        AddRule(new Rule("TopDown", RuleCategory.Camera,
            () => CameraController.Instance.SetCameraModeWithTransition(CameraMode.TopDown),
            () => {}));

        AddRule(new Rule("SideView", RuleCategory.Camera,
            () => CameraController.Instance.SetCameraModeWithTransition(CameraMode.SideView),
            () => {}));

        AddRule(new Rule("Normal", RuleCategory.Controls,
            () => PlayerMovement.Instance.SetNormalControls(),
            () => {}));

        AddRule(new Rule("Inverted", RuleCategory.Controls,
            () => PlayerMovement.Instance.SetInvertedControls(),
            () => {}));

        AddRule(new Rule("Clear", RuleCategory.Visibility,
            () => EnvironmentManager.Instance.DisableFog(),
            () => {}));

        AddRule(new Rule("Foggy", RuleCategory.Visibility,
            () => EnvironmentManager.Instance.EnableFog(0.06f),
            () => EnvironmentManager.Instance.DisableFog()));

        _swappableCategories = new List<RuleCategory>();
        foreach (RuleCategory cat in Enum.GetValues(typeof(RuleCategory))) {
            if (cat != RuleCategory.Camera) {
                _swappableCategories.Add(cat);
            }
        }

        ActivateInitialRules();

        StartCoroutine(SwapRoutine());
    }

    public void AddRule(Rule rule) {
        if (!_rulesByCategory.ContainsKey(rule.category)) {
            _rulesByCategory[rule.category] = new List<Rule>();
        }
        _rulesByCategory[rule.category].Add(rule);
    }

    private void ActivateRandomRule(RuleCategory category, bool firstTime = false) {
        var rules = _rulesByCategory[category];
        if (rules.Count == 0) return;

        Rule newRule;
        do {
            newRule = rules[UnityEngine.Random.Range(0, rules.Count)];
        } while (!firstTime && rules.Count > 1 && _activeRules.ContainsKey(category) && _activeRules[category] == newRule);

        if (!firstTime) {
            _activeRules[category]?.OnDeactivate?.Invoke();
        }

        _activeRules[category] = newRule;
        newRule.OnActivate?.Invoke();

        OnRuleChanged?.Invoke(category, newRule);
    }

    public Rule GetRule(RuleCategory category){
        if (_activeRules.TryGetValue(category, out var rule)){
            return rule;
        }
        return null;
    }

    private void ActivateInitialRules() {
        foreach (var category in Enum.GetValues(typeof(RuleCategory))) {
            var cat = (RuleCategory)category;
            if (_rulesByCategory.ContainsKey(cat) && _rulesByCategory[cat].Count > 0) {
                var firstRule = _rulesByCategory[cat][0];
                _activeRules[cat] = firstRule;
                firstRule.OnActivate?.Invoke();
                OnRuleChanged?.Invoke(cat, firstRule);
            }
        }
    }

    private IEnumerator SwapRoutine() {
        while (true) {
            yield return new WaitForSeconds(swapInterval);

            if (GameManager.Instance.State != GameState.Playing) yield break;
            if (_swappableCategories.Count == 0) yield break;

            var categoryToSwap = _swappableCategories[UnityEngine.Random.Range(0, _swappableCategories.Count)];
            var oldRule = _activeRules[categoryToSwap];
            
            var rules = _rulesByCategory[categoryToSwap];
            Rule newRule;
            do {
                newRule = rules[UnityEngine.Random.Range(0, rules.Count)];
            } while (rules.Count > 1 && _activeRules.ContainsKey(categoryToSwap) && _activeRules[categoryToSwap] == newRule);
            
            Debug.Log("Event invoked");
            OnRuleChangingWarning?.Invoke(categoryToSwap, newRule);

            
            yield return new WaitForSeconds(warningDuration);

            
            if (!GameManager.Instance.IsGameOver()){
                _activeRules[categoryToSwap]?.OnDeactivate?.Invoke();

                _activeRules[categoryToSwap] = newRule;
                newRule.OnActivate?.Invoke();
                OnRuleChanged?.Invoke(categoryToSwap, newRule);
            }
        }
    }
}
