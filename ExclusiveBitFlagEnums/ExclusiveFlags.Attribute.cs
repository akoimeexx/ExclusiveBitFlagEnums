/**      1         2         3         4         5         6         7         8
 * 45678901234567890123456789012345678901234567890123456789012345678901234567890
 *
 * Common.Attributes.ExclusiveFlags: Common library include that provides the 
 *    ability to declare exclusive bit-flags in Enumerations, v.0.0.1
 *    Johnathan Graham McKnight <akoimeexx@gmail.com>
 *
 *
 * Copyright (c) 2017, Johnathan Graham McKnight
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without
 * modification, are permitted provided that the following conditions are met:
 *
 * 1. Redistributions of source code must retain the above copyright notice,
 * this list of conditions and the following disclaimer.
 *
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 * this list of conditions and the following disclaimer in the documentation
 * and/or other materials provided with the distribution.
 *
 * 3. Neither the name of the copyright holder nor the names of its contributors
 * may be used to endorse or promote products derived from this software without
 * specific prior written permission.
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS"
 * AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE
 * IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE
 * ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT HOLDER OR CONTRIBUTORS BE
 * LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR
 * CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF
 * SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR PROFITS; OR BUSINESS
 * INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN
 * CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE)
 * ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED OF THE
 * POSSIBILITY OF SUCH DAMAGE.
 */
namespace com.akoimeexx.Common.Attributes {
    using System;
    using System.Reflection;

    /// <summary>
    /// Enumeration value attribute that allows declaring exclusive flags in a 
    /// bitwise Enum definition
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Field, 
        Inherited = true, 
        AllowMultiple = false
    )]
    public sealed partial class ExclusiveFlagsAttribute : Attribute {
#region Properties
        /// <summary>
        /// Flags that the defined enum value cannot be set with
        /// </summary>
        public object[] ExclusiveFlags {
            get { return _exclusiveFlags; }
        } private readonly object[] _exclusiveFlags;
#endregion Properties
    }
    public sealed partial class ExclusiveFlagsAttribute {
#region Methods
        /// <summary>
        /// Exclusive field validation method for 
        /// ExclusiveFlagsAttribute-marked objects
        /// </summary>
        /// <param name="instance">
        /// value to be checked for invalid combinations of flags
        /// </param>
        /// <returns>
        /// true on success/no ExclusiveFlagAttributes, false on conflicting 
        /// attributes
        /// </returns>
        public static bool IsValidExclusive(Enum instance) {
            bool b = default(bool);
            try {
                // Exclusive field container declaration
                // Fetch any FieldInfo information that has 
                // ExclusiveFlagsAttributes defined; if there are any FieldInfo 
                // elements, check to see if any invalidate the passed in 
                // instance value
                foreach (FieldInfo fi in Array.FindAll(
                    instance.GetType().GetFields(
                        BindingFlags.Public | BindingFlags.Static
                    ), 
                    _ => {
                        return IsDefined(_, typeof(ExclusiveFlagsAttribute));
                    }
                )) {
                    // If instance contains the current exclusive-enabled field 
                    // value...
                    // ...for each value that's excluded in that field value...
                    // ...if the instance also contains one of those excluded 
                    //    values from the field value, throw an exception and 
                    //    break.
                    if (instance.HasFlag((Enum)Enum.Parse(
                        instance.GetType(), 
                        fi.GetRawConstantValue().ToString()
                    ))) foreach (object exclusiveValue in (
                        (ExclusiveFlagsAttribute)fi.GetCustomAttributes(
                            typeof(ExclusiveFlagsAttribute), true
                        )[0]
                    ).ExclusiveFlags) if (
                        instance.HasFlag((Enum)exclusiveValue)
                    ) throw new ArgumentOutOfRangeException(
                        nameof(instance), 
                        String.Format(
                            "`{0}` are not valid inclusive bitwise values", 
                            instance
                        )
                    );
                }
                b = true;
            } catch (Exception e) {
#if DEBUG
                Console.Error.WriteLine(e.Message);
#endif
            }
            return b;
        }
#endregion Methods
    }
    public sealed partial class ExclusiveFlagsAttribute {
#region Constructors & Destructor
        /// <summary>
        /// Creates a new enum value attribute that declares exclusive flags 
        /// for said value
        /// </summary>
        /// <param name="exclusiveFlags"></param>
        public ExclusiveFlagsAttribute(params object[] exclusiveFlags) {
            foreach (var value in exclusiveFlags)
                if (!(value is Enum)) throw new ArgumentOutOfRangeException(
                    nameof(exclusiveFlags), 
                    String.Format("`{0}` is not a valid enum", value)
                );
            _exclusiveFlags = exclusiveFlags;
        }
#endregion Constructors & Destructor
    }


    /// <summary>
    /// Extension method wrapper class
    /// </summary>
    public static class ExclusiveFlagsExtensions {
#region Methods
        /// <summary>
        /// Extension method wrapper around exclusive field validation method 
        /// provided by ExclusiveFlagsAttribute class
        /// </summary>
        /// <param name="instance">
        /// value to be checked for invalid combinations of flags
        /// </param>
        /// <returns>
        /// true on success/no ExclusiveFlagAttributes, false on conflicting 
        /// attributes
        /// </returns>
        public static bool IsValidExclusive(this Enum instance) {
            return ExclusiveFlagsAttribute.IsValidExclusive(instance);
        }
#endregion Methods
    }
}
