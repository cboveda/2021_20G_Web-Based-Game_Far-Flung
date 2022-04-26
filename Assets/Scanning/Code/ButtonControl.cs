using UnityEngine.UI;
using Scanning;

public class ButtonControl : DialogAction {

    public Button start_scanning;

    public override bool DialogActionStartDialog() {
        return true;
    }

    public override void DialogActionDoWhenFinished() {
        start_scanning.interactable = true;
    }
}
