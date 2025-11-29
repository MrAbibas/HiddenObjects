using System;
using VContainer.Unity;

namespace App.Gameplay.Levels
{
    public interface ILevelResultChecker : IInitializable, IDisposable
    {
        bool LevelWin();
        bool LevelLose();
    }
}