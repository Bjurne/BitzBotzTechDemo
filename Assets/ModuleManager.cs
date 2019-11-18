using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleManager : MonoBehaviour
{
    //public WeaponData[] prototypeWeaponDatas;
    //public IState[] prototypeAerialBehaviours;
    //public IState[] prototypeSpecialBehaviours;

    //public enum StateType { AerialState, SpecialState, None };

    [SerializeField]
    public WeaponData[] prototypeWeaponDatas;
    
    public List<IModule> integratedModules;

    public IModule currentAerialModule;

    public int weaponTechLevel;
    public int aerialTechLevel;
    public int specialTechLevel;

    private IdleState idleState;

    public PlayerController playerController;

    public IState aerialTechState;

    public IState aerialTechLevelOneState;
    public IState aerialTechLevelTwoState;
    public IState aerialTechLevelThreeState;

    //public ModuleManager(PlayerController playerController)
    //{
    //    this.playerController = playerController;
    //}

    //public IState GetState(StateType stateType)
    //{
    //    //switch (stateType)
    //    //{
    //    //    case StateType.AerialState:
    //    //        newStateType = new SnappyJumpState(null, null, null);
    //    //        break;
    //    //    case StateType.SpecialState:
    //    //        break;
    //    //    case StateType.None:
    //    //        break;
    //    //    default:
    //    //        break;
    //    //}

    //    //newStateType = stateType;

    //    return newStateType;
    //}

    private void Awake()
    {
        aerialTechLevelOneState = NegativeGravityState();
        aerialTechLevelTwoState = ReversedDashingState();
        aerialTechLevelThreeState = DashingState();
    }

    private void Start()
    {
        integratedModules = new List<IModule>();
        IntegrateStartModules();
        playerController.AddStartingWeapons();
        //currentJumpingModule = new AerialTechModule();
        //currentJumpingModule.IntegrateModule(playerController);
    }

    public WeaponData GetPrototypeWeaponData(int weaponIndex)
    {
        if (prototypeWeaponDatas[weaponIndex] != null) return prototypeWeaponDatas[weaponIndex];
        else
        {
            Debug.LogWarning("Trying to access unavailable prototype weapon data: prototypeWeaponDatas[" + weaponIndex + "]");
            return null;
        }
    }

    public IState GetCurrentAerialTechState()
    {
        if (aerialTechLevel < 10)
        {
            aerialTechState = aerialTechLevelOneState;
            Debug.Log("AerialTechLevel is - " + aerialTechLevel + " , " + aerialTechState);
        }
        else if (aerialTechLevel < 25)
        {
            aerialTechState = aerialTechLevelTwoState;
            Debug.Log("AerialTechLevel is - " + aerialTechLevel + " , " + aerialTechState);
        }
        else
        {
            aerialTechState = aerialTechLevelThreeState;
            Debug.Log("AerialTechLevel is - " + aerialTechLevel + " , " + aerialTechState);
        }

        if (aerialTechState == null)
        {
            aerialTechState = new IdleState(10f, playerController.IdlingLoopRestart);
        }

        return aerialTechState;
    }

    public void IntegrateStartModules()
    {
        while (integratedModules.Count < 5)
        {
            if (integratedModules.Count == 0)
            {
                IModule module = new AerialTechModule();
                module.IntegrateModule(playerController);
                integratedModules.Add(module);
            }
            if (integratedModules.Count > 3)
            {
                IModule module = new WeaponModule();
                module.IntegrateModule(playerController);
                integratedModules.Add(module);
            }
            else
            {
                IModule module = new HullModule();
                module.IntegrateModule(playerController);
                integratedModules.Add(module);
            }
        }
    }

    public void AddModule(Module.ModuleType moduleType)
    {
        IModule newModule = null;
        switch (moduleType)
        {
            case Module.ModuleType.HullModule:
                newModule = new HullModule();
                break;
            case Module.ModuleType.WeaponModule:
                newModule = new WeaponModule();
                break;
            case Module.ModuleType.AerialTechModule:
                newModule = new AerialTechModule();
                break;
            case Module.ModuleType.None:
                Debug.LogWarning("ModuleType missing");
                //newModule = new Module();
                break;
            default:
                break;
        }

        newModule.IntegrateModule(playerController);
        integratedModules.Add(newModule);
    }

    public void RemoveModule(IModule module)
    {
        module.removeModule(playerController);
        integratedModules.Remove(module);
    }

    public void RemoveRandomModule()
    {
        if (integratedModules.Count > 0)
        {
            int randomModuleIndex = Random.Range(0, integratedModules.Count);
            if (integratedModules[randomModuleIndex] != null)
            {
                RemoveModule(integratedModules[randomModuleIndex]);
            }
        }
    }

    public void RemoveAllModules()
    {
        foreach (Module module in integratedModules)
        {
            RemoveModule(module);
        }
    }

    public IState NegativeGravityState()
    {
        IState newState = new NegativeGravityState(playerController);
        Debug.Log("new NegativeGravityState created, - " + newState + playerController);
        return newState;
    }

    public IState ReversedDashingState()
    {
        IState newState = new ReversedDashingState(playerController.playerRigidBody, playerController, playerController.jumpVector, playerController.stateMachine);
        Debug.Log("new ReversedDashingState created, - " + newState + playerController.playerRigidBody + playerController + playerController.jumpVector + playerController.stateMachine);
        return newState;
    }

    public IState DashingState()
    {
        IState newState = new DashingState(playerController.playerRigidBody, playerController, playerController.jumpVector, playerController.stateMachine);
        Debug.Log("new DashingState created, - " + newState + playerController.playerRigidBody + playerController + playerController.jumpVector + playerController.stateMachine);
        return newState;
    }

    public IState HoveringState()
    {
        IState newState = new HoveringState(playerController.playerRigidBody, playerController, playerController.jumpVector, playerController.stateMachine);
        Debug.Log("new HoveringState created, - " + newState + playerController.playerRigidBody + playerController + playerController.jumpVector + playerController.stateMachine);
        return newState;
    }

    public IState JetPackState()
    {
        IState newState = new JetPackState(playerController.playerRigidBody, playerController, playerController.jumpVector, playerController.stateMachine);
        Debug.Log("new JetPackState created, - " + newState + playerController.playerRigidBody + playerController + playerController.jumpVector + playerController.stateMachine);
        return newState;
    }
}
