﻿using Xunit;

namespace Leetcodecsharp.Test
{
    public class MyQueueUnitTest
    {
        [Fact]
        public void OnlyPushAndPop()
        {
            MyQueue queue = new MyQueue();
            queue.Push(1);
            int actual = queue.Pop();
            bool booleanActual = queue.Empty();

            int expected = 1;
            bool booleanExpected = true;
            Assert.Equal(expected, actual);
            Assert.Equal(booleanExpected, booleanActual);
        }
    }
}
