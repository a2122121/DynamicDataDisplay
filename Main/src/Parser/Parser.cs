﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;

namespace MathParser
{
	public sealed class Parser
	{
		public Parser() { }

		public Parser(params string[] parameters)
		{
			Contract.Assert(parameters != null);

			foreach (var parameter in parameters)
			{
				Contract.Assert(!String.IsNullOrEmpty(parameter));

				Parameters.Add(parameter);
			}
		}

		private Grammar grammar = new Grammar();
		public Grammar Grammar
		{
			get { return grammar; }
		}

		private readonly ParameterExpressionEqualityComparer comparer = new ParameterExpressionEqualityComparer();

		public ParsingResult Parse(string expression)
		{
			InputStream input = new InputStream(expression);
			var tokens = grammar.Parse(input);
			var filteredTokens = grammar.Filter(tokens);
			var mixedTokens = grammar.ConvertToMixed(filteredTokens);
			var ast = grammar.CreateAST(mixedTokens);

			if (mixedTokens.Count != 1)
				throw new ParserException("Wrong expression.");

			var optimizedAst = ast.Optimize();

			var result = new ParsingResult
			{
				Tree = optimizedAst,
				ParameterNames = Parameters,
				ParameterExpressions = grammar.ParameterExpressions.Values.ToArray()
			};
			return result;
		}

		public double Evaluate(string expression)
		{
			return Parse(expression).Evaluate();
		}

		public Collection<string> Parameters
		{
			get { return grammar.Parameters; }
		}
	}
}
