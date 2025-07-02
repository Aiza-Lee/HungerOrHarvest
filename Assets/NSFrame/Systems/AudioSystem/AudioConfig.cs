using System.Collections.Generic;
using UnityEngine;

namespace NSFrame {
	[CreateAssetMenu(fileName = "AudioConfig", menuName = "NSFrame/AudioConfig")]
	public class AudioConfig : ConfigBase {
		public GameObject SFXAudioSourcePrefab;
		[HideInInspector] public float _globalVolume;
		[HideInInspector] public float _bgmVolume;
		[HideInInspector] public float _sfxVolume;
		[HideInInspector] public bool _muteBGM;
		[HideInInspector] public bool _muteSFX;

		public List<NSPair<string, AudioClip>> BGMAudioClips;
		public List<NSPair<string, AudioClip>> SFXAuidoClips;
	}
}