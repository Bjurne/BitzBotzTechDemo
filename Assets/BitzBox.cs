using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitzBox : MonoBehaviour, ITakeDamage
{
    public Module.ModuleType moduleType = Module.ModuleType.None;
    public int hullPoints = 3;

    private SpriteRenderer spriteRenderer;

    public void TakeDamage(int value)
    {
        hullPoints -= value;
        if (hullPoints <= 0)
        {
            ObjectPoolManager.Instance.ReturnObjectHome(this.gameObject);
        }
    }

    public BitzBox(Module.ModuleType moduleType)
    {
        this.moduleType = moduleType;
        SetSprite();
    }

    public BitzBox()
    {

    }

    private void OnEnable()
    {
        if (moduleType == Module.ModuleType.None)
        {
            moduleType = (Module.ModuleType)Random.Range(1, System.Enum.GetValues(typeof(Module.ModuleType)).Length);
        }

        spriteRenderer = GetComponent<SpriteRenderer>();
        SetSprite();
    }

    private void SetSprite()
    {
        switch (moduleType)
        {
            case Module.ModuleType.HullModule:
                spriteRenderer.color = Color.gray;
                break;
            case Module.ModuleType.WeaponModule:
                spriteRenderer.color = Color.red;
                break;
            case Module.ModuleType.None:
                spriteRenderer.color = Color.black;
                break;
            default:
                break;
        }
    }

    public void PickedUp(PlayerController playerController)
    {
        playerController.AddModule(moduleType);
    }
}
