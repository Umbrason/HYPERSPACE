using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class Jukebox : MonoBehaviour
{
    [SerializeField] private AudioClip[] songs;
    [SerializeField] private TMP_Text songText;
    [SerializeField] private AudioSource songSource;
    private float ToastDuration = 5f;

    private Queue<AudioClip> playlist;
    void FixedUpdate()
    {
        songText.enabled = Time.fixedUnscaledTime - startTime < ToastDuration;
        if (!songSource.isPlaying)
            PlayNext();
    }


    private float transitionStartTime;
    public void ForceSong(AudioClip clip, float transitionDuration)
    {   
        
    }

    private void BuildPlaylist()
    {
        if (songs.Length == 0) return;
        while (playlist?.Count == 0 || playlist?.Peek() == lastSong)
            playlist = new(songs.OrderBy(x => Random.value).ToArray());

    }
    private AudioClip lastSong;
    private void PlayNext()
    {
        if ((playlist?.Count ?? 0) == 0)
            BuildPlaylist();
        if (playlist.Count == 0) return;
        PlayClip(playlist.Dequeue());
    }

    private const string NOW_PLAYING_FORMAT = "Now Playing: ♫ {0} ♫";
    private float startTime;
    private void PlayClip(AudioClip clip)
    {
        startTime = Time.fixedUnscaledTime;
        songSource.clip = playlist.Dequeue();
        songText.text = string.Format(NOW_PLAYING_FORMAT, clip.name);
        songSource.Play();
    }
}
