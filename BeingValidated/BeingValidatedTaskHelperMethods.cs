using System;
using System.Threading.Tasks;

namespace BeingValidated
{
    /// <summary>
    ///     Asynchronous extension methods for BeingValidated.
    /// </summary>
    public static class BeingValidatedTaskHelperMethods
    {
        /// <inheritdoc
        ///     cref="IBeingValidated{TInput,TOutput}.Validate(Func{TInput, bool}, Action{TInput}, Action{TInput, Exception})" />
        public static async Task<IBeingValidated<TInput, TOutput>> Validate<TInput, TOutput>(
            this Task<IBeingValidated<TInput, TOutput>> beingValidated
            , Func<TInput, bool> validation
            , Action<TInput> onFail = null
            , Action<TInput, Exception> onException = null)
        {
            IBeingValidated<TInput, TOutput> obj = await beingValidated;
            return obj.Validate(validation, onFail, onException);
        }

        /// <inheritdoc
        ///     cref="Validate{TInput,TOutput}(System.Threading.Tasks.Task{BeingValidated.IBeingValidated{TInput,TOutput}},System.Func{TInput,bool},System.Action{TInput},System.Action{TInput,System.Exception})" />
        public static async Task<IBeingValidated<TInput, TOutput>> Validate<TInput, TOutput>(
            this Task<IBeingValidated<TInput, TOutput>> beingValidated
            , Func<TInput, bool> validation
            , Action onFail = null
            , Action<Exception> onException = null)
        {
            return await beingValidated.Validate(validation, _ => onFail?.Invoke(), (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc
        ///     cref="IBeingValidated{TInput,TOutput}.Validate(Func{TInput, bool}, Action{TInput}, Action{TInput, Exception})" />
        public static async Task<IBeingValidated<TInput, TOutput>> Validate<TInput, TOutput>(
            this Task<IBeingValidated<TInput, TOutput>> beingValidated
            , Action<TInput> validation
            , Action<TInput, Exception> onException = null)
        {
            IBeingValidated<TInput, TOutput> obj = await beingValidated;
            return obj.Validate(validation, onException);
        }

        /// <inheritdoc
        ///     cref="Validate{TInput,TOutput}(System.Threading.Tasks.Task{BeingValidated.IBeingValidated{TInput,TOutput}},System.Action{TInput},System.Action{TInput,System.Exception})" />
        public static async Task<IBeingValidated<TInput, TOutput>> Validate<TInput, TOutput>(
            this Task<IBeingValidated<TInput, TOutput>> beingValidated
            , Action<TInput> validation
            , Action<Exception> onException)
        {
            return await beingValidated.Validate(validation, (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc
        ///     cref="IBeingValidated{TInput,TOutput}.ValidateAsync(Func{TInput, Task{bool}}, Action{TInput}, Action{TInput, Exception})" />
        public static async Task<IBeingValidated<TInput, TOutput>> ValidateAsync<TInput, TOutput>(
            this Task<IBeingValidated<TInput, TOutput>> beingValidated
            , Func<TInput, Task<bool>> validation
            , Action<TInput> onFail = null
            , Action<TInput, Exception> onException = null)
        {
            IBeingValidated<TInput, TOutput> obj = await beingValidated;
            return await obj.ValidateAsync(validation, onFail, onException);
        }

        /// <inheritdoc
        ///     cref="ValidateAsync{TInput,TOutput}(System.Threading.Tasks.Task{BeingValidated.IBeingValidated{TInput,TOutput}},System.Func{TInput,System.Threading.Tasks.Task{bool}},System.Action{TInput},System.Action{TInput,System.Exception})" />
        public static async Task<IBeingValidated<TInput, TOutput>> ValidateAsync<TInput, TOutput>(
            this Task<IBeingValidated<TInput, TOutput>> beingValidated
            , Func<TInput, Task<bool>> validation
            , Action onFail = null
            , Action<Exception> onException = null)
        {
            return await beingValidated.ValidateAsync(validation, _ => onFail?.Invoke(),
                (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc
        ///     cref="IBeingValidated{TInput,TOutput}.ValidateAsync(Func{TInput, Task{bool}}, Action{TInput}, Action{TInput, Exception})" />
        public static async Task<IBeingValidated<TInput, TOutput>> ValidateAsync<TInput, TOutput>(
            this Task<IBeingValidated<TInput, TOutput>> beingValidated
            , Func<TInput, Task> validation
            , Action<TInput, Exception> onException = null)
        {
            IBeingValidated<TInput, TOutput> obj = await beingValidated;
            return await obj.ValidateAsync(validation, onException);
        }

        /// <inheritdoc
        ///     cref="ValidateAsync{TInput,TOutput}(System.Threading.Tasks.Task{BeingValidated.IBeingValidated{TInput,TOutput}},System.Func{TInput,System.Threading.Tasks.Task},System.Action{TInput,System.Exception})" />
        public static async Task<IBeingValidated<TInput, TOutput>> ValidateAsync<TInput, TOutput>(
            this Task<IBeingValidated<TInput, TOutput>> beingValidated
            , Func<TInput, Task> validation
            , Action<Exception> onException)
        {
            return await beingValidated.ValidateAsync(validation, (_, e) => onException?.Invoke(e));
        }

        /// <inheritdoc cref="IBeingValidated{TInput,TOutput}.IsValid" />
        public static async Task<bool> IsValid<TInput, TOutput>(
            this Task<IBeingValidated<TInput, TOutput>> beingValidated)
        {
            IBeingValidated<TInput, TOutput> obj = await beingValidated;
            return obj.IsValid();
        }

        /// <inheritdoc cref="IBeingValidated{TInput,TOutput}.SkipIfAlreadyInvalid" />
        public static async Task<IBeingValidated<TInput, TOutput>> SkipIfAlreadyInvalid<TInput, TOutput>(
            this Task<IBeingValidated<TInput, TOutput>> beingValidated, bool setTo = true)
        {
            IBeingValidated<TInput, TOutput> obj = await beingValidated;
            return obj.SkipIfAlreadyInvalid(setTo);
        }

        /// <inheritdoc cref="IBeingValidated{TInput,TOutput}.ForceSkipIf" />
        public static async Task<IBeingValidated<TInput, TOutput>> ForceSkipIf<TInput, TOutput>(
            this Task<IBeingValidated<TInput, TOutput>> beingValidated, Predicate<TInput> predicate)
        {
            IBeingValidated<TInput, TOutput> obj = await beingValidated;
            return obj.ForceSkipIf(predicate);
        }

        /// <inheritdoc cref="IBeingValidated{TInput,TOutput}.StopForceSkipping" />
        public static async Task<IBeingValidated<TInput, TOutput>> StopForceSkipping<TInput, TOutput>(
            this Task<IBeingValidated<TInput, TOutput>> beingValidated)
        {
            IBeingValidated<TInput, TOutput> obj = await beingValidated;
            return obj.StopForceSkipping();
        }
    }
}