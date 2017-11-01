namespace Tests.ExclusiveBitFlags {
    using System;
    using com.akoimeexx.Common.Attributes;

    [Flags]
    public enum CLISwitches {
        None = 0, 
        [ExclusiveFlags(WideListing)]
        LongListing = 1 << 0,
        [ExclusiveFlags(LongListing)]
        WideListing = 1 << 1, 
        Recursive = 1 << 2, 
        HumanReadable = 1 << 3
    }
}
