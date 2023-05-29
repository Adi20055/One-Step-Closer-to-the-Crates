using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeData : MonoBehaviour
{
    public static int arraySize;
    public static int[] turretIDs;
    public static int[] upgradeIDs;
    private Node[] node;

    void Start()
    {
        arraySize = transform.childCount;

        turretIDs = new int[arraySize];
        upgradeIDs = new int[arraySize];
        node = new Node[arraySize];
        node = GetComponentsInChildren<Node>();

        for (int i = 0; i < arraySize; i++)
        {
            node[i].nodeID = i;

            if (node[i].turretBlueprint != null)
            {
                turretIDs[i] = node[i].turretBlueprint.turretID;
                upgradeIDs[i] = node[i].turretBlueprint.upgradeID;
            }
            else
            {
                turretIDs[i] = -1;
                upgradeIDs[i] = 0;
            }
        }
    }

    public static void SetIDs(int turretID, int upgradeID, int index)
    {
        turretIDs[index] = turretID;
        upgradeIDs[index] = upgradeID;
    }
    public static void ResetIDs(int index)
    {
        turretIDs[index] = -1;
        upgradeIDs[index] = 0;
    }
}
