# LeetCode 1679 - Max Number of K-Sum Pairs

**Student Name:** Ahmed Mohamed Abdel Sattar  
**Problem:** [1679. Max Number of K-Sum Pairs](https://leetcode.com/problems/max-number-of-k-sum-pairs/)  
**My Solution:** [Max Operations by Abdelsatttar](https://leetcode.com/problems/max-number-of-k-sum-pairs/solutions/8540097/max-operations-by-abdelsatttar-9gw8)

## Solution

```csharp
public class Solution
{
    public int MaxOperations(int[] nums, int k)
    {
        Array.Sort(nums);

        int left = 0;
        int right = nums.Length - 1;
        int operations = 0;

        while (left < right)
        {
            int sum = nums[left] + nums[right];

            if (sum == k)
            {
                operations++;
                left++;
                right--;
            }
            else if (sum < k)
            {
                left++;
            }
            else
            {
                right--;
            }
        }

        return operations;
    }
}
```



## Complexity

**Time Complexity:** `O(n log n)`

- Sorting takes `O(n log n)`.
- The two-pointer traversal takes `O(n)`.

Therefore, the total complexity is `O(n log n)`.

**Space Complexity:** `O(1)` additional space.
