using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(Renderer))]
public class VideoTextureUpdater : MonoBehaviour
{
    public VideoPlayer videoPlayer;

    private Renderer videoRenderer;

    private void Start()
    {
        videoRenderer = GetComponent<Renderer>();
        videoRenderer.material.mainTexture = videoPlayer.texture;
        videoPlayer.Play();
    }
}