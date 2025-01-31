using BenchmarkDotNet.Diagnosers;

namespace BenchmarkDotNet.Disassemblers
{
    internal class LinuxDisassembler
    {
        private readonly DisassemblyDiagnoserConfig config;

        internal LinuxDisassembler(DisassemblyDiagnoserConfig config) => this.config = config;

        internal DisassemblyResult Disassemble(DiagnoserActionParameters parameters)
            => ClrMdV2Disassembler.AttachAndDisassemble(BuildDisassemblerSettings(parameters));

        private Settings BuildDisassemblerSettings(DiagnoserActionParameters parameters)
            => new Settings(
                processId: parameters.Process.Id,
                typeName: $"BenchmarkDotNet.Autogenerated.Runnable_{parameters.BenchmarkId.Value}",
                methodName: DisassemblerConstants.DisassemblerEntryMethodName,
                printSource: config.PrintSource,
                maxDepth: config.MaxDepth,
                resultsPath: default
            );
    }
}