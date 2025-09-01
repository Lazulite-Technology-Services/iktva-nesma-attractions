using DG.Tweening;
using DG.Tweening.Plugins.Core.PathCore;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using Path = System.IO.Path;

public class VideoHandler : MonoBehaviour
{
    public static VideoHandler instance;

    [SerializeField] private VideoPlayer player;

    private string idleVideoPath = string.Empty;

    [SerializeField] private RawImage videoImage;

    [SerializeField] private RenderTexture videoTexture;

    [SerializeField] private float tweenSpeed = 0.25f;

    [SerializeField] private List<VideoPathHolder> pathList = new List<VideoPathHolder>();

    private void Awake()
    {
        instance = this;
        

        LoadPath();
        

    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    //This method will play the idle video
    private IEnumerator PlayIdleVideo(string pathname)
    {
        string path = FindVideoThePath(pathname);

        yield return new WaitForSeconds(0.5f);

        StartFadeOut(path);
    }

    private string FindVideoThePath(string buttonname)
    {
        var item = pathList.FirstOrDefault(x => x.name == buttonname);

        if (item == null)
        {
            Debug.LogError($"No video folder found with the name '{buttonname}'");
            return null;
        }
        else
        {
            Debug.Log("nothing found");
        }

        Debug.Log($"path is : {item.path}");
        string[] videoFiles = Directory.GetFiles(item.path, "*.*")
                                           .Where(s => s.EndsWith(".mp4") || s.EndsWith(".mov") || s.EndsWith(".avi"))
                                           .ToArray();

        //string path = Path.Combine(Application.streamingAssetsPath, videoFiles[0]);
        string path = videoFiles[0];
        
        return path;
    }

    public IEnumerator PlayVideo(string buttonname)
    {
        string path = FindVideoThePath(buttonname);

        yield return new WaitForSeconds(0.25f);

        StartFadeOut(path);
    }

    private void StartFadeOut(string path)
    {
        videoImage.DOColor(new Color(1, 1, 1, 0), tweenSpeed).OnComplete(() => StartFadeIn(path));
    }

    private void StartFadeIn(string path)
    {
        player.Stop();
        videoTexture.Release();
        player.url = path;
        player.Play();
        videoImage.DOColor(new Color(1, 1, 1, 1), tweenSpeed);
    }

    private void LoadPath()
    {
        string path = Application.streamingAssetsPath;

        Debug.Log("code is outside");

        if (Directory.Exists(path))
        {
            Debug.Log("code is inside");

            string[] folders = Directory.GetDirectories(path);

            foreach (string folder in folders)
            {
                string folderName = System.IO.Path.GetFileName(folder);

                VideoPathHolder holder = new VideoPathHolder();
                holder.name = folderName;
                holder.path = System.IO.Path.Combine(path, folderName);
                pathList.Add(holder);

                //Debug.Log("Folder: " + folderName);
            }

            StartCoroutine(PlayIdleVideo("home"));
        }
        else
        {
            Debug.LogWarning("StreamingAssets folder does not exist!");
        }

    }
}

[System.Serializable]
public class VideoPathHolder
{
    public string name;
    public string path;
}
