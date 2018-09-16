namespace Ustilz.Extensions
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    using JetBrains.Annotations;

    using Ustilz.Extensions.Models;

    #endregion

    /// <summary>The extensions func. </summary>
    [PublicAPI]
    public static class ExtensionsFunc
    {
        #region Méthodes publiques

        /// <summary>Exécute l'action donnée avec la valeur comme paramètre et gère toutes les exceptions pendant l'exécution.</summary>
        /// <param name="action">L'action à exécuter.</param>
        /// <param name="parameter">Paramètre de l'action, celui-ci est retourné après l'exécution dans la propriété <see cref="ExecutionResult{T}.Result" />.</param>
        /// <typeparam name="T">Le type du paramètre.</typeparam>
        /// <returns>Renvoie la valeur donnée en tant que résultat ou exception si une est survenue.</returns>
        /// <exception cref="ArgumentNullException">L'action ne peut pas être nulle.</exception>
        [NotNull]
        [Pure]
        [PublicAPI]
        public static IExecutionResult<T> ExecuteSafe<T>([NotNull] this Action<T> action, [CanBeNull] T parameter)
        {
            action.ThrowIfNull(nameof(action));

            var result = new ExecutionResult<T>();
            try
            {
                action(parameter);
                result.Result = parameter;
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }

        /// <summary>Exécute la fonction donnée avec la valeur comme paramètre et gère toutes les exceptions pendant l'exécution.</summary>
        /// <param name="func">La fonction à exécuter.</param>
        /// <param name="parameter">Le paramètre de la fonction.</param>
        /// <typeparam name="T">Le type du paramètre.</typeparam>
        /// <typeparam name="TResult">Le type du résultat.</typeparam>
        /// <returns>Renvoie le résultat de la fonction ou une exception si une est survenue.</returns>
        /// <exception cref="ArgumentNullException">La fonction ne peut pas être nulle.</exception>
        [NotNull]
        [Pure]
        [PublicAPI]
        public static IExecutionResult<TResult> ExecuteSafe<T, TResult>([NotNull] this Func<T, TResult> func, [CanBeNull] T parameter)
        {
            func.ThrowIfNull(nameof(func));

            var result = new ExecutionResult<TResult>();
            try
            {
                result.Result = func(parameter);
            }
            catch (Exception ex)
            {
                result.Exception = ex;
            }

            return result;
        }

        /// <summary>Méthode de mémoïsation d'une fonction. </summary>
        /// <param name="func">Fonction à mémoïser. </param>
        /// <typeparam name="T">Type du paramètres d'entrée. </typeparam>
        /// <typeparam name="TResult">Type du retour. </typeparam>
        /// <returns>Retourne une fonction <see cref="Func{T,TResult}" />. </returns>
        [NotNull]
        public static Func<T, TResult> Memoize<T, TResult>(this Func<T, TResult> func)
        {
            var t = new Dictionary<T, TResult>();
            return n =>
            {
                if (t.ContainsKey(n))
                {
                    return t[n];
                }

                var result = func(n);
                t.Add(n, result);
                return result;
            };
        }

        /// <summary>Méthode de mémoïsation d'une fonction. </summary>
        /// <param name="func">Fonction à mémoïser. </param>
        /// <typeparam name="TResult">Type du retour. </typeparam>
        /// <returns>Retourne une fonction <see cref="Func{T,TResult}" />. </returns>
        [NotNull]
        public static Func<TResult> Memoize<TResult>(this Func<TResult> func)
        {
            var t = new Dictionary<string, TResult>();
            return () =>
            {
                if (t.ContainsKey(func.GetMethodInfo().Name))
                {
                    return t[func.GetMethodInfo().Name];
                }

                var result = func();
                t.Add(func.GetMethodInfo().Name, result);
                return result;
            };
        }

        /// <summary>Méthode de test de performance.</summary>
        /// <param name="function">La fonction à exécuter.</param>
        /// <param name="timestamp">Retourne le temps d'exécution de la méthode en millisecondes.</param>
        /// <typeparam name="TResult">Le type du résultat. </typeparam>
        /// <returns>La valeur de retour.</returns>
        public static TResult TestPerf<TResult>([NotNull] this Func<TResult> function, out long timestamp)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var result = function.Invoke();

            stopWatch.Stop();
            timestamp = stopWatch.ElapsedMilliseconds;
            return result;
        }

        /// <summary>Méthode de test de performance. </summary>
        /// <param name="function">La fonction à exécuter.</param>
        /// <param name="timestamp">Retourne le temps d'exécution de la méthode en millisecondes.</param>
        /// <param name="param">Le paramètre de la fonction. </param>
        /// <typeparam name="T">Le type du paramètre. </typeparam>
        /// <typeparam name="TResult">Le type du retour de la fonction.</typeparam>
        /// <returns>La valeur de retour.</returns>
        public static TResult TestPerf<T, TResult>([NotNull] this Func<T, TResult> function, out long timestamp, T param)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var result = function.Invoke(param);

            stopWatch.Stop();
            timestamp = stopWatch.ElapsedMilliseconds;
            return result;
        }

        /// <summary>Méthode de test de performance. </summary>
        /// <param name="function">La fonction à exécuter.</param>
        /// <param name="timestamp">Retourne le temps d'exécution de la méthode en millisecondes.</param>
        /// <param name="param1">Le premier paramètre de la fonction.</param>
        /// <param name="param2">Le second paramètre de la fonction.</param>
        /// <typeparam name="T1">Le type du premier paramètre. </typeparam>
        /// <typeparam name="T2">Le type du second paramètre. </typeparam>
        /// <typeparam name="TResult">Le type du retour de la fonction.</typeparam>
        /// <returns>La valeur de retour.</returns>
        public static TResult TestPerf<T1, T2, TResult>([NotNull] this Func<T1, T2, TResult> function, out long timestamp, T1 param1, T2 param2)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var result = function.Invoke(param1, param2);

            stopWatch.Stop();
            timestamp = stopWatch.ElapsedMilliseconds;
            return result;
        }

        /// <summary>Méthode de test de performance. </summary>
        /// <param name="function">La fonction à exécuter.</param>
        /// <param name="timestamp">Retourne le temps d'exécution de la méthode en millisecondes.</param>
        /// <param name="param1">Le premier paramètre de la fonction.</param>
        /// <param name="param2">Le second paramètre de la fonction.</param>
        /// <param name="param3">Le troisième paramètre de la fonction.</param>
        /// <typeparam name="T1">Le type du premier paramètre. </typeparam>
        /// <typeparam name="T2">Le type du second paramètre. </typeparam>
        /// <typeparam name="T3">Le type du troisième paramètre.</typeparam>
        /// <typeparam name="TResult">Le type du retour de la fonction.</typeparam>
        /// <returns>La valeur de retour.</returns>
        public static TResult TestPerf<T1, T2, T3, TResult>([NotNull] this Func<T1, T2, T3, TResult> function, out long timestamp, T1 param1, T2 param2, T3 param3)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var result = function.Invoke(param1, param2, param3);

            stopWatch.Stop();
            timestamp = stopWatch.ElapsedMilliseconds;
            return result;
        }

        /// <summary>Méthode de test de performance. </summary>
        /// <param name="function">La fonction à exécuter.</param>
        /// <param name="timestamp">Retourne le temps d'exécution de la méthode en millisecondes.</param>
        /// <param name="param1">Le premier paramètre de la fonction.</param>
        /// <param name="param2">Le second paramètre de la fonction.</param>
        /// <param name="param3">Le troisième paramètre de la fonction.</param>
        /// <param name="param4">Le quatrième paramètre de la fonction.</param>
        /// <typeparam name="T1">Le type du premier paramètre. </typeparam>
        /// <typeparam name="T2">Le type du second paramètre. </typeparam>
        /// <typeparam name="T3">Le type du troisième paramètre.</typeparam>
        /// <typeparam name="T4">Le type du quatrième paramètre.</typeparam>
        /// <typeparam name="TResult">Le type du retour de la fonction.</typeparam>
        /// <returns>La valeur de retour.</returns>
        public static TResult TestPerf<T1, T2, T3, T4, TResult>(
            [NotNull] this Func<T1, T2, T3, T4, TResult> function,
            out long timestamp,
            T1 param1,
            T2 param2,
            T3 param3,
            T4 param4)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            var result = function.Invoke(param1, param2, param3, param4);

            stopWatch.Stop();
            timestamp = stopWatch.ElapsedMilliseconds;
            return result;
        }

        /// <summary>Méthode de test de performance. </summary>
        /// <param name="action">L'action à exécuter. </param>
        /// <param name="param">Le paramètre de l'action. </param>
        /// <typeparam name="T">Le type du paramètre. </typeparam>
        /// <returns>Retourne le temps d'exécution de la méthode en millisecondes.</returns>
        public static long TestPerf<T>([NotNull] this Action<T> action, T param)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            action.Invoke(param);

            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }

        /// <summary>Méthode de test de performance. </summary>
        /// <param name="action">L'action à exécuter. </param>
        /// <returns>Retourne le temps d'exécution de la méthode en millisecondes.</returns>
        public static long TestPerf([NotNull] this Action action)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();

            action.Invoke();

            stopWatch.Stop();
            return stopWatch.ElapsedMilliseconds;
        }

        #endregion
    }
}
