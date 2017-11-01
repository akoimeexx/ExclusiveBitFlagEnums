namespace Tests.ExclusiveBitFlags {
    using System;

    [Flags]
    public enum NonExclusive {
        None = 0, 
        Top = 1 << 0, 
        Right = 1 << 1,
        Bottom = 1 << 2, 
        Left = 1 << 3
    }
}
