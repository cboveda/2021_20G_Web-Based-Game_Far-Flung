using UnityEngine.UI;
using Scanning;

public class EndSceneControl : DialogAction {

    public Button return_to_hub;
    public Button restart_mission;

    public override bool DialogActionStartDialog() {
        return true;
    }

    public override void DialogActionDoWhenFinished() {

        return_to_hub.interactable = true;
        restart_mission.interactable = true;
    }
}
