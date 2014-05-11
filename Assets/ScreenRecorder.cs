using UnityEngine;
using System.Collections;
using System.IO;

public class ScreenRecorder : MonoBehaviour
{
	public int framerate = 30;
	public int superSize;
	public bool autoRecord;
	
	int frameCount;
	bool recording;
	
	void Start ()
	{
		if (autoRecord) StartRecording ();
	}
	
	void StartRecording ()
	{
		System.IO.Directory.CreateDirectory ("Capture");
		Time.captureFramerate = framerate;
		frameCount = -1;
		recording = true;
	}

	IEnumerator Capture (string filePath) {
		yield return new WaitForEndOfFrame();

		int width = Screen.width;
		int height = Screen.height;
		Texture2D tex = new Texture2D (width, height, TextureFormat.ARGB32, false);
		// Read screen contents into the texture
		tex.ReadPixels (new Rect(0, 0, width, height), 0, 0);
		tex.Apply ();
		
		// Encode texture into PNG
		byte[] bytes = tex.EncodeToPNG();
		Destroy (tex);
		
		// For testing purposes, also write to a file in the project folder
		File.WriteAllBytes(filePath, bytes);
	}

	void Update ()
	{
		if (recording)
		{
			if (Input.GetMouseButtonDown (0))
			{
				recording = false;
				enabled = false;
			}
			else
			{
				if (frameCount > 0)
				{
					var name = "Capture/frame" + frameCount.ToString ("0000") + ".png";
					StartCoroutine(Capture(name));
				}
				
				frameCount++;
				
				if (frameCount > 0 && frameCount % 60 == 0)
				{
					Debug.Log ((frameCount / 60).ToString() + " seconds elapsed.");
				}
			}
		}
	}
	
	void OnGUI ()
	{
		if (!recording && GUI.Button (new Rect (0, 0, 200, 50), "Start Recording"))
		{
			StartRecording ();
			Debug.Log ("Click Game View to stop recording.");
		}
	}
}