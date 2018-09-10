using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solutions
{
    /// <summary>
    /// Definition for a binary tree node.
    /// </summary>
    public class TreeNode
    {
        public int val;
        public TreeNode left;
        public TreeNode right;
        public TreeNode(int x) => val = x;
    }

    /// <summary>
    /// Definition for singly-linked list.
    /// </summary>
    public class ListNode
    {
        public int val;
        public ListNode next;

        public ListNode(int x) => val = x;
    }

    /// <summary>
    /// Dictionary Tree class
    /// </summary>
    public class Trie
    {
        TrieNode Root { get; set; }
        public Trie()
        {
            Root = new TrieNode();
        }

        public void Insert(string word)
        {
            TrieNode node = Root;
            for (int i = 0; i < word.Length; i++)
            {
                char currentChar = word[i];
                if (!node.ContainsKey(currentChar))
                {
                    node.Put(currentChar, new TrieNode());
                }
                node = node.Get(currentChar);
            }
            node.IsEnd = true;
        }


        public bool Search(string word)
        {
            TrieNode node = SearchPrefix(word);
            return node != null && node.IsEnd;
        }
        private TrieNode SearchPrefix(string word)
        {
            TrieNode node = Root;
            for (int i = 0; i < word.Length; i++)
            {
                char currLetter = word[i];
                if (node.ContainsKey(currLetter))
                    node = node.Get(currLetter);
                else
                    return null;
            }
            return node;
        }

        public bool StartWith(string prefix)
        {
            TrieNode node = SearchPrefix(prefix);
            return node != null;
        }
    }

    /// <summary>
    /// Trie Node
    /// </summary>
    public class TrieNode
    {
        public string Word { get; set; }
        public TrieNode[] Links;

        // a-z lowercase
        private static readonly int R = 26;

        public bool IsEnd { get; set; }

        public TrieNode()
        {
            Links = new TrieNode[R];
        }

        public bool ContainsKey(char ch)
        {
            return Links[ch - 'a'] != null;
        }

        public TrieNode Get(char ch)
        {
            return Links[ch - 'a'];
        }

        public void Put(char ch, TrieNode node)
        {
            Links[ch - 'a'] = node;
        }
    }

    public class Codec
    {
        private static readonly char spliter = ',';
        private static readonly string NN = "X";
        /// <summary>
        /// Encode a tree to single string.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public string Serialize(TreeNode root)
        {
            StringBuilder sb = new StringBuilder();
            BuildString(root, sb);
            return sb.ToString();
        }
        private void BuildString(TreeNode node, StringBuilder sb)
        {
            if (node == null)
                sb.Append(NN).Append(spliter);
            else
            {
                sb.Append(node.val).Append(spliter);
                BuildString(node.left, sb);
                BuildString(node.right, sb);
            }
        }

        public TreeNode Deserialize(string data)
        {
            Queue<string> nodes = new Queue<string>();
            foreach (string item in data.Split(spliter))
                nodes.Enqueue(item);
            return BuildeTree(nodes);
        }
        private TreeNode BuildeTree(Queue<string> nodes)
        {
            string val = nodes.Dequeue();
            if (val.Equals(NN)) return null;
            else
            {
                TreeNode node = new TreeNode(Convert.ToInt32(val))
                {
                    left = BuildeTree(nodes),
                    right = BuildeTree(nodes)
                };
                return node;
            }
        }
    }

    /// <summary>
    /// Leetcode Solution Class
    /// </summary>
    public class Solution
    {
        /// <summary>
        /// 1. Two Sum
        /// <para>Description: </para>
        /// <para>Given an array of integers, return indices of the two numbers such that they
        /// add up to a specific target.</para>
        /// <para>You may assume that each input would have exactly
        /// one solution, and you may not use the same element twice.</para>
        /// </summary>
        /// <param name="nums">Given array</param>
        /// <param name="target">Target number</param>
        /// <returns>Aarry of result</returns>
        public static int[] TwoSum(int[] nums, int target)
        {
            int[] res = new int[2];
            Dictionary<int, int> dic = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                if (dic.ContainsKey(target - nums[i]))
                {
                    res[1] = i;
                    res[0] = dic[target - nums[i]];
                    break;
                }
                if (!dic.ContainsKey(nums[i]))
                    dic.Add(nums[i], i);
            }
            return res;
        }

        /// <summary>
        /// 2. Add Two Numbers
        /// <para>Description: </para>
        /// <para>You are given two non-empty linked lists representing two non-negative
        /// integers.The digits are stored in reverse order and each of their nodes
        /// contain a single digit.Add the two numbers and return it as a linked list.</para>
        /// You may assume the two numbers do not contain any leading zero, except the     
        /// number 0 itself.
        /// </summary>
        /// <param name="l1"></param>
        /// <param name="l2"></param>
        /// <returns></returns>
        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            // 116ms
            ListNode dummyHead = new ListNode(0);
            if (l1 == null && l2 == null)
                return dummyHead;
            int carry = 0;
            ListNode curr = dummyHead;
            while (l1 != null || l2 != null)
            {
                int num1 = l1?.val ?? 0;
                int num2 = l2?.val ?? 0;
                int sum = num1 + num2 + carry;
                curr.next = new ListNode(sum % 10);
                curr = curr.next;
                carry = sum / 10;
                l1 = l1?.next;
                l2 = l2?.next;
            }
            if (carry != 0)
                curr.next = new ListNode(carry);
            return dummyHead.next;

            // 148ms
            // ListNode r = new ListNode(-1);
            // ListNode n = r;
            // int carry = 0;
            // while (carry > 0 || l1 != null || l2 != null)
            // {
            //     int v = (l1?.val ?? 0) + (l2?.val ?? 0) + carry;
            //     carry = v / 10;
            //     n = n.next = new ListNode(v % 10);
            //     l1 = l1?.next;
            //     l2 = l2?.next;
            // }
            // return r.next;
        }

        /// <summary>
        /// 3. Longest Substring Without Repeating Characters
        /// Given a string, find the length of the longest substring without repeating characters.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring(string s)
        {
            // testcase: "abcabcbb" -> "abc" return 3.
            // testcase: "bbbbb" -> "b" return 1.
            // testcase: "pwwkew" -> "wke" return 3. Note: "pwke" is a subsequence and not a substring.

            // 96 ms int[]
            // int n = s.Length, ans = 0;
            // int[] index = new int[128];
            // for (int j = 0, i = 0; j < n; j++)
            // {
            //     i = Math.Max(index[s[j]], i);
            //     ans = Math.Max(ans, j - i + 1);
            //     index[s[j]] = j + 1;
            // }
            // return ans;

            // 100 ms HashSet<char>
            // int len = s.Length;
            // HashSet<char> set = new HashSet<char>();
            // int ans = 0, i = 0, j = 0;
            // while (i < len && j < len)
            // {
            //     if (!set.Contains(s[j]))
            //     {
            //         set.Add(s[j++]);
            //         ans = Math.Max(ans, j - i);
            //     }
            //     else set.Remove(s[i++]);
            // }
            // return ans;

            // 100ms Dictionary<char, int>
            int n = s.Length, ans = 0;
            Dictionary<char, int> dic = new Dictionary<char, int>();
            for (int j = 0, i = 0; j < n; j++)
            {
                if (dic.ContainsKey(s[j]))
                {
                    i = Math.Max(dic[s[j]], i);
                    dic[s[j]] = j + 1;
                }
                else dic.Add(s[j], j + 1);
                ans = Math.Max(ans, j - i + 1);
            }
            return ans;
        }

        /// <summary>
        /// 4. Median of Two Sorted Arrays
        /// <para>There are two sorted arrays nums1 and nums2 of size m and n respectively.
        /// Find the median of the two sorted arrays.The overall run time complexity
        /// should be O(log (m+n)). </para>You may assume nums1 and nums2 cannot be both empty.
        /// </summary>
        /// <param name="A">First array</param>
        /// <param name="B">Second array</param>
        /// <returns>the median number</returns>
        public static double FindMedianSortedArrays(int[] A, int[] B)
        {
            int m = A.Length, n = B.Length;
            if (m > n)
            {
                int[] temp = A;
                A = B;
                B = temp;
                int tmp = m;
                m = n;
                n = tmp;
            }
            // use binary search
            int iMin = 0, iMax = m, halfLen = (m + n + 1) / 2;
            while (iMin <= iMax)
            {
                int i = (iMin + iMax) / 2;
                int j = halfLen - i;
                if (i < iMax && B[j - 1] > A[i])
                    iMin = iMin + 1;
                else if (i > iMin && A[i - 1] > B[j])
                    iMax = iMax - 1;
                else
                {
                    int maxLeft = 0;
                    if (i == 0) { maxLeft = B[j - 1]; }
                    else if (j == 0) { maxLeft = A[i - 1]; }
                    else { maxLeft = Math.Max(A[i - 1], B[j - 1]); }
                    if ((m + n) % 2 == 1) { return maxLeft; }

                    int minRight = 0;
                    if (i == m) { minRight = B[j]; }
                    else if (j == n) { minRight = A[i]; }
                    else { minRight = Math.Min(B[j], A[i]); }

                    return (maxLeft + minRight) / 2.0;
                }
            }
            return 0.0;
        }

        /// <summary>
        /// <para>5. Longest Palindromic Substring</para>
        /// <para>Given a string s, find the longest palindromic substring in s.</para>
        /// <para>You may assume that the maximum length of s is 1000.</para>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string LongestPalindrome(string s)
        {
            string T = PreProcess(s);
            int n = T.Length;
            int[] P = new int[n];
            int C = 0, R = 0;
            for (int i = 1; i < n - 1; i++)
            {
                int i_mirror = 2 * C - i; //equals to i'= C - (i-c)
                P[i] = (R > 1) ? Math.Min(R - i, P[i_mirror]) : 0;
                // Attempt to expand palindrome centered at i
                while (T[i + 1 + P[i]] == T[i - 1 - P[i]])
                {
                    P[i]++;
                }
                // If palindrome centered at i expand past R,
                // adjust center based on expended palindrome.
                if (i + P[i] > R)
                {
                    C = i;
                    R = i + P[i];
                }
            }
            // Find the maximum element in P
            int maxLen = 0;
            int centerIndex = 0;
            for (int i = 1; i < n - 1; i++)
            {
                if (P[i] > maxLen)
                {
                    maxLen = P[i];
                    centerIndex = i;
                }
            }
            return s.Substring((centerIndex - 1 - maxLen) / 2, maxLen);
        }

        /// <summary>
        /// <para>11. Container With Most Water</para>
        /// <para>
        /// Given n non-negative integers a1, a2, ..., an , where each represents a point
        /// at coordinate(i, ai).</para>
        /// <para>n vertical lines are drawn such that the two endpoints of line i is at(i, ai) and(i, 0).</para>
        /// <para>Find two lines, which together with x-axis forms a container, such that the container contains the most water.</para>
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int MaxArea(int[] height)
        {
            int maxArea = 0;
            int left = 0, right = height.Length - 1;
            while (left < right)
            {
                maxArea = Math.Max(maxArea, Math.Min(height[left], height[right]) * (right - left));
                if (height[left] < height[right]) left++;
                else right--;
            }
            return maxArea;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            Array.Sort(nums);
            List<IList<int>> res = new List<IList<int>>();
            //for (int i = 0; i < nums.Length - 2; i++)
            //{
            //    if (i == 0 || (i > 0 && nums[i] != nums[i - 1]))
            //    {
            //        int lo = i + 1, hi = nums.Length - 1, sum = 0 - nums[i];
            //        // two sum
            //        while (lo < hi)
            //        {
            //            if (nums[lo] + nums[hi] == sum)
            //            {
            //                res.Add(
            //                    new List<int>
            //                    {
            //                        nums[i],
            //                        nums[lo],
            //                        nums[hi]
            //                    });

            //                while (lo < hi && nums[lo] == nums[lo + 1]) lo++;
            //                while (lo < hi && nums[hi] == nums[hi - 1]) hi--;
            //                lo++;
            //                hi--;
            //            }
            //            else if (nums[lo] + nums[hi] < sum)
            //            {
            //                while (lo < hi && nums[lo] == nums[lo + 1]) lo++;
            //                lo++;
            //            }
            //            else
            //            {
            //                while (lo < hi && nums[hi] == nums[hi - 1]) hi--;
            //                hi--;
            //            }
            //        }
            //    }
            //}

            int len = nums.Length;
            for (int i = 0; i < len; i++)
            {
                int target = -nums[i];
                int left = i + 1;
                int right = len - 1;
                if (target < 0) break;
                while (left<right)
                {
                    int sum = nums[left] + nums[right];
                    if (sum < target) left++;
                    else if (sum > target) right--;
                    else
                    {
                        List<int> tmp = new List<int> { nums[i], nums[left], nums[right] };
                        res.Add(tmp);
                        while (left < right && nums[left] < tmp[1]) left++;
                        while (left < right && nums[right] == tmp[2]) right--;
                    }
                }
                while (i + 1 < len && nums[i + 1] == nums[i]) i++;
            }
            return res;
        }

        public static bool ContainsDuplicate(int[] nums)
        {
            Array.IndexOf(nums, 1);
            Dictionary<int, int> dic = new Dictionary<int, int>();
            foreach (int item in nums)
            {
                if (dic.ContainsKey(item))
                    return true;
                dic.Add(item, 1);
            }
            return false;
        }

        public static void Rotate(int[] nums, int k)
        {
            k %= nums.Length;
            Reverse(nums, 0, nums.Length - 1);
            Reverse(nums, 0, k - 1);
            Reverse(nums, k, nums.Length - 1);
        }
        private static void Reverse(int[] nums, int start, int end)
        {
            while (start < end)
            {
                int tmp = nums[start];
                nums[start] = nums[end];
                nums[end] = tmp;
                start++;
                end--;
            }
        }

        public static ListNode DeleteDuplicates(ListNode head)
        {
            ListNode current = head;
            while (current != null && current.next != null)
            {
                if (current.val == current.next.val) current.next = current.next.next;
                else current = current.next;
            }
            return head;
        }

        public static int RemoveDuplicates(int[] nums)
        {
            int i = nums.Length > 0 ? 1 : 0;
            foreach (int n in nums)
                if (n > nums[i - 1]) nums[i++] = n;
            return i;
        }

        public static bool IsAnagram(string s, string t)
        {
            if (s.Length != t.Length) return false;
            int[] tables = new int[26];
            foreach (char c in s)
                tables[c - 'a']++;
            foreach (char c in t)
                tables[c - 'a']--;
            return tables.All(e => e == 0);
        }

        public static bool IsMonotonic(int[] A)
        {
            int store = 0;
            for (int i = 0; i < A.Length - 1; i++)
            {
                int c = A[i].CompareTo(A[i + 1]);
                if (c != 0)
                {
                    if (c != store && store != 0)
                        return false;
                    store = c;
                }
            }
            return true;
        }

        /// <summary>
        /// Groups of Special-Equivalent Strings
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public int NumSpecialEquivGroups(string[] A)
        {
            HashSet<string> seen = new HashSet<string>();
            foreach (string s in A)
            {
                int[] count = new int[52];
                for (int i = 0; i < s.Length; i++)
                    count[s[i] - 'a' + 26 * (i % 2)]++;
                seen.Add(string.Join(",", count));
            }
            return seen.Count;
        }

        /// <summary>
        /// Surface Area
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int SurfaceArea(int[][] grid)
        {
            int res = 0, n = grid.Length;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (grid[i][j] > 0) res += grid[i][j] * 4 + 2;
                    if (i > 0) res -= Math.Min(grid[i][j], grid[i - 1][j]) * 2;
                    if (j > 0) res -= Math.Min(grid[i][j], grid[i][j - 1]) * 2;
                }
            }
            return res;
        }

        /// <summary>
        /// Missing Number
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int MissingNumvber(int[] nums)
        {
            int len = nums.Length;
            int sum = len * (len + 1) / 2;
            int actual = nums.Sum();
            return sum - actual;
        }

        /// <summary>
        /// Find Duplicate
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int FindDuplicate(int[] nums)
        {
            int slow = nums[0];
            int fast = nums[0];
            do
            {
                slow = nums[slow];
                fast = nums[nums[fast]];
            } while (slow != fast);
            int ptr1 = nums[0];
            int ptr2 = slow;
            while (ptr1 != ptr2)
            {
                ptr1 = nums[ptr1];
                ptr2 = nums[ptr2];
            }
            return ptr1;
        }

        /// <summary>
        /// 3D Projection Area 
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int ProjectionArea(int[][] grid)
        {
            int ans = 0;
            for (int x = 0; x < grid.Length; x++)
            {
                int row = 0;
                int column = 0;
                for (int y = 0; y < grid[x].Length; y++)
                {
                    if (grid[x][y] != 0) ans++;
                    row = Math.Max(row, grid[x][y]);
                    column = Math.Max(column, grid[y][x]);
                }
                ans += row + column;
            }
            return ans;
        }

        public static int[] ShortestToChar(string S, char C)
        {
            int n = S.Length;
            int[] ans = new int[n];
            int prev = Int32.MinValue / 2;
            for (int i = 0; i < n; i++)
            {
                if (S[i] == C) prev = i;
                ans[i] = i - prev;
            }
            prev = Int32.MaxValue / 2;
            for (int i = n - 1; i >= 0; i--)
            {
                if (S[i] == C) prev = i;
                ans[i] = Math.Min(ans[i], prev - i);
            }
            return ans;
        }
        public static int PeakIndexInMountainArray(int[] A)
        {
            int left = 0, right = A.Length - 1;
            while (left < right)
            {
                int mid = (left + right) / 2;
                if (A[mid] < A[mid + 1])
                    left = mid + 1;
                else
                    right = mid;
            }
            return left;
        }

        /// <summary>
        /// Get the middle node of head
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode MiddleNode(ListNode head)
        {
            ListNode slow = head;
            ListNode fast = head;
            while (fast != null && fast.next != null)
            {
                slow = slow.next;
                fast = fast.next.next;
            }
            return slow;
        }

        /// <summary>
        /// Uncommon words from two sentences.
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static string[] UncommonFromSentence(string A, string B)
        {
            List<string> res = new List<string>();
            Dictionary<string, int> dic = new Dictionary<string, int>();
            foreach (var word in A.Split(' '))
            {
                if (dic.ContainsKey(word))
                    dic[word]++;
                else
                    dic[word] = 1;
            }
            foreach (var word in B.Split(' '))
            {
                if (dic.ContainsKey(word))
                    dic[word]++;
                else
                    dic[word] = 1;
            }
            foreach (var word in dic)
            {
                if (word.Value == 1)
                {
                    res.Add(word.Key);
                }
            }
            return res.ToArray();
        }

        /// <summary>
        /// Max Profit
        /// </summary>
        /// <param name="prices"></param>
        /// <returns></returns>
        public static int MaxProfit(int[] prices)
        {
            // 108 ms
            //int total = 0;
            //for (int i = 1; i < prices.Length; i++)
            //    if (prices[i] > prices[i - 1])
            //        total += prices[i] - prices[i - 1];
            //return total;
            int len = prices.Length;
            if (prices == null || len < 1) return 0;
            int[] full = new int[len];
            int[] empty = new int[len];
            empty[0] = 0;
            full[0] = prices[0] * -1;
            for (int i = 1; i < len; i++)
            {
                empty[i] = Math.Max(empty[i - 1], full[i - 1] + prices[i]);
                full[i] = Math.Max(full[i - 1], empty[i - 1] - prices[i]);
            }
            return Math.Max(full[len - 1], empty[len - 1]);
        }

        /// <summary>
        /// Count Primes
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int CountPrimes(int n)
        {
            if (n < 3) return 0;
            bool[] f = new bool[n];
            int count = n / 2;
            for (int i = 3; i * i < n; i += 2)
            {
                if (f[i]) continue;
                for (int j = i * i; j < n; j += 2 * i)
                {
                    if (!f[j])
                    {
                        --count;
                        f[j] = true;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// ReverseBits
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static uint ReverseBits(uint n)
        {
            n = (n >> 16) | (n << 16);
            n = ((n & 0xff00ff00) >> 8) | ((n & 0x00ff00ff) << 8);
            n = ((n & 0xf0f0f0f0) >> 4) | ((n & 0x0f0f0f0f) << 4);
            n = ((n & 0xcccccccc) >> 2) | ((n & 0x33333333) << 2);
            n = ((n & 0xaaaaaaaa) >> 1) | ((n & 0x55555555) << 1);
            return n;
            //if (n == 0) return 0;
            //uint res = 0;
            //for (int i = 0; i < 32; i++)
            //{
            //    res <<= 1;
            //    if ((n & 1) == 1) res++;
            //    n >>= 1;
            //}
            //return res;
        }

        /// <summary>
        /// Pascal's triangle
        /// </summary>
        /// <param name="numRows"></param>
        /// <returns></returns>
        public static IList<IList<int>> Generate(int numRows)
        {
            IList<IList<int>> triangle = new List<IList<int>>();
            //if (numRows == 0) return triangle;
            //triangle.Add(new List<int>());
            //triangle[0].Add(1);
            //for(int rowNum = 1; rowNum < numRows; rowNum++)
            //{
            //    List<int> row = new List<int>();
            //    IList<int> prevRow = triangle[rowNum - 1];
            //    row.Add(1);
            //    for (int j = 1; j < rowNum; j++) row.Add(prevRow[j - 1] + prevRow[j]);
            //    row.Add(1);
            //    triangle.Add(row);
            //}
            for (int i = 0; i < numRows; ++i)
            {
                IList<int> row = new List<int>();
                for (int r = 1; r <= i + 1; r++) row.Add(1);
                triangle.Add(row);
                for (int j = 1; j < i; ++j) triangle[i][j] = triangle[i - 1][j - 1] + triangle[i - 1][j];
            }
            return triangle;
        }

        /// <summary>
        /// Sorted Array To BST
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static TreeNode SortedArrayToBST(int[] nums)
        {
            //if (nums == null || nums.Length == 0) return null;
            //int mid = nums.Length / 2;
            //TreeNode root = new TreeNode(nums[mid]);
            //root.left = SubProcess(nums.Where((num, index) => index < mid).ToArray());
            //root.right = SubProcess(nums.Where((num, index) => index > mid).ToArray());
            //return root;
            return SubProcess(nums, 0, nums.Length - 1);
        }
        private static TreeNode SubProcess(int[] nums, int start, int end)
        {
            if (start > end) return null;
            int mid = start + (end - start) / 2;
            TreeNode root = new TreeNode(nums[mid])
            {
                left = SubProcess(nums, start, mid - 1),
                right = SubProcess(nums, mid + 1, end)
            };
            return root;
        }

        /// <summary>
        /// Is Symmetric
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static bool IsSymmetric(TreeNode root)
        {
            // 116ms by recursion
            // return IsMirror(root, root);

            // 104ms by iteration
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                TreeNode t1 = queue.Dequeue();
                TreeNode t2 = queue.Dequeue();
                if (t1 == null & t2 == null) continue;
                if (t1 == null || t2 == null) return false;
                if (t1.val != t2.val) return false;
                queue.Enqueue(t1.left);
                queue.Enqueue(t2.right);
                queue.Enqueue(t1.right);
                queue.Enqueue(t2.left);
            }
            return true;
        }
        private static bool IsMirror(TreeNode l1, TreeNode l2)
        {
            if (l1 == null && l2 == null) return true;
            if (l1 == null || l2 == null) return false;
            return (l1.val == l2.val)
                && IsMirror(l1.left, l2.right)
                && IsMirror(l1.right, l2.left);
        }

        /// <summary>
        /// Climb Stairs
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int ClimbStairs(int n)
        {
            // 44ms
            //if (n == 1) return 1;
            //int[] dp = new int[n + 1];
            //dp[1] = 1;
            //dp[2] = 2;
            //for (int i = 3; i <= n; i++) dp[i] = dp[i - 1] + dp[i - 2];
            //return dp[n];

            // 56ms
            int a = 1, b = 1;
            while (n-- > 0)
                a = (b += a) - a;
            return a;
        }

        /// <summary>
        /// Add Binary
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string AddBinary(string a, string b)
        {
            string s = "";
            int c = 0, i = a.Length - 1, j = b.Length - 1;
            while (i >= 0 || j >= 0 || c == 1)
            {
                c += i >= 0 ? a[i--] - '0' : 0;
                c += j >= 0 ? b[j--] - '0' : 0;
                s = System.Convert.ToChar(c % 2 + '0') + s;
                c /= 2;
            }
            return s;
        }

        /// <summary>
        /// HasCycle
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static bool HasCycle(ListNode head)
        {
            if (head == null || head.next == null) return false;
            ListNode slow = head;
            ListNode fast = head.next;
            while (slow != fast)
            {
                if (fast == null || fast.next == null) return false;
                slow = slow.next;
                fast = fast.next.next;
            }
            return true;
        }

        /// <summary>
        /// Max Depth
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MaxDepth(TreeNode root)
        {
            // DFS 112ms
            // return root == null ? 0 : 1 + Math.Max(MaxDepth(root.left), MaxDepth(root.right));
            // BFS 112ms
            if (root == null) return 0;
            int res = 0;
            Queue<TreeNode> queue = new Queue<TreeNode>();
            queue.Enqueue(root);
            while (queue.Count > 0)
            {
                res++;
                for (int i = 0, n = queue.Count; i < n; i++)
                {
                    TreeNode p = queue.Peek();
                    queue.Dequeue();
                    if (p.left != null) queue.Enqueue(p.left);
                    if (p.right != null) queue.Enqueue(p.right);
                }
            }
            return res;
        }

        /// <summary>
        /// MinDepth
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int MinDepth(TreeNode root)
        {
            if (root == null) return 0;
            int l = MinDepth(root.left), r = MinDepth(root.right);
            return 1 + (l < r && l > 0 || r < 1 ? l : r);
        }

        /// <summary>
        /// Has Path Sum
        /// </summary>
        /// <param name="root"></param>
        /// <param name="sum"></param>
        /// <returns></returns>
        public static bool HasPathSum(TreeNode root, int sum)
        {
            if (root == null) return false;
            if (root.val == sum && root.left == null && root.right == null) return true;
            return HasPathSum(root.left, sum - root.val) || HasPathSum(root.right, sum - root.val);
        }

        /// <summary>
        /// IsBalanced
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static bool IsBalanced(TreeNode root)
        {
            return DFSHeight(root) != -1;
        }
        private static int DFSHeight(TreeNode root)
        {
            if (root == null) return 0;
            int leftHeight = DFSHeight(root.left);
            // when left node is null
            if (leftHeight == -1) return -1;
            int rightHeight = DFSHeight(root.right);
            // when right node is null
            if (rightHeight == -1) return -1;
            // balanced binary tree defination
            if (Math.Abs(leftHeight - rightHeight) > 1) return -1;
            return Math.Max(leftHeight, rightHeight) + 1;
        }

        // TODO: fix max/1 bug
        /// <summary>
        /// Divide
        /// </summary>
        /// <param name="dividend"></param>
        /// <param name="divisor"></param>
        /// <returns></returns>
        public static int Divide(int dividend, int divisor)
        {
            if (divisor == 0 || (dividend == Int32.MinValue && divisor == -1)) return Int32.MaxValue;
            int sign = ((dividend < 0) ^ (divisor < 0)) ? -1 : 1;
            int dvd = Math.Abs(dividend);
            int dvs = Math.Abs(divisor);
            int res = 0;
            while (dvd >= dvs)
            {
                int tmp = dvs, multiple = 1;
                // if on thec condition "max / 1", tmp will overflow. 
                // 2 ^30 << 1 = 2^31 but Int32 ranges from -2^31 to (2^31)-1
                while (dvd >= (tmp << 1))
                {
                    if (tmp != 1073741824)
                    {
                        tmp <<= 1;
                        multiple <<= 1;
                    }
                    else
                    {
                        tmp = Int32.MaxValue;
                        multiple = Int32.MaxValue;
                    }
                }
                dvd -= tmp;
                res += multiple;
            }
            return sign == 1 ? res : -res;
        }

        /// <summary>
        /// Level Order Bottom
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public IList<IList<int>> LevelOrderBottom(TreeNode root)
        {
            IList<IList<int>> res = new List<IList<int>>();
            if (root == null) return res;
            Queue<TreeNode> que = new Queue<TreeNode>();
            que.Enqueue(root);
            while (true)
            {
                int nodeCount = que.Count;
                if (nodeCount == 0) break;
                List<int> subList = new List<int>();
                while (nodeCount > 0)
                {
                    TreeNode dataNode = que.Dequeue();
                    subList.Add(dataNode.val);
                    if (dataNode.left != null)
                        que.Enqueue(dataNode.left);
                    if (dataNode.right != null)
                        que.Enqueue(dataNode.right);
                    nodeCount--;
                }
                res.Insert(0, subList);
            }
            return res;
        }

        /// <summary>
        /// Single Number II
        /// </summary>
        /// <param name="A"></param>
        /// <returns></returns>
        public static int SingleNumberII(int[] A)
        {
            int ones = 0, twos = 0;
            for (int i = 0; i < A.Length; i++)
            {
                ones = (ones ^ A[i]) & ~twos;
                twos = (twos ^ A[i]) & ~ones;
            }
            return ones;
        }

        /// <summary>
        /// Sort List
        /// </summary>
        /// <param name="head"></param>
        /// <returns></returns>
        public static ListNode SortList(ListNode head)
        {
            if (head == null || head.next == null) return head;

            ListNode prev = null, left = head, right = head;
            while (right != null && right.next != null)
            {
                prev = left;
                left = left.next;
                right = right.next.next;
            }
            prev.next = null;

            ListNode l1 = SortList(head);
            ListNode l2 = SortList(left);

            return MergeTwoLists(l1, l2);
        }

        /// <summary>
        /// Permute
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static IList<IList<int>> Permute(int[] nums)
        {
            IList<IList<int>> res = new List<IList<int>>();
            Queue<IList<int>> q = new Queue<IList<int>>();
            q.Enqueue(new List<int>());
            for (int i = 0; i < nums.Length; i++)
            {
                int size = q.Count;
                while (size-- > 0)
                {
                    IList<int> list = q.Dequeue();
                    for (int j = 0; j <= list.Count; j++)
                    {
                        List<int> tmp = new List<int>(list);
                        tmp.Insert(j, nums[i]);
                        q.Enqueue(tmp);
                    }
                }
            }
            while (q.Count > 0)
            {
                res.Add(q.Dequeue());
            }
            return res.Reverse().ToList();
        }

        /// <summary>
        /// strStr
        /// </summary>
        /// <param name="haystack"></param>
        /// <param name="needle"></param>
        /// <returns></returns>
        public static int StrStr(string haystack, string needle)
        {
            int m = haystack.Length, n = needle.Length;
            if (n < 1) return 0;
            List<int> lps = KMPProcess(needle);
            for (int i = 0, j = 0; i < m;)
            {
                if (haystack[i] == needle[j])
                {
                    i++; j++;
                }
                if (j == n) return i - j;
                if (i < m && haystack[i] != needle[j])
                {
                    if (j > 0) j = lps[j - 1];
                    else i++;
                }
            }
            return -1;
        }
        /// <summary>
        /// KMP Process
        /// </summary>
        /// <param name="needle"></param>
        /// <returns></returns>
        private static List<int> KMPProcess(string needle)
        {
            int n = needle.Length;
            List<int> lps = new List<int>();
            for (int i = 0; i < n; i++)
                lps.Add(0);
            for (int i = 1, len = 0; i < n;)
            {
                if (needle[i] == needle[len])
                {
                    lps[i] = ++len;
                    i++;
                }
                else if (len > 0) len = lps[len - 1];
                else lps[i++] = 0;
            }
            return lps;
        }

        /// <summary>
        /// Is Match
        /// </summary>
        /// <param name="text">string input</param>
        /// <param name="pattern">match pattern</param>
        /// <returns></returns>
        public static bool IsMatch(string text, string pattern)
        {
            bool[,] dp = new bool[text.Length + 1, pattern.Length + 1];
            dp[text.Length, pattern.Length] = true;
            for (int i = text.Length; i >= 0; i--)
            {
                for (int j = pattern.Length - 1; j >= 0; j--)
                {
                    bool first_match = (i < text.Length
                        && (pattern[j] == text[i] || pattern[j] == '.'));
                    if (j + 1 < pattern.Length && pattern[j + 1] == '*')
                    {
                        dp[i, j] = dp[i, j + 2] || first_match && dp[i + 1, j];
                    }
                    else
                    {
                        dp[i, j] = first_match && dp[i + 1, j + 1];
                    }
                }
            }
            return dp[0, 0];
        }

        /// <summary>
        /// Merge K Sorted LinkedList
        /// </summary>
        /// <param name="lists">the array of lists</param>
        /// <returns>merged list</returns>
        public static ListNode MergeKLists(ListNode[] lists)
        {
            int len = lists.Length;
            int interval = 1;
            while (interval < len)
            {
                for (int i = 0; i < len - interval; i += interval * 2)
                {
                    lists[i] = MergeTwoLists(lists[i], lists[i + interval]);
                }
                interval *= 2;
            }
            return len > 0 ? lists[0] : null;
        }
        /// <summary>
        /// Merge two sorted LinkedList
        /// </summary>
        /// <param name="l1">first list</param>
        /// <param name="l2">second list</param>
        /// <returns>merged list</returns>
        public static ListNode MergeTwoLists(ListNode l1, ListNode l2)
        {
            // Iteratively 124ms
            //ListNode point = new ListNode(0);
            //ListNode head = point;
            //while (l1 != null && l2 != null)
            //{
            //    if (l1.val <= l2.val)
            //    {
            //        point.next = l1;
            //        l1 = l1.next;
            //    }
            //    else
            //    {
            //        point.next = l2;
            //        l2 = l2.next;
            //    }
            //    point = point.next;
            //}
            //if (l1 == null)
            //    point.next = l2;
            //if(l2 == null)
            //    point.next = l1;
            //return head.next;

            // Recursively 112ms
            //if (l1 == null) return l2;
            //if (l2 == null) return l1;
            //ListNode ans = null;
            //if (l1.val < l2.val)
            //{
            //    ans = l1;
            //    ans.next = MergeTwoLists(l1.next, l2);
            //}
            //else
            //{
            //    ans = l2;
            //    ans.next = MergeTwoLists(l1, l2.next);
            //}
            //return ans;

            // use ref 108ms
            ListNode ret = null;
            ListNode end = null;
            if (l1 == null) return l2;
            if (l2 == null) return l1;
            while (l1 != null || l2 != null)
            {
                if (l1 == null)
                {
                    end.next = l2;
                    break;
                }
                if (l2 == null)
                {
                    end.next = l1;
                    break;
                }
                if (l1.val > l2.val)
                {
                    Add(ref ret, ref end, l2.val);
                    l2 = l2.next;
                    continue;
                }
                else
                {
                    Add(ref ret, ref end, l1.val);
                    l1 = l1.next;
                }
            }
            return ret;
        }
        private static void Add(ref ListNode head, ref ListNode end, int val)
        {
            ListNode node = new ListNode(val);
            if (end == null)
            {
                end = node;
                head = node;
            }
            else
            {
                end.next = node;
                end = end.next;
            }
        }

        /// <summary>
        /// Repeated Substring
        /// </summary>
        /// <param name="s">input string</param>
        /// <returns></returns>
        public static bool RepeatedSubstring(string s)
        {
            int n = s.Length;
            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= n / 2; i++)
            {
                if (n % i != 0) continue;
                sb.Clear();
                string sub = s.Substring(0, i);
                while (sb.Length < n) sb.Append(sub);
                if (sb.ToString().Equals(s)) return true;
            }
            return false;
        }

        /// <summary>
        /// Letter Combinations
        /// </summary>
        /// <param name="digits">input digits</param>
        /// <returns></returns>
        public static IList<string> LetterCombinations(string digits)
        {
            string[] map = { "0", "1", "abc", "def", "ghi", "jkl", "mno", "pqrs", "tuv", "wxyz" };
            if (String.IsNullOrEmpty(digits)) return new List<string>();
            Queue<string> ans = new Queue<string>();
            ans.Enqueue("");
            for (int i = 0; i < digits.Length; i++)
            {
                int x = digits[i] - '0';
                while (ans.Peek().Length == i)
                {
                    string t = ans.Dequeue();
                    foreach (char c in map[x])
                        ans.Enqueue(t + c);
                }
            }
            return ans.ToList();
        }

        /// <summary>
        /// Merge Sorted Array
        /// </summary>
        /// <param name="num1"></param>
        /// <param name="m"></param>
        /// <param name="num2"></param>
        /// <param name="n"></param>
        public static void Merge(int[] num1, int m, int[] num2, int n)
        {
            int p = m - 1, q = n - 1, i = m + n - 1;
            while (q >= 0)
            {
                if (p < 0 || num2[q] >= num1[p])
                    num1[i--] = num2[q--];
                else
                    num1[i--] = num1[p--];
            }
        }

        /// <summary>
        /// ZigZag Conversion
        /// </summary>
        /// <param name="s"></param>
        /// <param name="numRows"></param>
        /// <returns></returns>
        public static string Convert(string s, int numRows)
        {
            if (numRows <= 1) return s;
            StringBuilder res = new StringBuilder();
            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < s.Length; j += 2 * numRows - 2)
                {
                    int index = i + j;
                    if (index < s.Length)
                        res.Append(s[index]);
                    if (i != 0 && i != numRows - 1)
                    {
                        index = j + 2 * numRows - 2 - i;
                        if (index < s.Length) res.Append(s[index]);
                    }
                }
            }
            return res.ToString();
        }

        /// <summary>
        /// Judge Point 24
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static bool JudgePoint24(int[] nums)
        {
            List<double> A = new List<double>();
            foreach (int item in nums)
                A.Add(System.Convert.ToDouble(item));
            return Solve(A);
        }
        private static bool Solve(List<double> nums)
        {
            if (nums.Count == 0) return false;
            if (nums.Count == 1) return Math.Abs(nums[0] - 24) < 1e-6;

            for (int i = 0; i < nums.Count; i++)
            {
                for (int j = 0; j < nums.Count; j++)
                {
                    if (i != j)
                    {
                        List<double> nums2 = new List<double>();
                        for (int k = 0; k < nums.Count; k++)
                        {
                            if (k != i && k != j) nums2.Add(nums[k]);
                        }
                        for (int k = 0; k < 4; k++)
                        {
                            if (k < 2 && j > i) continue;
                            if (k == 0) nums2.Add(nums[i] + nums[j]);
                            if (k == 1) nums2.Add(nums[i] * nums[j]);
                            if (k == 2) nums2.Add(nums[i] - nums[j]);
                            if (k == 3)
                            {
                                if (nums[j] != 0) nums2.Add(nums[i] / nums[j]);
                                else continue;
                            }
                            if (Solve(nums2)) return true;
                            nums2.Remove(nums2.Count - 1);
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Rotate String
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <returns></returns>
        public static bool RotateString(string A, string B) => A.Length == B.Length && (A + A).Contains(B);

        /// <summary>
        /// Valid Parenthesess
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValid(string s)
        {
            Stack<char> stack = new Stack<char>();
            foreach (char item in s.ToCharArray())
            {
                if (item == '(')
                    stack.Push(')');
                else if (item == '{')
                    stack.Push('}');
                else if (item == '[')
                    stack.Push(']');
                else if (stack.Count == 0 || stack.Pop() != item)
                    return false;
            }
            return stack.Count == 0;
        }

        /// <summary>
        /// Find Words
        /// </summary>
        /// <param name="board"></param>
        /// <param name="words"></param>
        /// <returns></returns>
        public static IList<string> FindWords(char[,] board, string[] words)
        {
            IList<string> res = new List<string>();
            TrieNode root = BuildTrie(words);
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    Dfs(board, i, j, root, res);
                }
            }
            return res;
        }
        /// <summary>
        /// FindWords DFS
        /// </summary>
        /// <param name="board"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="p"></param>
        /// <param name="res"></param>
        private static void Dfs(char[,] board, int i, int j, TrieNode p, IList<string> res)
        {
            char c = board[i, j];
            if (c == '#' || p.Get(c) == null) return;
            p = p.Get(c);
            if (p.Word != null)
            {
                res.Add(p.Word);
                p.Word = null;
            }
            board[i, j] = '#';
            if (i > 0) Dfs(board, i - 1, j, p, res);
            if (j > 0) Dfs(board, i, j - 1, p, res);
            if (i < board.GetLength(0) - 1) Dfs(board, i + 1, j, p, res);
            if (j < board.GetLength(1) - 1) Dfs(board, i, j + 1, p, res);
            board[i, j] = c;
        }
        /// <summary>
        /// FindWords  Trie
        /// </summary>
        /// <param name="words"></param>
        /// <returns></returns>
        private static TrieNode BuildTrie(string[] words)
        {
            TrieNode root = new TrieNode();
            foreach (string item in words)
            {
                TrieNode p = root;
                foreach (char ch in item.ToCharArray())
                {
                    if (p.Get(ch) == null)
                    {
                        p.Links[ch - 'a'] = new TrieNode();
                    }

                    p = p.Get(ch);
                }
                p.Word = item;
            }
            return root;
        }

        /// <summary>
        /// Is Isomorphic
        /// </summary>
        /// <param name="s"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool IsIsomorphic(string s, string t)
        {
            int[] m1 = new int[256];
            int[] m2 = new int[256];
            int n = s.Length;
            for (int i = 0; i < 256; i++)
            {
                m1[i] = m2[i] = -1;
            }
            for (int i = 0; i < n; i++)
            {
                if (m1[s[i]] != m2[t[i]]) return false;
                m1[s[i]] = m2[t[i]] = i;
            }
            return true;
        }

        /* Happy Number
         * 19
         * 1^2+9^2=82
         * 8^2+2^2=68
         * 6^2+8^2=100
         * 1^2+0^2+0^2=1
         */
        /// <summary>
        /// Happy Number
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsHappy(int n)
        {
            int slow = n, fast = n;
            while (slow != 1)
            {
                slow = DigitSquareSum(slow);
                fast = DigitSquareSum(DigitSquareSum(fast));
                if (slow != 1 && slow == fast)
                    return false;
            }
            return true;
        }
        /// <summary>
        /// Happy Number Squaresum
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        private static int DigitSquareSum(int n)
        {
            int sum = 0;
            while (n > 0)
            {
                int tmp = n % 10;
                sum += tmp * tmp;
                n /= 10;
            }
            return sum;
        }


        private static string PreProcess(string s)
        {
            int n = s.Length;
            if (n == 0) return "^$";
            string ret = "^";
            for (int i = 0; i < n; i++)
            {
                ret += "#" + s.Substring(i, 1);
            }
            ret += "#$";
            return ret;
        }

        /// <summary>
        /// Power of Four
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public static bool IsPowerOfFour(int num) => num > 0 && (num & (num - 1)) == 0 && (num - 1) % 3 == 0;

        /// <summary>
        /// Power of Two
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static bool IsPowerOfTwo(int n) => n > 0 && (n & (n - 1)) == 0;

        /// <summary>
        /// Rob III 
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public static int RobIII(TreeNode root)
        {
            int[] res = SubRob(root);
            return Math.Max(res[0], res[1]);
        }
        private static int[] SubRob(TreeNode root)
        {
            if (root == null) return new int[2];
            int[] left = SubRob(root.left);
            int[] right = SubRob(root.right);
            int[] res = new int[2];
            res[0] = Math.Max(left[0], left[1]) + Math.Max(right[0], right[1]);
            res[1] = root.val + left[0] + right[0];
            return res;
        }

        /// <summary>
        /// Rob II
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int RobII(int[] nums)
        {
            int n = nums.Length;
            if (n < 2) return n == 1 ? nums[0] : 0;
            return Math.Max(Robber(nums, 0, n - 2), Robber(nums, 1, n - 1));
        }
        private static int Robber(int[] nums, int l, int r)
        {
            int pre = 0, cur = 0;
            for (int i = l; i <= r; i++)
            {
                int temp = Math.Max(pre + nums[i], cur);
                pre = cur;
                cur = temp;
            }
            return cur;
        }

        /// <summary>
        /// Rob I
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int RobI(int[] nums)
        {
            //int a = 0;
            //int b = 0;
            //for (int i = 0; i < nums.Length; i++)
            //{
            //    if (i % 2 == 0)
            //        a = Math.Max(a + nums[i], b);
            //    else
            //        b = Math.Max(a, b + nums[i]);
            //}
            //return Math.Max(a, b);
            int rob = 0;
            int notrob = 0;
            foreach (int item in nums)
            {
                int current = notrob + item;
                notrob = Math.Max(notrob, rob);
                rob = current;
            }
            return Math.Max(notrob, rob);
        }

        /// <summary>
        /// Calculate Minimum HP
        /// </summary>
        /// <param name="dungeon">map</param>
        /// <returns></returns>
        public static int CalculateMinimumHP(int[,] dungeon)
        {
            if (dungeon == null || dungeon.GetLength(0) == 0 || dungeon.GetLength(1) == 0) return 0;
            int m = dungeon.GetLength(0);
            int n = dungeon.GetLength(1);
            int[,] health = new int[m, n];
            health[m - 1, n - 1] = Math.Max(1 - dungeon[m - 1, n - 1], 1);
            for (int i = m - 2; i >= 0; i--)
                health[i, n - 1] = Math.Max(health[i + 1, n - 1] - dungeon[i, n - 1], 1);
            for (int j = n - 2; j >= 0; j--)
                health[m - 1, j] = Math.Max(health[m - 1, j + 1] - dungeon[m - 1, j], 1);
            for (int i = m - 2; i >= 0; i--)
            {
                for (int j = n - 2; j >= 0; j--)
                {
                    int down = Math.Max(health[i + 1, j] - dungeon[i, j], 1);
                    int right = Math.Max(health[i, j + 1] - dungeon[i, j], 1);
                    health[i, j] = Math.Min(right, down);
                }
            }
            return health[0, 0];
        }

        /// <summary>
        /// Trainling Zeroes
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static int TrailingZeroes(int n) => n == 0 ? 0 : n / 5 + TrailingZeroes(n / 5);

        /// <summary>
        /// Set Zeroes
        /// </summary>
        /// <param name="matrix"></param>
        public static void SetZeroes(int[,] matrix)
        {
            int col0 = 1, rows = matrix.GetLength(0), cols = matrix.GetLength(1);
            // top-down
            for (int i = 0; i < rows; i++)
            {
                // first column
                if (matrix[i, 0] == 0) col0 = 0;
                for (int j = 1; j < cols; j++)
                {
                    // if the current cell is "0" set i row and j col as "0"
                    if (matrix[i, j] == 0)
                        matrix[i, 0] = matrix[0, j] = 0;
                }
            }
            // bottom-up
            for (int i = rows - 1; i >= 0; i--)
            {
                for (int j = cols - 1; j >= 1; j--)
                {
                    if (matrix[i, 0] == 0 || matrix[0, j] == 0)
                        matrix[i, j] = 0;
                }
                if (col0 == 0) matrix[i, 0] = 0;
            }
        }

        ///<summary>
        /// Solve Sudoku
        ///</summary>
        public static void SolveSudoku(char[,] board) => DoSolve(board, 0, 0);
        /// <summary>
        /// Do Solve
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <returns></returns>
        private static bool DoSolve(char[,] board, int row, int col)
        {
            for (int i = row; i < 9; i++, col = 0)
            {
                for (int j = col; j < 9; j++)
                {
                    if (board[i, j] != '.') continue;
                    for (char num = '1'; num <= '9'; num++)
                    {
                        if (IsValid(board, i, j, num))
                        {
                            board[i, j] = num;
                            if (DoSolve(board, i, j + 1))
                                return true;
                            board[i, j] = '.';
                        }
                    }
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Is Valid
        /// </summary>
        /// <param name="board"></param>
        /// <param name="row"></param>
        /// <param name="col"></param>
        /// <param name="num"></param>
        /// <returns></returns>
        private static bool IsValid(char[,] board, int row, int col, char num)
        {
            int blkrow = (row / 3) * 3, blkcol = (col / 3) * 3;
            for (int i = 0; i < 9; i++)
            {
                if (board[i, col] == num || board[row, i] == num || board[blkrow + i / 3, blkcol + i % 3] == num)
                    return false;
            }
            return true;
        }

        #region Binary Search
        /// <summary>
        /// Square root
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static int Sqrt(int x)
        {
            if (x == 0)
                return 0;
            int left = 1, right = x;
            int mid = 1;
            while (left <= right)
            {
                mid = left + (right - left) / 2;
                if (mid == x / mid)
                    return mid;
                else if (mid < x / mid)
                    left = mid + 1;
                else
                    right = mid - 1;
            }
            return right;
        }

        private static int Guess(int num)
        {
            Random random = new Random();
            int target = random.Next(1, int.MaxValue);
            if (num > target) return 1;
            else if (num < target) return -1;
            else return 1;
        }
        public static int GuessNumber(int n)
        {
            int left = 1, right = n;
            int mid = 1;
            while (left <= right)
            {
                mid = left + (right - left) / 2;
                int res = Guess(mid);
                if (res == 0) return mid;
                else if (res < 0) right = mid - 1;
                else left = mid + 1;
            }
            return -1;
        }
        public static int Search(int[] nums, int target)
        {
            int left = 0, right = nums.Length;
            while (left < right)
            {
                int mid = (left + right) / 2;
                double num = (nums[mid] < nums[0]) == (target < nums[0])
                                 ? nums[mid]
                                 : target < nums[0] ? double.NegativeInfinity : double.PositiveInfinity;
                if (num < target)
                    left = mid + 1;
                else if (num > target)
                    right = mid;
                else
                    return mid;
            }
            return -1;
        }
        #endregion



        /// <summary>
        /// Length Of Last Word
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLastWord(string s)
        {
            s = s.Trim();
            int lastIndex = s.LastIndexOf(' ') + 1;
            return s.Length - lastIndex;
        }

        /// <summary>
        /// Jump Game
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static bool CanJump(int[] nums)
        {
            int lastPos = nums.Length - 1;
            for (int i = nums.Length - 1; i >= 0; i--)
            {
                if (i + nums[i] >= lastPos)
                    lastPos = i;
            }
            return lastPos == 0;
        }

        /// <summary>
        /// Plus One 
        /// </summary>
        /// <param name="digits"></param>
        /// <returns></returns>
        public static int[] PlusOne(int[] digits)
        {
            int n = digits.Length;
            for (int i = n - 1; i >= 0; i--)
            {
                if (digits[i] < 9)
                {
                    digits[i]++;
                    return digits;
                }
                digits[i] = 0;
            }
            int[] newNumber = new int[n + 1];
            newNumber[0] = 1;
            return newNumber;
        }

        /// <summary>
        ///  Remove Nth Node from end of list
        /// </summary>
        /// <param name="head"></param>
        /// <param name="n"></param>
        /// <returns></returns>
        public static ListNode RemoveNthFromEnd(ListNode head, int n)
        {
            ListNode dummy = new ListNode(0)
            {
                next = head
            };
            ListNode first = dummy;
            ListNode second = dummy;
            for (int i = 1; i <= n + 1; i++)
                first = first.next;
            while (first != null)
            {
                first = first.next;
                second = second.next;
            }
            second.next = second.next.next;
            return dummy.next;
        }

        /// <summary>
        /// To Underscore
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToUnderscore(string str)
        {
            // testcase: ABC -> a_b_c
            string[] digs = {
                "A",
                "B",
                "C",
                "D",
                "E",
                "F",
                "G",
                "H",
                "I",
                "J",
                "K",
                "L",
                "M",
                "N",
                "O",
                "P",
                "Q",
                "R",
                "S",
                "T",
                "U",
                "V",
                "W",
                "X",
                "Y",
                "Z"
            };
            string[] res = new string[str.Length];
            for (int i = 0; i < str.Length; i++)
            {
                res[i] = str.Substring(i, 1);
            }
            for (int i = 0; i < str.Length; i++)
            {
                if (digs.Contains(res[i]))
                {
                    if (i > 0)
                    {
                        res[i] = "_" + res[i].ToLower();
                    }
                    res[i] = res[i].ToLower();
                }
            }
            string result = String.Join("", res);
            return result;
        }

        /// <summary>
        /// Score
        /// </summary>
        /// <param name="dice"></param>
        /// <returns></returns>
        public static int Score(int[] dice)
        {
            int[] diceR = { 0, 0, 0, 0, 0, 0 };
            int[] tdr = { 1000, 200, 300, 400, 500, 600 };
            int[] sdr = { 100, 0, 0, 0, 50, 0 };
            foreach (int item in dice)
            {
                diceR[item - 1]++;
            }
            int score = 0;
            for (int i = 0; i < diceR.Length; i++)
            {
                score += (diceR[i] >= 3 ? tdr[i] : 0) + sdr[i] * (diceR[i] % 3);
            }
            return score;
        }

        /// <summary>
        /// Move Zeroes
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static void MoveZeroes(int[] nums)
        {
            int j = 0;
            for (int i = 0; i < nums.Length; i++)
                if (nums[i] != 0) nums[j++] = nums[i];
            for (; j < nums.Length; j++) nums[j] = 0;
        }





        /// <summary>
        /// Roman To Integer
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int RomanToInteger(string s)
        {
            Dictionary<char, int> dic = new Dictionary<char, int> { { 'I', 1 },
                { 'V', 5 },
                { 'X', 10 },
                { 'L', 50 },
                { 'C', 100 },
                { 'D', 500 },
                { 'M', 1000 }
            };
            int value = 0;
            char prev = s[0];
            foreach (char curr in s)
            {
                value += dic[curr];
                if (dic[prev] < dic[curr])
                {
                    value -= dic[prev] * 2;
                }
                prev = curr;
            }
            return value;
        }

        /// <summary>
        /// Reverse Words
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ReverseWords(string str)
        {
            char[] chars = str.ToCharArray().Reverse().ToArray();
            string newstr = "";
            string relstr = "";
            foreach (char item in chars) newstr += item;
            string[] result = newstr.Split(' ');
            for (int i = result.Length - 1; i >= 0; i--)
            {
                if (i == 0) relstr += result[i];
                else relstr = relstr + result[i] + " ";
            }
            return relstr;
        }

        /// <summary>
        /// Single Number
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int SingleNumber(int[] nums)
        {
            int result = 0;
            foreach (int item in nums) result ^= item;
            return result;
        }

        /// <summary>
        /// Reverse String
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ReverseString(string s)
        {
            if (s == string.Empty)
                return String.Empty;
            // two pointers
            char[] ch = new char[s.Length];
            int i = 0;
            int j = s.Length - 1;
            while (i <= j)
            {
                ch[j] = s[i];
                ch[i] = s[j];
                i++;
                j--;
            }
            return new string(ch);
        }

        /// <summary>
        /// Is Land Perimeter
        /// </summary>
        /// <param name="grid"></param>
        /// <returns></returns>
        public static int IsLandPerimeter(int[,] grid)
        {
            int island = 0;
            int neighbor = 0;
            for (int i = 0; i < grid.Length; i++)
            {
                for (int j = 0; j < grid.GetLength(1) - 1; j++)
                {
                    if (grid[i, j] == 1)
                    {
                        island++;
                        if (i < grid.Length - 1 && grid[i, j + 1] == 1) neighbor++;
                        if (j < grid.GetLength(1) - 1 && grid[i, j] == 1) neighbor++;
                    }
                }
            }
            return island * 4 - neighbor * 2;
        }

        /// <summary>
        /// Is Palindrome
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public static bool IsPalindrome(int x)
        {
            if (x < 0 || (x != 0 && x % 10 == 0)) return false;
            int res = 0;
            while (res < x)
            {
                res = res * 10 + x % 10;
                x /= 10;
            }
            return (x == res || x == res / 10);
            // if (x < 10) return true;
            // int n = 0, temp = x;
            // while (temp / 10 != 0)
            // {
            //     n += temp % 10;
            //     n *= 10;
            //     temp /= 10;
            // }
            // n += temp % 10;
            // return (n == x);
        }

        public static bool IsPalindrome2(int x)
        {
            if (x < 0) return false;
            if (x == 0) return true;
            int tmp = x;
            int y = 0;
            while (x != 0)
            {
                y = y * 10 + x % 10;
                x = x / 10;
            }
            if (y == tmp) return true;
            return false;
        }

        /// <summary>
        /// Longest Common Prefix
        /// </summary>
        /// <param name="strs"></param>
        /// <returns></returns>
        public static string LongestCommonPrefix(string[] strs)
        {
            if (strs.Length == 0) return "";
            string pre = strs[0];
            for (int i = 1; i < strs.Length; i++)
                while (strs[i].IndexOf(pre, StringComparison.Ordinal) != 0)
                    pre = pre.Substring(0, pre.Length - 1);
            return pre;
        }

        /// <summary>
        /// Fib by Rec
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long ReFib(int n)
        {
            if (n < 2)
                return n;
            else
                return ReFib(n - 1) + ReFib(n - 2);
        }

        /// <summary>
        /// Fib by dg with one array
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long IterFibWithArray(int n)
        {
            int[] val = new int[n];
            if (n == 1 || n == 2)
                return 1;
            else
            {
                val[1] = 1;
                val[2] = 2;
                for (int i = 3; i <= n - 1; i++)
                {
                    val[i] = val[i - 1] + val[i - 2];
                }
            }
            return val[n - 1];

        }

        /// <summary>
        /// Fib by dg without array
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public static long IterFibWithoutArray(int n)
        {
            long last = 1;
            long nextLast = 1;
            long result = 1;
            for (int i = 2; i <= n - 1; i++)
            {
                result = last + nextLast;
                nextLast = last;
                last = result;
            }
            return result;
        }



        /// <summary>
        /// Change string to integer
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int Atoi(string str)
        {
            if (string.IsNullOrEmpty(str)) return 0;
            int sign = 1;
            int bas = 0;
            int i = 0;
            while (str[i] == ' ')
                i++;

            if (str[i] == '-' || str[i] == '+')
                sign = str[i++] == '-' ? -1 : 1;
            while (i < str.Length && str[i] >= '0' && str[i] <= '9')
            {
                if (bas > int.MaxValue / 10 || (bas == int.MaxValue / 10 && str[i] - '0' > 7))
                {
                    return (sign == 1) ? int.MaxValue : int.MinValue;
                }
                bas = 10 * bas + (str[i++] - '0');
            }
            return bas * sign;
        }



        /// <summary>
        /// Trap Rain Water
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public static int Trap(int[] height)
        {
            if (height == null) return 0;
            int l = 0, r = height.Length - 1;
            int lmax = 0, rmax = 0;
            int ans = 0;
            while (l < r)
            {
                if (l < r)
                {
                    if (height[l] < height[r])
                    {

                        if (height[l] >= lmax)
                            lmax = height[l];
                        else
                            ans += (lmax - height[l]);
                        l++;
                    }
                    else
                    {
                        if (height[r] >= rmax)
                            rmax = height[r];
                        else
                            ans += (rmax - height[r]);
                        r--;
                    }
                }
            }
            return ans;
        }



        /// <summary>
        /// Jump
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int Jump(int[] nums)
        {
            int n = nums.Length;
            if (n < 2) return 0;
            int level = 0;
            int currentMax = 0;
            int i = 0;
            int nextMax = 0;
            while (currentMax - i + 1 > 0)
            {
                level++;
                for (; i <= currentMax; i++)
                {
                    nextMax = Math.Max(nextMax, nums[i] + i);
                    if (nextMax >= n - 1)
                        return level;
                }
                currentMax = nextMax;
            }
            return 0;
        }

        /// <summary>
        /// Jump by Greedy
        /// </summary>
        /// <param name="nums"></param>
        /// <returns></returns>
        public static int JumpByGreedy(int[] nums)
        {
            int jumps = 0, currEnd = 0, currFarthest = 0;
            for (int i = 0; i < nums.Length - 1; i++)
            {
                currFarthest = Math.Max(currFarthest, i + nums[i]); ;
                if (i == currEnd)
                {
                    jumps++;
                    currEnd = currFarthest;
                    if (currEnd >= nums.Length - 1)
                        break;
                }
            }
            return jumps;
        }
    }
}