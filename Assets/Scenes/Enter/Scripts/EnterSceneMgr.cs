using NSFrame;
using UnityEngine;
using UnityEngine.Playables;

public class EnterSceneMgr : MonoBehaviour {
	private PlayableDirector _playableDirector;

	// Start is called before the first frame update
	void Start() {
		_playableDirector = GetComponent<PlayableDirector>();
		_playableDirector.stopped += OnPlayEnd;
		_playableDirector.Play();
	}

	// Update is called once per frame
	void Update() {

	}

	private void OnPlayEnd(PlayableDirector director) {
		SceneSystem.LoadSceneAsync("MainWorld", null, true);
	}
}
