using System;
using Data;
using Enums;
using Interfaces.Data;
using Interfaces.Model.Systems;
using Interfaces.Presenters;
using Interfaces.Providers;
using Interfaces.View;
using Misc;
using Model.Providers;
using Model.Systems;
using Presenters;
using UnityEngine;
using View;
using Zenject.Factories;
using Zenject.ObjectsPool;

namespace Zenject
{
    public class ZenjectInstaller : MonoInstaller<ZenjectInstaller>
    {
        private const int AnswerButtonPoolSize = 4;
        private const string QuestionsContainerResourcePath = "QuestionsAssets/QuestionsContainer";
        private const string AnswerObjectPrefabResourcePath = "Prefabs/UI/AnswerObject";
        private const string AnswerObjectPoolTransformGroupname = "AnswerObjectPool";

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ScoreSystem>().AsSingle().NonLazy();
            Container.Bind<IScoreBarView>().To<ScoreBarView>().FromComponentsInHierarchy().AsSingle().NonLazy();
            Container.Bind<IScoreBarPresenter>().To<ScoreBarPresenter>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ScreenSystem>().AsSingle().NonLazy();
            Container.Bind<IScreenSystemPresenter>().To<ScreenSystemPresenter>().AsSingle().NonLazy();
            Container.Bind<IScreenFaderView>().To<ScreenFaderView>().FromComponentsInHierarchy().AsSingle();
            Container.BindInterfacesAndSelfTo<QuestionsMapScreenPresenter>().AsSingle();
            Container.BindInterfacesAndSelfTo<QuestionScreenPresenter>().AsSingle();
            Container.Bind<IQuestionsMapScreenView>().To<QuestionsMapsScreenView>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<IQuestionScreenView>().To<QuestionScreenView>().FromComponentsInHierarchy().AsSingle();
            Container.Bind<IQuestionsAssetsProvider>().To<QuestionsAssetsProvider>().AsSingle().NonLazy();
            Container.Bind<IQuestionsContainerAsset>().To<QuestionsContainerAsset>().FromScriptableObjectResource(QuestionsContainerResourcePath).AsSingle();
            Container.Bind<IAnswerValidationSystem>().To<AnswerValidationSystem>().AsSingle().NonLazy();
             Container.Bind<IAnswerButtonPoolAdapter>().To<AnswerButtonPoolAdapter>().AsSingle();
            
            BindPoolsAndFactories();
        }

        private void BindPoolsAndFactories()
        {
            Container.BindFactory<RectTransform, Vector2, IQuestionAsset, IQuestionMapTile, QuestionMapTileFactory>().FromFactory<CustomQuestionMapTileFactory>();
            Container.BindFactory<float, TileChilds, ITileChildsInfo, MapTileChildsInfoFactory>().FromFactory<CustomMapTileChildsInfoFactory>();
            Container.BindMemoryPool<AnswerButton, AnswerButtonMemoryPool>()
                .WithInitialSize(AnswerButtonPoolSize)
                .FromComponentInNewPrefabResource(AnswerObjectPrefabResourcePath)
                .UnderTransformGroup(AnswerObjectPoolTransformGroupname);
        }
    }
}