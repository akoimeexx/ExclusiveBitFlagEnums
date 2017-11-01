namespace Tests.ExclusiveBitFlags {
    using System;

    using com.akoimeexx.Common.Attributes;
    [Flags]
    public enum EmptyExclusiveConstructor {
        Null = 0, 
        NotReal = 1 << 0,
        Fake = 1 << 1,
        [ExclusiveFlags]
        EmptyConstructor = 1 << 2,
    }
}
