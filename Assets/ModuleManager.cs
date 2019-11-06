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
            Debug.Log("Trying to access unavailable prototype weapon data: prototypeWeaponDatas[" + weaponIndex + "]");
            return null;
        }
    }

    public IState GetCurrentAerialTechState()
    {
        if (aerialTechLevel > 10)
        {
            aerialTechState = new DashingState(playerController.playerRigidBody, playerController, playerController.jumpVector, playerController.stateMachine);
            Debug.Log("AerialTechLevel is - " + aerialTechLevel + " , " + aerialTechState);
        }
        else
        {
            aerialTechState = new HoveringState(playerController.playerRigidBody, playerController, playerController.jumpVector, playerController.stateMachine);
            Debug.Log("AerialTechLevel is - " + aerialTechLevel + " , " + aerialTechState);
        }
        return aerialTechState;
    }

    private void IntegrateStartModules()
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
}
