﻿using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class MapDocumentProcessor : DocumentProcessor<Map>
    {
        private readonly string[] IgnoredNodes = {
            "/Script/DatasmithContent.DatasmithAssetUserData",
            "/Script/Engine.ArrowComponent",
            "/Script/Engine.BillboardComponent",
            "/Script/Engine.DrawSphereComponent",
            "/Script/NavigationSystem.AbstractNavData"
        };
        
        public MapDocumentProcessor()
        {
            AddNodeProcessor(new LevelProcessor());
            AddNodeProcessor(new MapProcessor());
            AddNodeProcessor(new DecalActorProcessor());
            AddNodeProcessor(new DecalComponentProcessor());
            AddNodeProcessor(new DirectionalLightActorProcessor());
            AddNodeProcessor(new DirectionalLightComponentProcessor());
            AddNodeProcessor(new ExponentialHeightFogProcessor());
            AddNodeProcessor(new ExponentialHeightFogComponentProcessor());
            AddNodeProcessor(new PointLightActorProcessor());
            AddNodeProcessor(new PointLightComponentProcessor());
            AddNodeProcessor(new PostProcessingVolumeProcessor());
            AddNodeProcessor(new SkeletalMeshActorProcessor());
            AddNodeProcessor(new SkeletalMeshComponentProcessor());
            AddNodeProcessor(new SkyLightProcessor());
            AddNodeProcessor(new SkyLightComponentProcessor());
            AddNodeProcessor(new SphereReflectionCaptureActorProcessor());
            AddNodeProcessor(new SphereReflectionCaptureComponentProcessor());
            AddNodeProcessor(new SpotLightActorProcessor());
            AddNodeProcessor(new SpotLightComponentProcessor());
            AddNodeProcessor(new StaticMeshActorProcessor());
            AddNodeProcessor(new StaticMeshComponentProcessor());
        }

        protected override bool IsIgnoredNode(ParsedNode parsedNode)
        {
            var nodeClass = parsedNode.AttributeBag.FindProperty("Class")?.Value;

            return IgnoredNodes.Count(s => s == nodeClass) != 0;
        }
    }
}
