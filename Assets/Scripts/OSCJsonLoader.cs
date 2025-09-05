using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class OSCJsonLoader : MonoBehaviour
{
    public static OSCJsonLoader Instance;

    [Header("Editable in Inspector")]
    public List<OSCEntry> entries = new List<OSCEntry>();

    // Dictionary for fast lookup at runtime
    public Dictionary<string, List<string>> oscMap;

    [Header("Optional JSON file (StreamingAssets)")]
    public string jsonFileName = "commands.json";

    void Awake()
    {
        Instance = this;    
        // Load JSON if available
        LoadFromJSON();

        // Build the dictionary from the list
        BuildDictionary();
    }

    void LoadFromJSON()
    {
        string path = Path.Combine(Application.persistentDataPath,jsonFileName);
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            OSCFileData data = JsonUtility.FromJson<OSCFileData>(json);

            entries.Clear();
            entries.AddRange(data.fileData);

            Debug.Log("Loaded " + entries.Count + " OSC entries from JSON.");
        }
        else
        {
            Debug.Log($"JSON file not found, using Inspector list instead.{path}");
        }
    }

    void BuildDictionary()
    {
        oscMap = new Dictionary<string, List<string>>();

        foreach (var entry in entries)
        {
            if (!oscMap.ContainsKey(entry.Name))
                oscMap[entry.Name] = new List<string>();

            oscMap[entry.Name].Add(entry.oscCmd);
        }

        Debug.Log("OSC dictionary built with " + oscMap.Count + " unique names.");
    }

    /// <summary>
    /// Send all OSC commands for a given Name
    /// </summary>
    public void SendOSCCommandByName(string name)
    {
        if (oscMap.ContainsKey(name))
        {
            foreach (var cmd in oscMap[name])
            {
                OscMessage msg = new OscMessage();
                msg.address = cmd;
                OSC.INSTANCE.Send(msg);

                Debug.Log("Sent OSC: " + cmd);
            }
        }
        else
        {
            Debug.LogWarning("No OSC command found for: " + name);
        }
    }
}

[System.Serializable]
public class OSCEntry
{
    public string Name;
    public string oscCmd;
}

[System.Serializable]
public class OSCFileData
{
    public OSCEntry[] fileData;
}
