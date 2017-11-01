namespace Tests.ExclusiveBitFlags {
    using System;
    using com.akoimeexx.Common.Attributes;

    [Flags]
    public enum InvalidAttribParam {
        None = 0, 
        One = 1 << 0,
        Two = 1 << 1,
        Four = 1 << 2, 
        [ExclusiveFlags(typeof(Type))]
        Eight = 1 << 3, 
    }
}
