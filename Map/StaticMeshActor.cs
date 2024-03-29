﻿using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
    public class StaticMeshActor : BaseActorNode
    {
        public string StaticMeshComponentName { get; }

        public StaticMeshComponent StaticMeshComponent => Children.First(node => node.Name == StaticMeshComponentName) as StaticMeshComponent;

        public StaticMeshActor(string name, ResourceReference archetype, string actorLabel, SpawnCollisionHandlingMethod spawnCollisionHandlingMethod, string folderPath, string rootComponentName, Node[] children, string parentActorName, string staticMeshComponentName)
            : base(name, actorLabel, spawnCollisionHandlingMethod, folderPath, rootComponentName, archetype, children, parentActorName)
        {
            StaticMeshComponentName = staticMeshComponentName;
        }
    }

    public class StaticMeshActorProcessor : BaseActorNodeProcessor
    {
        public override string Class => "/Script/Engine.StaticMeshActor";

        public StaticMeshActorProcessor()
        {
            AddRequiredProperty("StaticMeshComponent", PropertyDataType.String);
        }

        public override Node Convert(ParsedNode node, Node[] children)
        {
            return new StaticMeshActor(
                node.FindAttributeValue("Name"),
                ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                node.FindPropertyValue("ActorLabel"),
                ValueUtil.ParseSpawnCollisionHandlingMethod(node.FindPropertyValue("SpawnCollisionHandlingMethod")),
                node.FindPropertyValue("FolderPath"),
                node.FindPropertyValue("RootComponent"),
                children,
                node.FindAttributeValue("ParentActor"),
                node.FindPropertyValue("StaticMeshComponent")
            );
        }
    }
}
