// COEN346-Assignment1.cpp
// Copyright © February 12, 2021 all rights reserved.
// Authors: Orion Kevlar (40099465), Steven Arsan (40113224), Lucas Hotton (27220812)

#include <iostream>
#include <fstream>
#include <thread>

using namespace std;

// Funtion prototype declarations
void merge(int lo, int mid, int hi);
void merge_sort(int lo, int hi);

// Global Varaibles
const int ARRAY_SIZE = 8;               // *make dynamic in future
int arr[ARRAY_SIZE];
ofstream outputFile;

int main()
{
    // Change inputPath variable to your input file's path
    ifstream inputFile;
    string inputPath = "C:\\Users\\okevl\\source\\repos\\COEN346-Assignment1\\input.txt";
   
    // Read input file into an array
    inputFile.open(inputPath);
    for (int i = 0; i < ARRAY_SIZE; i++)    { inputFile >> arr[i]; }
    inputFile.close();

    // Open output file
    outputFile.open("output.txt");
    
    // perform merge sort by spawning a thread
    thread th = thread(merge_sort, 0, ARRAY_SIZE - 1);
    th.join();

    outputFile << "Thread finished: ";
    for (int i = 0; i < ARRAY_SIZE; i++) { outputFile << arr[i] << ", "; }
    //close output file
    outputFile.close();
    return 0;
}

// Merges 2 subarrays
void merge(int lo, int mid, int hi)
{
    // Variables used
    int* left = new int[mid - lo + 1];  // left sub-array
    int* right = new int[hi - mid];     // right sub-array
    int leftLength = mid - lo + 1;      // size of left sub-array
    int rightLength = hi - mid;         // size of right sub-array
    int i, j;                           // itterators used for loops

    // Store values in left sub-array
    for (i = 0; i < leftLength; i++)
    { left[i] = arr[i + lo]; }

    // Store values in right sub-array 
    for (i = 0; i < rightLength; i++)
    { right[i] = arr[i + mid + 1]; }

    // Update itterators 
    int k = lo;
    i = j = 0;

    // merge left and right sub arrays
    // sorting from smallest to largest
    while (i < leftLength && j < rightLength) 
    {
        // if left value is smaller, insert left, otherwise insert right & increment itterators
        if (left[i] <= right[j])
            { arr[k++] = left[i++];  } 
        else
            { arr[k++] = right[j++]; }
    }

    // insert any remaining values from the left sub-array
    while (i < leftLength) 
    { arr[k++] = left[i++];  }

    // insert any remaining values from the right sub-array
    while (j < rightLength) 
    { arr[k++] = right[j++]; } 
}

// merge sort function  // calls merge function
void merge_sort(int lo, int hi)
{
    // Output begining of each thread to file

    outputFile << "Thread started." << "\n";

    // finding the midpoint of the array 
    int mid = lo + (hi - lo) / 2;

    // spawn threads
    thread th1;
    thread th2;

    if (lo < hi) 
    {
        // Synchonize threads:
        // recursively sort left array
        th1 = thread(merge_sort, lo, mid);  
      
        // recursively sort right array
        th2 = thread(merge_sort, mid + 1, hi);

        // joins must be here
        th1.join();
        th2.join();

        // merge sub arrays together 
        merge(lo, mid, hi);

        // Output progress of each thread to file
        outputFile << "Thread finished: ";
        for (int i = 0; i < hi; i++) { outputFile << arr[i] << ", "; }
        outputFile << "\n";
    }
}