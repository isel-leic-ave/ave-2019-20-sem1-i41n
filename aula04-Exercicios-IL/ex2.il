.method private hidebysig static bool  Foo(string msg) cil managed
{
  // Code size       68 (0x44)
  .maxstack  4
  IL_0000:  ldarg.0
  IL_0001:  callvirt   instance int32 [mscorlib]System.String::get_Length()
  IL_0006:  ldc.i4.1
  IL_0007:  bne.un.s   IL_000b
  IL_0009:  ldc.i4.1
  IL_000a:  ret
  IL_000b:  ldarg.0
  IL_000c:  ldc.i4.0
  IL_000d:  callvirt   instance char [mscorlib]System.String::get_Chars(int32)
  IL_0012:  ldarg.0
  IL_0013:  ldarg.0
  IL_0014:  callvirt   instance int32 [mscorlib]System.String::get_Length()
  IL_0019:  ldc.i4.1
  IL_001a:  sub
  IL_001b:  callvirt   instance char [mscorlib]System.String::get_Chars(int32)
  IL_0020:  beq.s      IL_0024
  IL_0022:  ldc.i4.0
  IL_0023:  ret
  IL_0024:  ldarg.0
  IL_0025:  callvirt   instance int32 [mscorlib]System.String::get_Length()
  IL_002a:  ldc.i4.2
  IL_002b:  bne.un.s   IL_002f
  IL_002d:  ldc.i4.1
  IL_002e:  ret
  IL_002f:  ldarg.0
  IL_0030:  ldc.i4.1
  IL_0031:  ldarg.0
  IL_0032:  callvirt   instance int32 [mscorlib]System.String::get_Length()
  IL_0037:  ldc.i4.2
  IL_0038:  sub
  IL_0039:  callvirt   instance string [mscorlib]System.String::Substring(int32,
                                                                          int32)
  IL_003e:  call       bool App::Foo(string)
  IL_0043:  ret
} // end of method App::Foo