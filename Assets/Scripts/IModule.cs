using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IModule
{
    void IntegrateModule(PlayerController playerController);

    void removeModule(PlayerController playerController);

    void activateModuleSpecial(PlayerController playerController);
}
