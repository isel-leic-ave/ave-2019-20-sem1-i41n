.assembly extern mscorlib
{
  .ver 4:0:0:0
}
.assembly App
{
  .ver 0:0:0:0
}

// =============== CLASS MEMBERS DECLARATION ===================

.class private abstract auto ansi sealed beforefieldinit App
       extends [mscorlib]System.Object
{
  .method private hidebysig static void  Foo(int32 nr) cil managed
  {
    // Code size       51 (0x33)
    .maxstack  8
    IL_0000:  ldarg.0
    IL_0001:  call       void [mscorlib]System.Console::Write(int32)
    IL_0006:  ldstr      ", "
    IL_000b:  call       void [mscorlib]System.Console::Write(string)
    IL_0010:  ldarg.0
    IL_0011:  ldc.i4.1
    IL_0012:  bne.un.s   IL_001a
    IL_0014:  call       void [mscorlib]System.Console::WriteLine()
    IL_0019:  ret
    IL_001a:  ldarg.0
    IL_001b:  ldc.i4.2
    IL_001c:  rem
    IL_001d:  brtrue.s   IL_0028
    IL_001f:  ldarg.0
    IL_0020:  ldc.i4.2
    IL_0021:  div
    IL_0022:  call       void App::Foo(int32)
    IL_0027:  ret
    IL_0028:  ldarg.0
    IL_0029:  ldc.i4.3
    IL_002a:  mul
    IL_002b:  ldc.i4.1
    IL_002c:  add
    IL_002d:  call       void App::Foo(int32)
    IL_0032:  ret
  } // end of method App::Foo

  .method private hidebysig static void  Main() cil managed
  {
    .entrypoint
    // Code size       15 (0xf)
    .maxstack  8
    IL_0000:  ldc.i4.s   12
    IL_0002:  call       void App::Foo(int32)
    IL_0007:  ldc.i4.s   19
    IL_0009:  call       void App::Foo(int32)
    IL_000e:  ret
  } // end of method App::Main

} // end of class App

