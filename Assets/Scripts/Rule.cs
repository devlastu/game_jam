using System;

public enum RuleCategory {
    Camera,
    Controls,
    Visibility
}

[Serializable]
public class Rule {
    public string ruleName;
    public RuleCategory category;

    public Action OnActivate;
    public Action OnDeactivate;

    public Rule(string name, RuleCategory cat, Action activate, Action deactivate) {
        ruleName = name;
        category = cat;
        OnActivate = activate;
        OnDeactivate = deactivate;
    }
}