using System;

namespace TheTVDBSharp.Entities
{
    public class TheTvDbActor : IEquatable<TheTvDbActor>
    {
        public uint ActorId { get; }

        public string ImageRemotePath { get; set; }

        public string Name { get; set; }

        public string Role { get; set; }

        public int SortOrder { get; set; }

        /// <summary>
        ///     Private parameter-less constructor to facilitate serialization
        /// </summary>
        private TheTvDbActor() { }

        public TheTvDbActor(uint actorId)
        {
            ActorId = actorId;
        }

        public bool Equals(TheTvDbActor other) => other?.ActorId == ActorId;

        public override bool Equals(object obj) => Equals(obj as TheTvDbActor);

        public override int GetHashCode() => ActorId.GetHashCode();
    }
}
