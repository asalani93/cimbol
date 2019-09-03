﻿using System.Globalization;
using System.Threading.Tasks;
using BenchmarkDotNet.Attributes;
using Cimpress.Cimbol.Runtime.Types;

namespace Cimpress.Cimbol.PerformanceTests.Evaluation
{
    public class AsyncFormulaBenchmark
    {
        private Executable _executable;

        [ParamsSource(nameof(FormulaListCount))]
        public int FormulaCount { get; set; }

        [GlobalSetup]
        public void GlobalSetup()
        {
            var program = new Program();

            var constantInner = Task.FromResult((ILocalValue)new NumberValue(1));

            var constant = program.AddConstant("Constant1", new PromiseValue(constantInner));

            var module = program.AddModule("Main");

            module.AddReference("Constant1", constant);

            for (var i = 0; i < FormulaCount; ++i)
            {
                var formulaName = string.Format(CultureInfo.InvariantCulture, "Formula{0}", i);

                module.AddFormula(formulaName, "Constant1");
            }

            _executable = program.Compile();
        }

        [Benchmark]
        public async Task<EvaluationResult> Benchmark_FormulaAsync()
        {
            return await _executable.Call();
        }

        public int[] FormulaListCount()
        {
            return new[] { 4, 8, 16, 32, 64, 128 };
        }
    }
}