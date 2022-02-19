using UnityEngine;
using DialogMaker;

namespace Scanning {

    public class DialogController : MonoBehaviour {

        [SerializeField]
        public GameObject _dialogGeneratorPrefab;
        private GameObject _dialogGenerator;
        private DialogGenerator _dg;

        [SerializeField]
        private DialogScriptableObject[] scanning_scripts;
        private int script_index;

        public DialogAction scene_script;

        private bool start_once;

        void Start() {

            // set up params
            script_index = 0;
            start_once = true;

            // setup dialog generator
            _dialogGenerator = Object.Instantiate(_dialogGeneratorPrefab, this.transform);
            _dg = _dialogGenerator.GetComponent<DialogGenerator>();
            _dg.dialogContainer = scanning_scripts[script_index];
        }

        void Update() {

            if ( start_once && scene_script.DialogActionStartDialog() ) {
                start_once = false;
                _dg.BeginPlayingDialog();
            }

            if (_dg.AllDialogComplete()) {
                scene_script.DialogActionDoWhenFinished();
            }
        }
    }

    public abstract class DialogAction : MonoBehaviour { // abstract rather than interface as unity inspector does not reliable display interfaces

        public abstract bool DialogActionStartDialog(); // Check the status of the scene

        public abstract void DialogActionDoWhenFinished(); // when done playing, notify the scene
    }
}