using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeData : MonoBehaviour
{
    public static int arraySize;
    public static int[] turretIDs;
    public static int[] upgradeIDs;
    public Node[] nodes;

    void Start()
    {
        arraySize = transform.childCount;

        turretIDs = new int[arraySize];
        upgradeIDs = new int[arraySize];
        nodes = new Node[arraySize];
        nodes = GetComponentsInChildren<Node>();
    }

    public static void SetIDs(int turretID, int upgradeID, int index)
    {
        turretIDs[index] = turretID;
        upgradeIDs[index] = upgradeID;
    }
    public static void ResetIDs(int index)
    {
        turretIDs[index] = 0;
        upgradeIDs[index] = 0;
    }

    public void UpdateNodeData()
    {
        for (int i = 0; i < arraySize; i++)
        {
            if (nodes[i].turretBlueprint != null)
            {
                turretIDs[i] = nodes[i].turretBlueprint.turretID;
                upgradeIDs[i] = nodes[i].upgradeID;
            }
            else
            {
                Debug.Log("Node " + i + " Has no turrets");
                turretIDs[i] = 0;
                upgradeIDs[i] = 0;
            }
        }
    }

    public void InitializeNodeID()
    {
        for (int i = 0; i < arraySize; i++)
        {
            nodes[i].nodeID = i;
        }
    }
}
