﻿
//  Microsoft (R) .NET Framework IL Disassembler.  Version 4.8.3928.0
//  Copyright (c) Microsoft Corporation.  All rights reserved.



// Metadata version: v4.0.30319
.assembly extern mscorlib
{
  .publickeytoken = (B7 7A 5C 56 19 34 E0 89 )                         // .z\V.4..
  .ver 4:0:0:0
}
.assembly App
{
  .custom instance void [mscorlib]System.Runtime.CompilerServices.CompilationRelaxationsAttribute::.ctor(int32) = ( 01 00 08 00 00 00 00 00 ) 
  .custom instance void [mscorlib]System.Runtime.CompilerServices.RuntimeCompatibilityAttribute::.ctor() = ( 01 00 01 00 54 02 16 57 72 61 70 4E 6F 6E 45 78   // ....T..WrapNonEx
                                                                                                             63 65 70 74 69 6F 6E 54 68 72 6F 77 73 01 )       // ceptionThrows.

  // --- The following custom attribute is added automatically, do not uncomment -------
  //  .custom instance void [mscorlib]System.Diagnostics.DebuggableAttribute::.ctor(valuetype [mscorlib]System.Diagnostics.DebuggableAttribute/DebuggingModes) = ( 01 00 07 01 00 00 00 00 ) 

  .hash algorithm 0x00008004
  .ver 0:0:0:0
}
.module App.exe
// MVID: {C260E98F-6C14-43D5-88D0-87A16E7460D8}
.imagebase 0x00400000
.file alignment 0x00000200
.stackreserve 0x00100000
.subsystem 0x0003       // WINDOWS_CUI
.corflags 0x00000001    //  ILONLY
// Image base: 0x07300000


// =============== CLASS MEMBERS DECLARATION ===================

.class interface private abstract auto ansi I
{
  .method public hidebysig newslot abstract virtual 
          instance void  MI() cil managed
  {
  } // end of method I::MI

} // end of class I

.class private auto ansi beforefieldinit A
       extends [mscorlib]System.Object
       implements I
{
  .field private initonly int32 nr
  .method public hidebysig static void  S() cil managed
  {
    // Code size       13 (0xd)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldstr      "S"
    IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_000b:  nop
    IL_000c:  ret
  } // end of method A::S

  .method public hidebysig instance void 
          M() cil managed
  {
    // Code size       32 (0x20)
    .maxstack  2
    .locals init (int32 V_0)
    IL_0000:  nop
    IL_0001:  ldstr      "M"
    IL_0006:  ldarg.0
    IL_0007:  ldfld      int32 A::nr
    IL_000c:  stloc.0
    IL_000d:  ldloca.s   V_0
    IL_000f:  call       instance string [mscorlib]System.Int32::ToString()
    IL_0014:  call       string [mscorlib]System.String::Concat(string,
                                                                string)
    IL_0019:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_001e:  nop
    IL_001f:  ret
  } // end of method A::M

  .method public hidebysig instance void 
          M2() cil managed
  {
    // Code size       9 (0x9)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldarg.0
    IL_0002:  call       instance void A::M()
    IL_0007:  nop
    IL_0008:  ret
  } // end of method A::M2

  .method public hidebysig newslot virtual 
          instance void  MV() cil managed
  {
    // Code size       13 (0xd)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldstr      "MV"
    IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_000b:  nop
    IL_000c:  ret
  } // end of method A::MV

  .method public hidebysig newslot virtual 
          instance void  MI() cil managed
  {
    // Code size       13 (0xd)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldstr      "MI"
    IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_000b:  nop
    IL_000c:  ret
  } // end of method A::MI

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       16 (0x10)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  ldc.i4.s   73
    IL_0003:  stfld      int32 A::nr
    IL_0008:  ldarg.0
    IL_0009:  call       instance void [mscorlib]System.Object::.ctor()
    IL_000e:  nop
    IL_000f:  ret
  } // end of method A::.ctor

} // end of class A

.class private auto ansi beforefieldinit B
       extends A
{
  .method public hidebysig virtual instance void 
          MV() cil managed
  {
    // Code size       20 (0x14)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  ldstr      "MV of B"
    IL_0006:  call       void [mscorlib]System.Console::WriteLine(string)
    IL_000b:  nop
    IL_000c:  ldarg.0
    IL_000d:  callvirt       instance void A::MV()
    IL_0012:  nop
    IL_0013:  ret
  } // end of method B::MV

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       8 (0x8)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void A::.ctor()
    IL_0006:  nop
    IL_0007:  ret
  } // end of method B::.ctor

} // end of class B

.class private auto ansi beforefieldinit App
       extends [mscorlib]System.Object
{
  .method private hidebysig static void  Main() cil managed
  {
    .entrypoint
    // Code size       13 (0xd)
    .maxstack  8
    IL_0000:  nop
    IL_0001:  newobj     instance void B::.ctor()
    IL_0006:  callvirt   instance void A::MV()
    IL_000b:  nop
    IL_000c:  ret
  } // end of method App::Main

  .method public hidebysig specialname rtspecialname 
          instance void  .ctor() cil managed
  {
    // Code size       8 (0x8)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       instance void [mscorlib]System.Object::.ctor()
    IL_0006:  nop
    IL_0007:  ret
  } // end of method App::.ctor

} // end of class App


// =============================================================

// *********** DISASSEMBLY COMPLETE ***********************
// WARNING: Created Win32 resource file D:\ISEL\pg5 ave - 2019-2020 - 1º sem\aulas\aula38-call-callvirt-and-virtual\App.res
