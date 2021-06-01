namespace Domain.MainBoundedContext.BatchModule.Aggregates.Jobs
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using Aggregates.Tasks;

    /// <summary>
    /// Represents a collection of <see cref="Tasks"/> 
    /// </summary>
    public class Tasks : ICollection<ITask>
    {
        private readonly IList<ITask> tasks = new List<ITask>();

        private readonly Job owner;

        /// <summary>
        /// Gets the number of elements contained in the <see cref="Tasks" />.
        /// </summary>
        /// <returns>
        /// The number of elements contained in the <see cref="Tasks" />.
        /// </returns>
        public int Count => tasks.Count;

        /// <summary>
        /// Gets a value indicating whether the <see cref="Tasks" /> is read-only.
        /// </summary>
        /// <returns>
        /// <see langword="true" /> if the <see cref="Tasks" /> is read-only; otherwise, <see langword="false" />.
        /// </returns>
        public bool IsReadOnly => tasks.IsReadOnly;

        /// <summary>
        /// Create a new instance of <see cref="Tasks"/> collection.
        /// </summary>
        /// <param name="owner">The <see cref="Job"/> owner of the task.</param>
        public Tasks(Job owner)
        {
            this.owner = owner ?? throw new ArgumentNullException(nameof(owner));
        }

        /// <summary>
        /// Add a <see cref="ITask"/> to the collection
        /// </summary>
        /// <param name="item"><see cref="ITask"/> element.</param>
        public void Add(ITask item)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            var task = tasks.FirstOrDefault(x => x.Identifier == item.Identifier);

            if (task is null)
            {
                if(item is Task other)
                {
                    other.Job = this.owner;
                }
                tasks.Add(item);
            }
        }

        /// <summary>
        /// Clear the collection.
        /// </summary>
        public void Clear()
        {
            tasks.Clear();
        }

        /// <summary>
        /// Verify if the collection contains a <see cref="ITask"/>.
        /// </summary>
        /// <param name="item"><see cref="ITask"/> to search.</param>
        /// <returns>
        /// <see langword="true" /> if the <see cref="Job"/> exists in the collection; otherwise, <see langword="false" />.
        /// </returns>
        public bool Contains(ITask item)
        {
            return tasks.Contains(item);
        }

        /// <summary>
        /// Copies the elements of the <see cref="ICollection{Task}" /> to an <see cref="ITask[]" />,
        /// starting at a particular <see cref="ITask[]" /> index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied
        /// from <see cref="ICollection{ITask}" />. The <see cref="ITask[]" /> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>   
        public void CopyTo(ITask[] array, int arrayIndex)
        {
            tasks.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ITask> GetEnumerator()
        {
            return tasks.GetEnumerator();
        }

        /// <summary>
        /// Remove a <see cref="ITask"/> from collection
        /// </summary>
        /// <param name="item"><see cref="ITask"/> to be eliminated.</param>
        /// <returns>
        /// <see langword="true" /> if the <see cref="ITask"/> was eliminated; otherwise, <see langword="false" />.
        /// </returns>
        public bool Remove(ITask item)
        {
            return tasks.Remove(item);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
