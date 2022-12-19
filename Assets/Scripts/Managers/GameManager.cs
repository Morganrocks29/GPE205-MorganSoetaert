using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject playerControllerPrefab;
    public GameObject tankPawnPrefab;
    public Transform playerSpawnTransform;
    [Space(10)] // 10 pixels of spacing here.
    public GameObject playerController2Prefab;
    public GameObject tankPawn2Prefab;
    public Transform playerSpawn2Transform;

    public List<PlayerController> players;
    [Space(10)] // 10 pixels of spacing here.
    public GameObject AIControllerCowardPrefab;
    public GameObject AICowardPrefab;
    public Transform AICowardTransform;
    [Space(10)] // 10 pixels of spacing here.
    public GameObject AIControllerAttackPrefab;
    public GameObject AIAttackPrefab;
    public Transform AIAttackTransform;
    [Space(10)] // 10 pixels of spacing here.
    public GameObject AIControllerGuardPrefab;
    public GameObject AIGuardPrefab;
    public Transform AIGuardTransform;
    [Space(10)] // 10 pixels of spacing here.
    public GameObject AIControllerChasePrefab;
    public GameObject AIChasePrefab;
    public Transform AIChaseTransform;

    [Space(10)] // 10 pixels of spacing here.
    public CowardAIController AIControllerCoward;
    public GuardAIController AIControllerGuard;
    public ChaseAIController AIControllerChase;
    public AttackAIController AIControllerAttack;

    [Space(10)] // 10 pixels of spacing here.
    public GameObject[] playerTanks;
    public GameObject[] AITanks;

    [Space(10)] // 10 pixels of spacing here.
    // States
    public GameObject TitleScreenStateObject;
    public GameObject MainMenuStateObject;
    public GameObject OptionsScreenStateObject;
    public GameObject CreditsScreenStateObject;
    public GameObject GameplayStateObject;
    public GameObject GameOverScreenStateObject;

   


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
        SpawnAICoward();
        SpawnAIAttack();
        SpawnAIGuard();
        SpawnAIChase();
        ActivateTitleScreen();
        SpawnPlayer();
        SpawnPlayer2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*public GameObject RandomSpawner()
    {
        return spawnTanks[UnityEngine.Random.Range(SpawnAIAttack,SpawnAIChase)];
    }*/
    private void SpawnPlayer()
    {
        GameObject newPlayerObject = Instantiate(playerControllerPrefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObject = Instantiate(tankPawnPrefab, playerSpawnTransform.position, playerSpawnTransform.rotation);
        //Player array random 

        Controller newController = newPlayerObject.GetComponent<Controller>();
        //tankPawnPrefab newPawn = newPawnObject.GetComponent<Pawn>();
        Pawn newPawn = newPawnObject.GetComponent<Pawn>();
        newController.pawn = newPawn;
    }

    private void SpawnPlayer2()
    {
        GameObject newPlayerObject = Instantiate(playerController2Prefab, Vector3.zero, Quaternion.identity);
        GameObject newPawnObject = Instantiate(tankPawn2Prefab, playerSpawn2Transform.position, playerSpawnTransform.rotation);
        //Player array random 

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

    private void DeactivateAllStates()
    {
        //Deactivate
        TitleScreenStateObject.SetActive(false);
        MainMenuStateObject.SetActive(false);
        OptionsScreenStateObject.SetActive(false);
        CreditsScreenStateObject.SetActive(false);
        GameplayStateObject.SetActive(false);
        GameOverScreenStateObject.SetActive(false);
    }

    public void ActivateTitleScreen()
    {
        // Deactivate
        DeactivateAllStates();
        // Activate Title Screen
        TitleScreenStateObject.SetActive(true);
    }

    public void ActivateMainMenu()
    {
        // Deactivate
        DeactivateAllStates();
        // Activate Main Menu
        MainMenuStateObject.SetActive(true);
    }

    public void ActivateOptionsScreen()
    {
        // Deactivate
        DeactivateAllStates();
        // Activate Options Screen
        OptionsScreenStateObject.SetActive(true);
    }

    public void ActivateCreditsScreen()
    {
        // Deactivate
        DeactivateAllStates();
        // Activate Credits Menu
        CreditsScreenStateObject.SetActive(true);
    }

    public void ActivateGameplayState()
    {
        // Deactivate 
        DeactivateAllStates();
        // Activate Gameplay State
        GameplayStateObject.SetActive(true);
    }

    public void ActivateGameoverScreen()
    {
        // Deactivate
        DeactivateAllStates();
        // Activate Gameover Screen
        GameOverScreenStateObject.SetActive(true);
    }
}
