
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using FlaUI.Core.AutomationElements;
using FlaUI.Core.WindowsAPI;
using WeAutoCommon.Configs;
using WeAutoCommon.Models;
using WeAutoCommon.Utils;
using WeChatAuto.Components;
using WeChatAuto.Services;

namespace WeChatAuto.Utils
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// 固定尺寸的Queue,用于ocr识别的缓存中
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class FixedSizeQueue<T>
    {
        private readonly Queue<T> _queue;
        private readonly int _capacity;
        private readonly object _lock = new();

        public FixedSizeQueue(int capacity = 10)
        {
            if (capacity <= 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            _capacity = capacity;
            _queue = new Queue<T>(capacity);
        }

        /// <summary>
        /// 最大容量
        /// </summary>
        public int Capacity => _capacity;

        /// <summary>
        /// 当前元素数量
        /// </summary>
        public int Count
        {
            get
            {
                lock (_lock)
                {
                    return _queue.Count;
                }
            }
        }

        /// <summary>
        /// 添加元素。
        /// 如果超过容量，则自动删除最早进入的元素。
        /// </summary>
        public void Enqueue(T item)
        {
            lock (_lock)
            {
                if (_queue.Count >= _capacity)
                {
                    _queue.Dequeue();
                }

                _queue.Enqueue(item);
            }
        }

        /// <summary>
        /// 尝试获取并删除最早进入的元素。
        /// </summary>
        public bool TryDequeue(out T item)
        {
            lock (_lock)
            {
                return _queue.TryDequeue(out item);
            }
        }

        /// <summary>
        /// 尝试查看最早进入的元素，但不删除。
        /// </summary>
        public bool TryPeek(out T item)
        {
            lock (_lock)
            {
                return _queue.TryPeek(out item);
            }
        }

        /// <summary>
        /// 清空队列。
        /// </summary>
        public void Clear()
        {
            lock (_lock)
            {
                _queue.Clear();
            }
        }

        /// <summary>
        /// 获取当前所有元素的快照。
        /// 返回的数组与内部 Queue 无关，可以安全地在锁外使用。
        /// </summary>
        public T[] ToArray()
        {
            lock (_lock)
            {
                return _queue.ToArray();
            }
        }
    }
}