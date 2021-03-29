using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HordeController : MonoBehaviour
{
    List<NPCBehaviour> m_currentNpcHorde;
    [SerializeField] NormalGuy normalGuyPrefabRef;
    [SerializeField] Doctor doctorPrefabRef;
    [SerializeField] GrenadeGuy grenadeGuyPrefabRef;
    [SerializeField] ShieldGuy shieldGuyPrefabRef;
    [SerializeField] AmmoGuy ammoGuyPrefabRef;
    [SerializeField] float maxPathLength;
    [SerializeField] float maxNpcGap;
    [SerializeField] float npcGapDecreaseAmmountOnFailing;
    GameObject m_HordeCenter;

    void Awake()
    {
        m_currentNpcHorde = new List<NPCBehaviour>();
        InitializeHordeCenter();
    }

    void Start()
    {

    }

    private void InitializeHordeCenter()
    {
        m_HordeCenter = new GameObject("HordeCenter");
        m_HordeCenter.AddComponent(typeof(NavMeshAgent2D));
        m_HordeCenter.AddComponent(typeof(Grid));
    }
    public void InitilizeHorde(HordeData hordeData, Vector2 hordeStartingPos)
    {
        NPCBehaviour npc;
        foreach (NPCData data in hordeData.startingHorde)
        {
            if (data is AmmoGuyData)
            {
                npc = Instantiate(ammoGuyPrefabRef, hordeStartingPos, Quaternion.identity, transform).GetComponent<NPCBehaviour>();
            }
            else if (data is DoctorData)
            {
                npc = Instantiate(doctorPrefabRef, hordeStartingPos, Quaternion.identity, transform).GetComponent<NPCBehaviour>();
            }
            else if (data is GrenadeGuyData)
            {
                npc = Instantiate(grenadeGuyPrefabRef, hordeStartingPos, Quaternion.identity, transform).GetComponent<NPCBehaviour>();
            }
            else if (data is NormalGuyData)
            {
                npc = Instantiate(normalGuyPrefabRef, hordeStartingPos, Quaternion.identity, transform).GetComponent<NPCBehaviour>();
            }
            else if (data is ShieldGuyData)
            {
                npc = Instantiate(shieldGuyPrefabRef, hordeStartingPos, Quaternion.identity, transform).GetComponent<NPCBehaviour>();
            }
            else
                throw new System.Exception("Invalid hordeData");
            m_currentNpcHorde.Add(npc);
            npc.Initialize(data);
        }
        m_HordeCenter.transform.position = hordeStartingPos;
        MoveHordeTowards(hordeStartingPos);
    }


    private void MoveHordeTowards(Vector2 position)
    {
        int hordeCount = m_currentNpcHorde.Count;
        NavMeshAgent2D pathfinder = m_HordeCenter.GetComponent<NavMeshAgent2D>();
        Grid grid = m_HordeCenter.GetComponent<Grid>();
        grid.displayGridGizmos = true;
        pathfinder.destination = position;
        if (pathfinder.remainingDistance > maxPathLength)
        {
            Debug.Log("The path to the choosen position is too long.");
            pathfinder.destination = m_HordeCenter.transform.position;
            return;
        }
        m_HordeCenter.transform.position = position;
        grid.UpdateGrid(new Vector2Int(hordeCount * 2, hordeCount * 2), maxNpcGap * 2, LayerMask.NameToLayer("NoneWalkable"));
        int iteration = 0;
        while (!GenerateValidPos(ref grid, hordeCount, position))
        {
            iteration++;
            grid.UpdateGrid(new Vector2Int(hordeCount * 2, hordeCount * 2), (maxNpcGap * 2) - (npcGapDecreaseAmmountOnFailing * iteration), LayerMask.NameToLayer("NoneWalkable"));
        }

        var generatedPositions = grid.GetChosenPositions();

        for (int i = 0; i < m_currentNpcHorde.Count; i++)
        {
            m_currentNpcHorde[i].SetDestination(generatedPositions[i]);
        }
    }

    private bool GenerateValidPos(ref Grid grid, int hordeCount, Vector2 startingPos)
    {
        //Doing a Flood Fill-ish Algorithm to find the nodes
        Queue<Node> nodesToCheck = new Queue<Node>();
        Node startingNode = grid.WorldPointToNode(startingPos);
        grid.SetNodeAsChosen(startingNode);
        int choosenNodeCount = 1;
        nodesToCheck.Enqueue(startingNode);
        while (nodesToCheck.Count == 0)
        {
            var nextNode = nodesToCheck.Dequeue();
            if (choosenNodeCount >= hordeCount)
                break;
            foreach (var node in grid.GetNeighbours(nextNode))
            {
                if (node.walkable)
                {
                    choosenNodeCount++;
                    grid.SetNodeAsChosen(node);
                    foreach (var i in grid.GetNeighbours(node))
                    {
                        if (!i.isChecked)
                            nodesToCheck.Enqueue(i);
                    }
                }
            }
        }
        if (choosenNodeCount == 1)
            return false;
        return true;
    }
}