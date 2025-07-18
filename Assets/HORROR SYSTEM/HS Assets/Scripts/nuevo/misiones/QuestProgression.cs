using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestProgression : MonoBehaviour
{
    private bool moved = false;
    private bool openedInventory = false;
    private bool threwStone = false;

    void Update()
    {
        if (!moved && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)))
        {
            moved = true;
            QuestDatabase.Instance.CompleteQuest("conocer_controles");
        }

        if (!openedInventory && Input.GetKeyDown(KeyCode.I))
        {
            openedInventory = true;
            QuestDatabase.Instance.CompleteQuest("abrir_inventario");
        }

        if (!threwStone && Input.GetKeyDown(KeyCode.G))
        {
            threwStone = true;
            QuestDatabase.Instance.CompleteQuest("lanzar_piedra");
        }
    }
}
