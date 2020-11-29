using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SoundManager
{

	static GameObject _oneShotGameObject;
	static AudioSource _oneShotAudioSource;

	public static float MusicIntensity = 1;
	public static float SoundIntensity = 1;


	public enum Sound
	{
		Walk,
		Woosh,
		Kill,
		Spawn,
		MenuMove,
		MenuOk,
		WordTyping,
		Dead,
		Pickup,
		MusicMenu,
		MusicFight,
		MusicAmbient1,
		MusicAmbient2

	}

	public static void PlayLoop(string name, Sound sound, float volume = 1f, bool isMusic = false)
	{
		GameObject loop = new GameObject(name);
		AudioSource sourceLoop = loop.AddComponent<AudioSource>();

		float volumeOptions;

		if (isMusic)
		{
			volumeOptions = MusicIntensity;
		}
		else
		{
			volumeOptions = SoundIntensity;
		}

		sourceLoop.loop = true;
		sourceLoop.volume = volume * volumeOptions;
		sourceLoop.clip = GetAudioClip(sound);

		SoundAssets.i.AudioLoops.Add(loop);

		sourceLoop.Play();
	}


	public static void PlaySound(Sound sound, float volume = 1f,bool isMusic = false) //not Spacialized
	{
		if (_oneShotGameObject == null)
		{
			_oneShotGameObject = new GameObject("OneShotSound");
			_oneShotAudioSource = _oneShotGameObject.AddComponent<AudioSource>();
		}

		_oneShotAudioSource.bypassEffects = false;

		float volumeOptions;

		if(isMusic)
		{
			volumeOptions = MusicIntensity;
		}
		else
		{
			volumeOptions = SoundIntensity;
		}

		_oneShotAudioSource.PlayOneShot(GetAudioClip(sound), volume * volumeOptions);
	}

	public static AudioClip GetAudioClip(Sound sound)
	{
		List<AudioClip> audioClips = new List<AudioClip>();
		foreach(SoundAssets.SoundAudioClip soundAudioClip in SoundAssets.i.soundAudioClips)
		{
			if(soundAudioClip.sound == sound)
			{
				audioClips.Add(soundAudioClip.audioClip);
			}
		}
		return audioClips[Random.Range(0,audioClips.Count)];
	}

}
