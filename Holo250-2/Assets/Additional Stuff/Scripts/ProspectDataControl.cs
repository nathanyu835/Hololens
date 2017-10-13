using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProspectDataControl : MonoBehaviour
{

    public ProspectControl.ImmersedAvatarPathInfo[] LevelPaths;

    // Use this for initialization
    void Awake()
    {
        ProspectControl levelControl = ProspectControl.Instance;
        if (levelControl == null || LevelPaths.Length != levelControl.AvatarStuff.Length)
        {
            Debug.Log("Mismatch between # of paths and # of expected paths");
            return;
        }

        for (int index = 0; index < LevelPaths.Length; index++)
        {
            levelControl.AvatarStuff[index] = LevelPaths[index];
        }
    }
}
