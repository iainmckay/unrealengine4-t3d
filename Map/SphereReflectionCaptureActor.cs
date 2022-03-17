using System.Linq;
using JollySamurai.UnrealEngine4.T3D.Parser;
using JollySamurai.UnrealEngine4.T3D.Processor;

namespace JollySamurai.UnrealEngine4.T3D.Map
{
     public class SphereReflectionCaptureActor : BaseActorNode
     {
         public string CaptureComponentName { get; }

         public SphereReflectionCaptureComponent CaptureComponent => Children.First(node => node.Name == CaptureComponentName) as SphereReflectionCaptureComponent;

         public SphereReflectionCaptureActor(string name, ResourceReference archetype, string actorLabel, SpawnCollisionHandlingMethod spawnCollisionHandlingMethod, string folderPath, string rootComponentName, Node[] children, string parentActorName, string captureComponentName)
             : base(name, actorLabel, spawnCollisionHandlingMethod, folderPath, rootComponentName, archetype, children, parentActorName)
         {
             CaptureComponentName = captureComponentName;
         }
     }

     public class SphereReflectionCaptureActorProcessor : BaseActorNodeProcessor
     {
         public override string Class => "/Script/Engine.SphereReflectionCapture";

         public SphereReflectionCaptureActorProcessor()
         {
             AddRequiredProperty("CaptureComponent", PropertyDataType.String);

             AddOptionalProperty("CaptureOffset", PropertyDataType.String);

             AddIgnoredProperty("CaptureOffsetComponent");
             AddIgnoredProperty("DrawCaptureRadius");
             AddIgnoredProperty("SpriteComponent");
         }

         public override Node Convert(ParsedNode node, Node[] children)
         {
             return new SphereReflectionCaptureActor(
                 node.FindAttributeValue("Name"),
                 ValueUtil.ParseResourceReference(node.FindAttributeValue("Archetype")),
                 node.FindPropertyValue("ActorLabel"),
                 ValueUtil.ParseSpawnCollisionHandlingMethod(node.FindPropertyValue("SpawnCollisionHandlingMethod")),
                 node.FindPropertyValue("FolderPath"),
                 node.FindPropertyValue("RootComponent"),
                 children,
                 node.FindAttributeValue("ParentActor"),
                 node.FindPropertyValue("CaptureComponent")
             );
         }
     }
}
