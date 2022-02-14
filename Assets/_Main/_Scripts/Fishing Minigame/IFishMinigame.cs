using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

interface IFishMinigame
{
    void OnEnd();

    void ActionsInMinigame(bool isMinigameRunning);

    void Initialize();
}
