// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/******************************************************************************
 * This file is auto-generated from a template file by the GenerateTests.csx  *
 * script in tests\src\JIT\HardwareIntrinsics\General\Shared. In order to make    *
 * changes, please update the corresponding template and run according to the *
 * directions listed in the file.                                             *
 ******************************************************************************/

using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;

namespace JIT.HardwareIntrinsics.General
{
    public static partial class Program
    {
        private static void CreateScalarUnsafeByte()
        {
            var test = new VectorCreate__CreateScalarUnsafeByte();

            // Validates basic functionality works
            test.RunBasicScenario();

            // Validates calling via reflection works
            test.RunReflectionScenario();

            if (!test.Succeeded)
            {
                throw new Exception("One or more scenarios did not complete as expected.");
            }
        }
    }

    public sealed unsafe class VectorCreate__CreateScalarUnsafeByte
    {
        private static readonly int LargestVectorSize = 8;

        private static readonly int ElementCount = Unsafe.SizeOf<Vector64<Byte>>() / sizeof(Byte);

        public bool Succeeded { get; set; } = true;

        public void RunBasicScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunBasicScenario));

            Byte value = TestLibrary.Generator.GetByte();
            Vector64<Byte> result = Vector64.CreateScalarUnsafe(value);

            ValidateResult(result, value);
        }

        public void RunReflectionScenario()
        {
            TestLibrary.TestFramework.BeginScenario(nameof(RunReflectionScenario));

            Byte value = TestLibrary.Generator.GetByte();
            object result = typeof(Vector64)
                                .GetMethod(nameof(Vector64.CreateScalarUnsafe), new Type[] { typeof(Byte) })
                                .Invoke(null, new object[] { value });

            ValidateResult((Vector64<Byte>)(result), value);
        }

        private void ValidateResult(Vector64<Byte> result, Byte expectedValue, [CallerMemberName] string method = "")
        {
            Byte[] resultElements = new Byte[ElementCount];
            Unsafe.WriteUnaligned(ref Unsafe.As<Byte, byte>(ref resultElements[0]), result);
            ValidateResult(resultElements, expectedValue, method);
        }

        private void ValidateResult(Byte[] resultElements, Byte expectedValue, [CallerMemberName] string method = "")
        {
            if (resultElements[0] != expectedValue)
            {
                Succeeded = false;
            }
            else
            {
                for (var i = 1; i < ElementCount; i++)
                {
                    if (false /* value is uninitialized */)
                    {
                        Succeeded = false;
                        break;
                    }
                }
            }

            if (!Succeeded)
            {
                TestLibrary.TestFramework.LogInformation($"Vector64.CreateScalarUnsafe(Byte): {method} failed:");
                TestLibrary.TestFramework.LogInformation($"   value: {expectedValue}");
                TestLibrary.TestFramework.LogInformation($"  result: ({string.Join(", ", resultElements)})");
                TestLibrary.TestFramework.LogInformation(string.Empty);
            }
        }
    }
}
