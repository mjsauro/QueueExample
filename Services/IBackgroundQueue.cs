using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackgroundQueue.Services
{
    public interface IBackgroundQueue<T>
    {
        public void Enqueue(T item);

        T Dequeue();
    }

}
