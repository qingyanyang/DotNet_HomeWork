// 12 > Nested Loop Practice (Must-Know Simple Algorithm):
// Given an array: int[] nums = { 1, 4, 3, 9, 6, 8, 11 };
// Sort the elements of this array in ascending order.
int[] nums = { 1, 4, 3, 9, 6, 8, 11 };
int lenOfNums = nums.Length;

for (int i = 0; i < lenOfNums; i++) {
    for (int j = 0; j < lenOfNums-i-1; j++)
    {
        if (nums[j] > nums[j+1])
        {
            int temp = nums[j]; 
            nums[j] = nums[j + 1];
            nums[j + 1] = temp;
        }
    }
}

foreach (int ele in nums)
{
    Console.Write(ele+" ");
}
