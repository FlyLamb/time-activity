using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapturePreview : MonoBehaviour {
    public RenderTexture texture;
    private void Start() {
        
                RenderTexture rt = texture;

        RenderTexture.active = rt;
        Texture2D tex = new Texture2D(rt.width, rt.height, TextureFormat.RGBA32, false);
        tex.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0);
        RenderTexture.active = null;

        byte[] bytes;
        
        bytes = tex.EncodeToPNG();
        
        string path = Application.dataPath + "/texture.png";
        System.IO.File.WriteAllBytes(path, bytes);
        Debug.Log("Saved to " + path);
    }
}