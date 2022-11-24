using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
    public Transform playerSpawnTransform;
    public List<PlayerController> players;
    public GameObject AIControllerCowardPrefab;
    public GameObject AICowardPrefab;
    public Transform AICowardTransform;
    public GameObject AIControllerAttackPrefab;
    public GameObject AIAttackPrefab;
    public Transform AIAttackTransform;
    public GameObject AIControllerGuardPrefab;
    public GameObject AIGuardPrefab;
    public Transform AIGuardTransform;
    public GameObject AIControllerChasePrefab;
    public GameObject AIChasePrefab;
    public Transform AIChaseTransform;

    public CowardAIController AIControllerCoward;
    public GuardAIController AIControllerGuard;
    public ChaseAIController AIControllerChase;
    public AttackAIController AIControllerAttack;

    private void Awake()
    {
        players = new List<PlayerController>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer();
        SpawnAICoward();
        SpawnAIAttack();
        SpawnAIGuard();
        SpawnAIChase();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnPlayer()
    {
        GameObject newPlayerObject = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObject = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);
        

        Controller newController = newPlayerObject.GetComponent<Controller>();
        //tankPawnPrefab newPawn = newPawnObject.GetComponent<Pawn>();
        Pawn newPawn = newPawnObject.GetComponent<Pawn>();
        newController.pawn = newPawn;
    }

    private void SpawnAICoward()
    {
        GameObject newPlayerObject = Instantiate(AIControllerCowardPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObject = Instantiate(AICowardPrefab, AICowardTransform.position, AICowardTransform.rotation);


        Controller newController = newPlayerObject.GetComponent<Controller>();
        //tankPawnPrefab newPawn = newPawnObject.GetComponent<Pawn>();
        Pawn newPawn = newPawnObject.GetComponent<Pawn>();
        newController.pawn = newPawn;
    }

    private void SpawnAIAttack()
    {
        GameObject newPlayerObject = Instantiate(AIControllerAttackPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObject = Instantiate(AIAttackPrefab, AIAttackTransform.position, AIAttackTransform.rotation);


        Controller newController = newPlayerObject.GetComponent<Controller>();
        //tankPawnPrefab newPawn = newPawnObject.GetComponent<Pawn>();
        Pawn newPawn = newPawnObject.GetComponent<Pawn>();
        newController.pawn = newPawn;
    }

    private void SpawnAIGuard()
    {
        GameObject newPlayerObject = Instantiate(AIControllerGuardPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObject = Instantiate(AIGuardPrefab, AIGuardTransform.position, AIGuardTransform.rotation);


        Controller newController = newPlayerObject.GetComponent<Controller>();
        //tankPawnPrefab newPawn = newPawnObject.GetComponent<Pawn>();
        Pawn newPawn = newPawnObject.GetComponent<Pawn>();
        newController.pawn = newPawn;
    }

    private void SpawnAIChase()
    {
        GameObject newPlayerObject = Instantiate(AIControllerChasePrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObject = Instantiate(AIChasePrefab, AIChaseTransform.position, AIChaseTransform.rotation);


        Controller newController = newPlayerObject.GetComponent<Controller>();
        //tankPawnPrefab newPawn = newPawnObject.GetComponent<Pawn>();
        Pawn newPawn = newPawnObject.GetComponent<Pawn>();
        newController.pawn = newPawn;
    }
}
