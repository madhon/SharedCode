using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Madhon
{

    /// <summary>
    /// A thread-safe lazy value.
    /// </summary>
    /// <typeparam name="T">The type of the value to be lazily evaluated.</typeparam>
    /// <remarks>
    /// This implements IOptional&lt;T&gt; since it is temporally optional. In other words, at any given
    /// time it may or may not have a value.
    /// 
    /// In general, lazy values computed using side-effecting functions are very difficult to reason about.
    /// </remarks>
    [Serializable]
    public sealed class Lazy<T> : IOptional<T>
    {
        T value;
        Func<T> thunk;

        // some frameworks require parameterless constructors to be available, like NHibernate.
        Lazy()
        {
        }

        /// <summary>
        /// Return a lazily value computed by invoked thunk().
        /// </summary>
        /// <param name="thunk">The function used to compute the value when required.</param>
        public Lazy(Func<T> thunk)
        {
            this.thunk = thunk;
        }

        /// <summary>
        /// Return a resolved lazy value.
        /// </summary>
        /// <param name="v">The value to encapsulate in a lazy type.</param>
        public Lazy(T v)
        {
            value = v;
        }

        /// <summary>
        /// Force evaluation of the value.
        /// </summary>
        /// <returns>Returns the computed value.</returns>
        public T Value
        {
            get { return HasValue ? value : Eval(); }
        }
        /// <summary>
        /// Returns true if the value has been computed.
        /// </summary>
        /// <remarks>
        /// This proeprty is thread-safe.
        /// 
        /// Accessing this property does not force the lazy computation.
        /// </remarks>
        public bool HasValue
        {
            get { return thunk == null; }
        }
        /// <summary>
        /// Attempts to extract the value.
        /// </summary>
        /// <param name="value">The lazy value to extract.</param>
        /// <returns>Returns true if the lazy value was already forced, false otherwise.</returns>
        /// <remarks>
        /// This method is thread-safe.
        /// 
        /// Calling this method does not force the lazy computation.
        /// </remarks>
        public bool TryGetValue(out T value)
        {
            var forced = HasValue;
            value = forced ? this.value : default(T);
            return forced;
        }

        /// <summary>
        /// Evaluate the thunk.
        /// </summary>
        /// <returns>The value returned from the thunk.</returns>
        T Eval()
        {
            // thread-safe lazy evaluation
            //FIXME: this is probably not ideal, since clients may deadlock on 'this'
            lock (this)
            {
                if (this.thunk != null)
                {
                    value = this.thunk();
                    this.thunk = null; // free closure for GC
                }
            }
            return value;
        }

        /// <summary>
        /// Implicitly construct a lazy value from a thunk.
        /// </summary>
        /// <param name="t">The thunk used to compute the lazy value.</param>
        /// <returns>A lazily computed value.</returns>
        public static implicit operator Lazy<T>(Func<T> t)
        {
            return new Lazy<T>(t);
        }

        /// <summary>
        /// Implicitly convert a value to an initialized lazy value.
        /// </summary>
        /// <param name="t">The value to wrap.</param>
        /// <returns>The lazily computed value.</returns>
        public static implicit operator Lazy<T>(T t)
        {
            return new Lazy<T>(t);
        }
    }

    /// <summary>
    /// Convenience methods for lazy values.
    /// </summary>
    public static class Lazy
    {
        /// <summary>
        /// Construct a new lazy value.
        /// </summary>
        /// <typeparam name="T">The type of the lazy value.</typeparam>
        /// <param name="make">The function constructing the lazy value.</param>
        /// <returns>A new lazily initialized value.</returns>
        public static Lazy<T> Create<T>(Func<T> make)
        {
            return make;
        }
    }
}
