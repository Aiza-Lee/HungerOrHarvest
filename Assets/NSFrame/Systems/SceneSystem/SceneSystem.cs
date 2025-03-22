using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

// note: 在小项目中，通过名称加载场景完全没有问题，所以这里只需要做异步加载的进度监听，加载过渡就可以了
namespace NSFrame {

	using static MonoServiceTool;

	public static class SceneSystem {

		private static bool _isTransitioning = false;

		private static SceneConfig _config;
		private static TransitionImage _transitionImage;

		static SceneSystem() {
			_config = NSFrameRoot.Inst.GetConfig<SceneConfig>();
			GameObject imageGO = GameObject.Instantiate(_config.TransitionImage);
			imageGO.name = _config.TransitionImage.name;
			_transitionImage = imageGO.GetComponent<TransitionImage>();
		}

		/// <summary>
		/// 加载进度触发事件,传入的参数就是当前进度 "LoadSceneProcess_f"
		/// </summary>
		public static void LoadSceneAsync(string sceneName, UnityAction callBack = null, bool useDefaultTransition = false, float transitiontime = 1.0f) {
			if (_isTransitioning) return;
			NS_StartCoroutine(DoLoadSceneAsync(sceneName, callBack, useDefaultTransition, transitiontime));
		}
		static IEnumerator DoLoadSceneAsync(string sceneName, UnityAction callBack, bool useDefaultTransition, float transitiontime) {
			if (useDefaultTransition) 
				yield return NS_StartCoroutine(PlayTransitionAnimation(true, transitiontime));

			AsyncOperation loadOp = SceneManager.LoadSceneAsync(sceneName);
			loadOp.allowSceneActivation = false;
			while (!loadOp.isDone) {
				EventSystem.Invoke<float>((int)NSFrameEvent.LoadSceneProcess_f, loadOp.progress, eventType: EventType.NSFrame);
				if (loadOp.progress >= 0.9f) 
					loadOp.allowSceneActivation = true;
				yield return null;
			}
			EventSystem.Invoke<float>((int)NSFrameEvent.LoadSceneProcess_f, 1, eventType: EventType.NSFrame);
			callBack?.Invoke();

			if (useDefaultTransition)
				NS_StartCoroutine(PlayTransitionAnimation(false, transitiontime));
		}
		static IEnumerator PlayTransitionAnimation(bool fadeOut, float duration) {
			_isTransitioning = true;
			if (fadeOut) UISystem.Show(_transitionImage);

			Color startColor = fadeOut ? Color.clear : Color.black;
			Color endColor = fadeOut ? Color.black : Color.clear;
			_transitionImage.Image.color = startColor;

			float elapsedTime = 0f;
			while (elapsedTime < duration) {
				_transitionImage.Image.color = Color.Lerp(startColor, endColor, elapsedTime / duration);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
			_transitionImage.Image.color = endColor;

			_isTransitioning = false;
			if (!fadeOut) UISystem.Close(_transitionImage);
		}

		public static void InitTest() {}

	}
}