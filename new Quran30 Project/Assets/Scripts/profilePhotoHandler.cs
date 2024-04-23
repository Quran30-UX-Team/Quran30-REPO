using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class profilePhotoHandler : MonoBehaviour
{
    public Image imageDisplay;
    private string savedImagePathKey = "SavedImagePath";

    public void ImportPhoto()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                // Load the selected image and display it
                Texture2D texture = NativeGallery.LoadImageAtPath(path, -1);
                if (texture != null)
                {
                    imageDisplay.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);

                    // Save the image path to PlayerPrefs
                    PlayerPrefs.SetString(savedImagePathKey, path);
                    PlayerPrefs.Save();

                    Debug.Log("Image path saved: " + path);
                }
            }
        }, "Select a photo", "image/*");

        Debug.Log("Permission result: " + permission);
    }

    private void Start()
    {
        // Load the saved image path when the game starts
        string savedImagePath = PlayerPrefs.GetString(savedImagePathKey);
        if (!string.IsNullOrEmpty(savedImagePath))
        {
            Texture2D texture = NativeGallery.LoadImageAtPath(savedImagePath, -1);
            if (texture != null)
            {
                imageDisplay.sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
            }
        }
    }
}
