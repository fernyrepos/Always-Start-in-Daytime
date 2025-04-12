using HarmonyLib;
using RimWorld;
using System.Collections.Generic;
using System.Reflection.Emit;
using Verse;

namespace AlwaysStartInDaytime
{
    public class AlwaysStartInDaytimeMod : Mod
    {
        public AlwaysStartInDaytimeMod(ModContentPack pack) : base(pack)
        {
            new Harmony("AlwaysStartInDaytimeMod").PatchAll();
        }
    }

    [HarmonyPatch(typeof(GenTicks), "ConfiguredTicksAbsAtGameStart", MethodType.Getter)]
    public static class GenTicks_ConfiguredTicksAbsAtGameStart_Patch
    {
        public static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> codeInstructions)
        {
            foreach (var codeInstruction in codeInstructions)
            {
                if (codeInstruction.opcode == OpCodes.Ldc_I4_6)
                {
                    yield return new CodeInstruction(OpCodes.Ldc_I4, 12);
                }
                else
                {
                    yield return codeInstruction;
                }
            }
        }
    }
}
