using System;
using System.Collections.Generic;
using App.Factories;
using App.Gameplay.Common;
using App.Gameplay.GameplayFSM;
using App.Gameplay.GameplayFSM.States;
using App.Gameplay.ItemCollecting;
using App.Gameplay.Items;
using App.Gameplay.Levels;
using App.UI.CollectedItemsPanel;
using App.UI.HUD;
using App.UI.LevelResult;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace App.LifetimeScopes
{
    public class GameplayLifetimeScope : LifetimeScope
    {
        [SerializeField] private LevelConfig testLevel;
        [SerializeField] private HUD hud;
        [SerializeField] private CollectedItemsPanel collectedItemsPanel;
        [SerializeField] private LevelResultPanel levelResultPanel;
        [SerializeField] private List<ItemOnField> itemsOnField;
        [SerializeField] private ItemCollectAnimationConfig itemCollectAnimationConfig;
        [SerializeField] private CollectedItemsAnimationsConfig collectedItemsAnimationsConfig;
        [SerializeField] private CameraConfig cameraConfig;
        
        protected override void Configure(IContainerBuilder builder)
        {
            RegisterGameplayStates(builder);
            builder.RegisterEntryPoint<GameplayStateFactory>();
            builder.RegisterEntryPoint<GameplayStateMachine>()
                .AsSelf();
            builder.Register<LevelResultChecker>(Lifetime.Singleton)
                .As<ILevelResultChecker>()
                .As<IDisposable>();
            builder.Register<Timer>(Lifetime.Singleton);
            builder.Register<CameraController>(Lifetime.Singleton);
            
            builder.RegisterInstance(testLevel);
            builder.RegisterInstance(cameraConfig);
            builder.RegisterInstance(itemsOnField);
            
            builder.Register<CollectedItemsContainer>(Lifetime.Singleton);
            builder.Register<CollectedItemsContainerAnimator>(Lifetime.Singleton);
            builder.RegisterInstance(collectedItemsAnimationsConfig);
            
            builder.Register<LeftToCollectItemsContainer>(Lifetime.Singleton);
            
            builder.Register<ItemCollector>(Lifetime.Singleton)
                .AsSelf()
                .As<IItemCollector>();
            builder.Register<ItemCollectAnimator>(Lifetime.Singleton);
            builder.RegisterInstance(itemCollectAnimationConfig);

            builder.Register<SlotMerger>(Lifetime.Singleton);
            
            RegisterUIElements(builder);
        }

        private void RegisterUIElements(IContainerBuilder builder)
        {
            builder.RegisterComponent(hud);
            builder.RegisterComponent(collectedItemsPanel);
            builder.RegisterComponent(levelResultPanel);
        }

        private void RegisterGameplayStates(IContainerBuilder builder)
        {
            builder.Register<InitLevelGameplayState>(Lifetime.Transient);
            builder.Register<MainLoopGameplayState>(Lifetime.Transient);
            builder.Register<LevelLoseGameplayState>(Lifetime.Transient);
            builder.Register<LevelWinGameplayState>(Lifetime.Transient);
        }
    }
}
