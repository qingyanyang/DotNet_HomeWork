## Assignment

Please create a collection of students with the courses - C#, HTML and React, please set the scores of the courses

1. Please select the proper collection type to print out the unique scores for each course, no duplicated scores printed out

e.g. C# course, scores are 70, 80, 90, 95, 96, 99.

       HTML course, scores are 75, 81, 94, 96, 98, 99.

       React course, scores are 85, 88, 64, 76, 99, 100.

1. Please select one collection type from HashTable/Dictionary/SortedList to store the scores for each course of each student and print them out:

Note: Please use the "student Name  + course Name" as the key, and score as the value,

e.g.

      Key: James Hush C#

      Value: 80

One edge case: Please test the edge case - two students with same Name, for same course, is it possible to store both of them in the selected collection (HashTable/Dictionary/SortedList)?

1. Please use Lookup to represents a collection of keys each mapped to one or more values.

Need store the course score and student names, then print out:

e.g. for C# course, please use the score as the key, student name as the values, Print the student course scores in grouping format

Key: 80

Value: James Hush, Luck Linton, Jack Matt, ......

Print output:

80: James Hush, Luck Linton, Jack Matt, ......

Key: 90

Value: Mike Lu, Jimmy Hugh, Penny A, ......

Print output:

90: Mike Lu, Jimmy Hugh, Penny A, ......

1. Implement Undo and Redo features

Given an array of strings Q[], consisting of queries of the following types:

“WRITE X”: Write a character X into the array.

“UNDO”: Erases the last change made to the array.

“REDO”: Restores the most recent UNDO operation performed on the array.

“READ”: Reads and prints the contents of the array.

Examples:

Input: Q = {“WRITE A”, “WRITE B”, “WRITE C”, “UNDO”, “READ”, “REDO”, “READ”}

Output: AB ABC

Explanation:

Perform “WRITE A” on the document. Therefore, the document contains only “A”.

Perform “WRITE B” on the document. Therefore, the document contains “AB”.

Perform “WRITE C” on the document. Therefore, the document contains “ABC”.

Perform “UNDO” on the document. Therefore, the document contains “AB”.

Perform "READ” to Print the contents of the document, i.e. “AB”

Perform “REDO” on the document. Therefore, the document contains “ABC”.

Perform "READ” to Print the contents of the document, i.e. “ABC”

Input: Q = {“WRITE x”, “WRITE y”, “UNDO”, “WRITE z”, “READ”, “REDO”, “READ”}

Output:xz xzy

Approach: The problem can be solved using Stack. Follow the steps below to solve the problem:

Initialize two stacks, say Undo and Redo.

Traverse the array of strings, Q, and perform the following operations:

If “WRITE” string is encountered, push the character to Undo stack

If “UNDO” string is encountered, pop the top element from Undo stack and push it to Redo stack.

If “REDO” string is encountered, pop the top element of Redo stack and push it into the Undo stack.

If “READ” string is encountered, print all the elements of the Undo stack in reverse order.

Define below string array, after invoking the method, print out the correct result:

string[] Q = { "WRITE A", "WRITE B", "WRITE C", "UNDO", "READ", "REDO", "READ" };

QUERY(Q);
