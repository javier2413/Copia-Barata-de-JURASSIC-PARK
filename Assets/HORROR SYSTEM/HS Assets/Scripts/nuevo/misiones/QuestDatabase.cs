using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Collections.Generic;
using System.IO;

public class QuestDatabase : MonoBehaviour
{
    public static QuestDatabase Instance;
    public List<Quest> quests = new List<Quest>();
    private string savePath;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            savePath = Application.persistentDataPath + "/quest_save.json";
            LoadQuests();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void LoadQuests()
    {
        TextAsset jsonFile = Resources.Load<TextAsset>("quests");

        if (jsonFile == null)
        {
            Debug.LogError("No se encontró el archivo quests.json en Resources.");
            return;
        }

        Debug.Log("Archivo quests.json cargado.");

        QuestListWrapper wrapper = JsonUtility.FromJson<QuestListWrapper>(jsonFile.text);

        if (wrapper == null)
        {
            Debug.LogError("Fallo al deserializar el archivo quests.json (wrapper es null).");
            return;
        }

        if (wrapper.quests == null)
        {
            Debug.LogError(" El campo 'quests' dentro del wrapper es null.");
            return;
        }

        quests = wrapper.quests;
        Debug.Log("Se cargaron " + quests.Count + " misiones correctamente.");
    }

    public void SaveQuests()
    {
        QuestListWrapper wrapper = new QuestListWrapper { quests = quests };
        string json = JsonUtility.ToJson(wrapper, true);
        File.WriteAllText(savePath, json);
    }

    public Quest GetQuestById(string id)
    {
        return quests.Find(q => q.id == id);
    }

    public void CompleteQuest(string id)
    {
        Quest quest = GetQuestById(id);
        if (quest != null && !quest.isCompleted)
        {
            quest.isCompleted = true;
            Debug.Log($"Misión completada: {quest.title}");
            SaveQuests();
        }
    }

    public bool IsQuestCompleted(string id)
    {
        Quest quest = GetQuestById(id);
        return quest != null && quest.isCompleted;
    }
}
