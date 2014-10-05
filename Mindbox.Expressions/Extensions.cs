﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mindbox.Expressions
{
	/// <summary>
	/// Contains extension methods.
	/// </summary>
	public static class Extensions
	{
		/// <summary>
		/// Finds nested expression evaluations in the expression and, 
		/// if possible, expands their bodies and substitutes the parameters with the argument values.
		/// Nested expression evaluation can be either <c>Evaluate</c> method call or <c>Expression{}.Compile</c>
		/// method call producing a delegate that is then invoked.
		/// </summary>
		/// <param name="expression">Expression that can contain nested expression evaluations. Cannot be null.</param>
		/// <returns>Expression with replaced nested expression evaluations.</returns>
		public static Expression ExpandExpressions(this Expression expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return ExpressionExpander.ExpandExpression(expression);
		}

		/// <summary>
		/// Finds nested expression evaluations in the expression and, 
		/// if possible, expands their bodies and substitutes the parameters with the argument values.
		/// Nested expression evaluation can be either <c>Evaluate</c> method call or <c>Expression{}.Compile</c>
		/// method call producing a delegate that is then invoked.
		/// </summary>
		/// <param name="expression">Expression that can contain nested expression evaluations. Cannot be null.</param>
		/// <returns>Expression with replaced nested expression evaluations.</returns>
		public static Expression<TDelegate> ExpandExpressions<TDelegate>(this Expression<TDelegate> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return (Expression<TDelegate>)ExpandExpressions((Expression)expression);
		}

		/// <summary>
		/// Finds nested expression evaluations in the expressions used to build the query and, 
		/// if possible, expands their bodies and substitutes the parameters with the argument values.
		/// Nested expression evaluation can be either <c>Evaluate</c> method call or <c>Expression{}.Compile</c>
		/// method call producing a delegate that is then invoked.
		/// </summary>
		/// <param name="query">Query build using expressions that can contain nested expression evaluations.
		/// Cannot be null.</param>
		/// <returns>Query rebuilt using expressions with replaced nested expression evaluations.</returns>
		public static IQueryable<T> ExpandExpressions<T>(this IQueryable<T> query)
		{
			return (IQueryable<T>)query.Provider.CreateQuery(query.Expression.ExpandExpressions());
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<TResult>(this Expression<Func<TResult>> expression)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()();
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, TResult>(
			this Expression<Func<T1, TResult>> expression,
			T1 argument1)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(argument1);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, TResult>(
			this Expression<Func<T1, T2, TResult>> expression, 
			T1 argument1,
			T2 argument2)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(argument1, argument2);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, TResult>(
			this Expression<Func<T1, T2, T3, TResult>> expression, 
			T1 argument1,
			T2 argument2, 
			T3 argument3)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(argument1, argument2, argument3);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="T4">Argument #4 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <param name="argument4">Argument #4.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, T4, TResult>(
			this Expression<Func<T1, T2, T3, T4, TResult>> expression,
			T1 argument1, 
			T2 argument2, 
			T3 argument3, 
			T4 argument4)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(argument1, argument2, argument3, argument4);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="T4">Argument #4 type.</typeparam>
		/// <typeparam name="T5">Argument #5 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <param name="argument4">Argument #4.</param>
		/// <param name="argument5">Argument #5.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, T4, T5, TResult>(
			this Expression<Func<T1, T2, T3, T4, T5, TResult>> expression,
			T1 argument1, 
			T2 argument2, 
			T3 argument3, 
			T4 argument4, 
			T5 argument5)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(argument1, argument2, argument3, argument4, argument5);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="T4">Argument #4 type.</typeparam>
		/// <typeparam name="T5">Argument #5 type.</typeparam>
		/// <typeparam name="T6">Argument #6 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <param name="argument4">Argument #4.</param>
		/// <param name="argument5">Argument #5.</param>
		/// <param name="argument6">Argument #6.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, T4, T5, T6, TResult>(
			this Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression,
			T1 argument1,
			T2 argument2,
			T3 argument3,
			T4 argument4,
			T5 argument5,
			T6 argument6)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(argument1, argument2, argument3, argument4, argument5, argument6);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="T4">Argument #4 type.</typeparam>
		/// <typeparam name="T5">Argument #5 type.</typeparam>
		/// <typeparam name="T6">Argument #6 type.</typeparam>
		/// <typeparam name="T7">Argument #7 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <param name="argument4">Argument #4.</param>
		/// <param name="argument5">Argument #5.</param>
		/// <param name="argument6">Argument #6.</param>
		/// <param name="argument7">Argument #7.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, T4, T5, T6, T7, TResult>(
			this Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression,
			T1 argument1,
			T2 argument2,
			T3 argument3,
			T4 argument4,
			T5 argument5,
			T6 argument6,
			T7 argument7)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(
				argument1, 
				argument2, 
				argument3, 
				argument4, 
				argument5, 
				argument6, 
				argument7);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="T4">Argument #4 type.</typeparam>
		/// <typeparam name="T5">Argument #5 type.</typeparam>
		/// <typeparam name="T6">Argument #6 type.</typeparam>
		/// <typeparam name="T7">Argument #7 type.</typeparam>
		/// <typeparam name="T8">Argument #8 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <param name="argument4">Argument #4.</param>
		/// <param name="argument5">Argument #5.</param>
		/// <param name="argument6">Argument #6.</param>
		/// <param name="argument7">Argument #7.</param>
		/// <param name="argument8">Argument #8.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(
			this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expression,
			T1 argument1,
			T2 argument2,
			T3 argument3,
			T4 argument4,
			T5 argument5,
			T6 argument6,
			T7 argument7,
			T8 argument8)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(
				argument1,
				argument2,
				argument3,
				argument4,
				argument5,
				argument6,
				argument7,
				argument8);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="T4">Argument #4 type.</typeparam>
		/// <typeparam name="T5">Argument #5 type.</typeparam>
		/// <typeparam name="T6">Argument #6 type.</typeparam>
		/// <typeparam name="T7">Argument #7 type.</typeparam>
		/// <typeparam name="T8">Argument #8 type.</typeparam>
		/// <typeparam name="T9">Argument #9 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <param name="argument4">Argument #4.</param>
		/// <param name="argument5">Argument #5.</param>
		/// <param name="argument6">Argument #6.</param>
		/// <param name="argument7">Argument #7.</param>
		/// <param name="argument8">Argument #8.</param>
		/// <param name="argument9">Argument #9.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(
			this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression,
			T1 argument1,
			T2 argument2,
			T3 argument3,
			T4 argument4,
			T5 argument5,
			T6 argument6,
			T7 argument7,
			T8 argument8,
			T9 argument9)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(
				argument1,
				argument2,
				argument3,
				argument4,
				argument5,
				argument6,
				argument7,
				argument8,
				argument9);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="T4">Argument #4 type.</typeparam>
		/// <typeparam name="T5">Argument #5 type.</typeparam>
		/// <typeparam name="T6">Argument #6 type.</typeparam>
		/// <typeparam name="T7">Argument #7 type.</typeparam>
		/// <typeparam name="T8">Argument #8 type.</typeparam>
		/// <typeparam name="T9">Argument #9 type.</typeparam>
		/// <typeparam name="T10">Argument #10 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <param name="argument4">Argument #4.</param>
		/// <param name="argument5">Argument #5.</param>
		/// <param name="argument6">Argument #6.</param>
		/// <param name="argument7">Argument #7.</param>
		/// <param name="argument8">Argument #8.</param>
		/// <param name="argument9">Argument #9.</param>
		/// <param name="argument10">Argument #10.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(
			this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expression,
			T1 argument1,
			T2 argument2,
			T3 argument3,
			T4 argument4,
			T5 argument5,
			T6 argument6,
			T7 argument7,
			T8 argument8,
			T9 argument9,
			T10 argument10)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(
				argument1,
				argument2,
				argument3,
				argument4,
				argument5,
				argument6,
				argument7,
				argument8,
				argument9,
				argument10);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="T4">Argument #4 type.</typeparam>
		/// <typeparam name="T5">Argument #5 type.</typeparam>
		/// <typeparam name="T6">Argument #6 type.</typeparam>
		/// <typeparam name="T7">Argument #7 type.</typeparam>
		/// <typeparam name="T8">Argument #8 type.</typeparam>
		/// <typeparam name="T9">Argument #9 type.</typeparam>
		/// <typeparam name="T10">Argument #10 type.</typeparam>
		/// <typeparam name="T11">Argument #11 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <param name="argument4">Argument #4.</param>
		/// <param name="argument5">Argument #5.</param>
		/// <param name="argument6">Argument #6.</param>
		/// <param name="argument7">Argument #7.</param>
		/// <param name="argument8">Argument #8.</param>
		/// <param name="argument9">Argument #9.</param>
		/// <param name="argument10">Argument #10.</param>
		/// <param name="argument11">Argument #11.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(
			this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expression,
			T1 argument1,
			T2 argument2,
			T3 argument3,
			T4 argument4,
			T5 argument5,
			T6 argument6,
			T7 argument7,
			T8 argument8,
			T9 argument9,
			T10 argument10,
			T11 argument11)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(
				argument1,
				argument2,
				argument3,
				argument4,
				argument5,
				argument6,
				argument7,
				argument8,
				argument9,
				argument10,
				argument11);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="T4">Argument #4 type.</typeparam>
		/// <typeparam name="T5">Argument #5 type.</typeparam>
		/// <typeparam name="T6">Argument #6 type.</typeparam>
		/// <typeparam name="T7">Argument #7 type.</typeparam>
		/// <typeparam name="T8">Argument #8 type.</typeparam>
		/// <typeparam name="T9">Argument #9 type.</typeparam>
		/// <typeparam name="T10">Argument #10 type.</typeparam>
		/// <typeparam name="T11">Argument #11 type.</typeparam>
		/// <typeparam name="T12">Argument #12 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <param name="argument4">Argument #4.</param>
		/// <param name="argument5">Argument #5.</param>
		/// <param name="argument6">Argument #6.</param>
		/// <param name="argument7">Argument #7.</param>
		/// <param name="argument8">Argument #8.</param>
		/// <param name="argument9">Argument #9.</param>
		/// <param name="argument10">Argument #10.</param>
		/// <param name="argument11">Argument #11.</param>
		/// <param name="argument12">Argument #12.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(
			this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> expression,
			T1 argument1,
			T2 argument2,
			T3 argument3,
			T4 argument4,
			T5 argument5,
			T6 argument6,
			T7 argument7,
			T8 argument8,
			T9 argument9,
			T10 argument10,
			T11 argument11,
			T12 argument12)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(
				argument1,
				argument2,
				argument3,
				argument4,
				argument5,
				argument6,
				argument7,
				argument8,
				argument9,
				argument10,
				argument11,
				argument12);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="T4">Argument #4 type.</typeparam>
		/// <typeparam name="T5">Argument #5 type.</typeparam>
		/// <typeparam name="T6">Argument #6 type.</typeparam>
		/// <typeparam name="T7">Argument #7 type.</typeparam>
		/// <typeparam name="T8">Argument #8 type.</typeparam>
		/// <typeparam name="T9">Argument #9 type.</typeparam>
		/// <typeparam name="T10">Argument #10 type.</typeparam>
		/// <typeparam name="T11">Argument #11 type.</typeparam>
		/// <typeparam name="T12">Argument #12 type.</typeparam>
		/// <typeparam name="T13">Argument #13 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <param name="argument4">Argument #4.</param>
		/// <param name="argument5">Argument #5.</param>
		/// <param name="argument6">Argument #6.</param>
		/// <param name="argument7">Argument #7.</param>
		/// <param name="argument8">Argument #8.</param>
		/// <param name="argument9">Argument #9.</param>
		/// <param name="argument10">Argument #10.</param>
		/// <param name="argument11">Argument #11.</param>
		/// <param name="argument12">Argument #12.</param>
		/// <param name="argument13">Argument #13.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
			this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>> expression,
			T1 argument1,
			T2 argument2,
			T3 argument3,
			T4 argument4,
			T5 argument5,
			T6 argument6,
			T7 argument7,
			T8 argument8,
			T9 argument9,
			T10 argument10,
			T11 argument11,
			T12 argument12,
			T13 argument13)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(
				argument1,
				argument2,
				argument3,
				argument4,
				argument5,
				argument6,
				argument7,
				argument8,
				argument9,
				argument10,
				argument11,
				argument12,
				argument13);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="T4">Argument #4 type.</typeparam>
		/// <typeparam name="T5">Argument #5 type.</typeparam>
		/// <typeparam name="T6">Argument #6 type.</typeparam>
		/// <typeparam name="T7">Argument #7 type.</typeparam>
		/// <typeparam name="T8">Argument #8 type.</typeparam>
		/// <typeparam name="T9">Argument #9 type.</typeparam>
		/// <typeparam name="T10">Argument #10 type.</typeparam>
		/// <typeparam name="T11">Argument #11 type.</typeparam>
		/// <typeparam name="T12">Argument #12 type.</typeparam>
		/// <typeparam name="T13">Argument #13 type.</typeparam>
		/// <typeparam name="T14">Argument #14 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <param name="argument4">Argument #4.</param>
		/// <param name="argument5">Argument #5.</param>
		/// <param name="argument6">Argument #6.</param>
		/// <param name="argument7">Argument #7.</param>
		/// <param name="argument8">Argument #8.</param>
		/// <param name="argument9">Argument #9.</param>
		/// <param name="argument10">Argument #10.</param>
		/// <param name="argument11">Argument #11.</param>
		/// <param name="argument12">Argument #12.</param>
		/// <param name="argument13">Argument #13.</param>
		/// <param name="argument14">Argument #14.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
			this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>> expression,
			T1 argument1,
			T2 argument2,
			T3 argument3,
			T4 argument4,
			T5 argument5,
			T6 argument6,
			T7 argument7,
			T8 argument8,
			T9 argument9,
			T10 argument10,
			T11 argument11,
			T12 argument12,
			T13 argument13,
			T14 argument14)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(
				argument1,
				argument2,
				argument3,
				argument4,
				argument5,
				argument6,
				argument7,
				argument8,
				argument9,
				argument10,
				argument11,
				argument12,
				argument13,
				argument14);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="T4">Argument #4 type.</typeparam>
		/// <typeparam name="T5">Argument #5 type.</typeparam>
		/// <typeparam name="T6">Argument #6 type.</typeparam>
		/// <typeparam name="T7">Argument #7 type.</typeparam>
		/// <typeparam name="T8">Argument #8 type.</typeparam>
		/// <typeparam name="T9">Argument #9 type.</typeparam>
		/// <typeparam name="T10">Argument #10 type.</typeparam>
		/// <typeparam name="T11">Argument #11 type.</typeparam>
		/// <typeparam name="T12">Argument #12 type.</typeparam>
		/// <typeparam name="T13">Argument #13 type.</typeparam>
		/// <typeparam name="T14">Argument #14 type.</typeparam>
		/// <typeparam name="T15">Argument #15 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <param name="argument4">Argument #4.</param>
		/// <param name="argument5">Argument #5.</param>
		/// <param name="argument6">Argument #6.</param>
		/// <param name="argument7">Argument #7.</param>
		/// <param name="argument8">Argument #8.</param>
		/// <param name="argument9">Argument #9.</param>
		/// <param name="argument10">Argument #10.</param>
		/// <param name="argument11">Argument #11.</param>
		/// <param name="argument12">Argument #12.</param>
		/// <param name="argument13">Argument #13.</param>
		/// <param name="argument14">Argument #14.</param>
		/// <param name="argument15">Argument #15.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
			this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>> expression,
			T1 argument1,
			T2 argument2,
			T3 argument3,
			T4 argument4,
			T5 argument5,
			T6 argument6,
			T7 argument7,
			T8 argument8,
			T9 argument9,
			T10 argument10,
			T11 argument11,
			T12 argument12,
			T13 argument13,
			T14 argument14,
			T15 argument15)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(
				argument1,
				argument2,
				argument3,
				argument4,
				argument5,
				argument6,
				argument7,
				argument8,
				argument9,
				argument10,
				argument11,
				argument12,
				argument13,
				argument14,
				argument15);
		}

		/// <summary>
		/// Represent expression evaluation. Intended to be used in another expression that is later transformed
		/// via <c>ExpandExpressions</c> method.
		/// If called during run-time, compiles the expression and evaluates the resulting delegate.
		/// </summary>
		/// <typeparam name="T1">Argument #1 type.</typeparam>
		/// <typeparam name="T2">Argument #2 type.</typeparam>
		/// <typeparam name="T3">Argument #3 type.</typeparam>
		/// <typeparam name="T4">Argument #4 type.</typeparam>
		/// <typeparam name="T5">Argument #5 type.</typeparam>
		/// <typeparam name="T6">Argument #6 type.</typeparam>
		/// <typeparam name="T7">Argument #7 type.</typeparam>
		/// <typeparam name="T8">Argument #8 type.</typeparam>
		/// <typeparam name="T9">Argument #9 type.</typeparam>
		/// <typeparam name="T10">Argument #10 type.</typeparam>
		/// <typeparam name="T11">Argument #11 type.</typeparam>
		/// <typeparam name="T12">Argument #12 type.</typeparam>
		/// <typeparam name="T13">Argument #13 type.</typeparam>
		/// <typeparam name="T14">Argument #14 type.</typeparam>
		/// <typeparam name="T15">Argument #15 type.</typeparam>
		/// <typeparam name="T16">Argument #16 type.</typeparam>
		/// <typeparam name="TResult">Result type.</typeparam>
		/// <param name="expression">Expression being evaluated. Cannot be null.</param>
		/// <param name="argument1">Argument #1.</param>
		/// <param name="argument2">Argument #2.</param>
		/// <param name="argument3">Argument #3.</param>
		/// <param name="argument4">Argument #4.</param>
		/// <param name="argument5">Argument #5.</param>
		/// <param name="argument6">Argument #6.</param>
		/// <param name="argument7">Argument #7.</param>
		/// <param name="argument8">Argument #8.</param>
		/// <param name="argument9">Argument #9.</param>
		/// <param name="argument10">Argument #10.</param>
		/// <param name="argument11">Argument #11.</param>
		/// <param name="argument12">Argument #12.</param>
		/// <param name="argument13">Argument #13.</param>
		/// <param name="argument14">Argument #14.</param>
		/// <param name="argument15">Argument #15.</param>
		/// <param name="argument16">Argument #16.</param>
		/// <returns>Expression result.</returns>
		public static TResult Evaluate<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
			this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>> expression,
			T1 argument1,
			T2 argument2,
			T3 argument3,
			T4 argument4,
			T5 argument5,
			T6 argument6,
			T7 argument7,
			T8 argument8,
			T9 argument9,
			T10 argument10,
			T11 argument11,
			T12 argument12,
			T13 argument13,
			T14 argument14,
			T15 argument15,
			T16 argument16)
		{
			if (expression == null)
				throw new ArgumentNullException("expression");

			return expression.Compile()(
				argument1,
				argument2,
				argument3,
				argument4,
				argument5,
				argument6,
				argument7,
				argument8,
				argument9,
				argument10,
				argument11,
				argument12,
				argument13,
				argument14,
				argument15,
				argument16);
		}
	}
}