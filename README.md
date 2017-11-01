# Exclusive Bit-Flag Enums via Attributes

---

.Net library to provide exclusivity testing for Bit-Flag enum definitions. 
Written in C# & .Net 4.6.2, using Visual Studio. 
[Licensed](https://github.com/akoimeexx/ExclusiveBitFlagEnums/blob/master/LICENSE) under 
[BSD-3](https://opensource.org/licenses/BSD-3-Clause).

### Unit Tests and Error Handling
Tests provide a near "real-world" example using the included `CLISwitches` 
enum, in addition to other arbitrary enums to test vaious use cases.

Exceptions are thrown, trapped, reported to the Error Console, then silently 
ignored, to prevent impeding program execution (indeed, when testing for a 
valid bit-flag enum, an `ArgumentOutOfRange` exception will be thrown if invalid).

### Nifty Bits

* `ExclusiveFlagsAttribute.IsValidExclusive(Enum instance)`  
  Static method provided to check for conflicting enum bit-flags. Extension 
  method wrapper for `IsValidExclusive` provided by `ExclusiveFlagsExtensions`.

### Project Progress

Stable
