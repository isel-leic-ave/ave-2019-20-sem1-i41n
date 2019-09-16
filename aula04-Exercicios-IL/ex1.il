.method private hidebysig static int32  gcd(int32 m, int32 n) cil managed
{
  // Code size       30 (0x1e)
  .maxstack  8
  IL_0000:  ldarg.0
  IL_0001:  ldarg.1
  IL_0002:  bne.un.s   IL_0006
  IL_0004:  ldarg.0
  IL_0005:  ret
  IL_0006:  ldarg.0
  IL_0007:  ldarg.1
  IL_0008:  ble.s      IL_0014
  IL_000a:  ldarg.0
  IL_000b:  ldarg.1
  IL_000c:  sub
  IL_000d:  ldarg.1
  IL_000e:  call       int32 App::gcd(int32, int32)
  IL_0013:  ret
  IL_0014:  ldarg.0
  IL_0015:  ldarg.1
  IL_0016:  ldarg.0
  IL_0017:  sub
  IL_0018:  call       int32 App::gcd(int32, int32)
  IL_001d:  ret
} // end of method App::gcd
