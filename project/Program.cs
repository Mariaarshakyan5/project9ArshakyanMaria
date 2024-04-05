using System;
using System.Diagnostics;


abstract class SortingAlgorithm
{
    
    public void Sort(int[] array)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        PerformSort(array);
        stopwatch.Stop();
        Console.WriteLine($"{GetType().Name}: Execution Time = {stopwatch.ElapsedMilliseconds / 1000.0f}s");
    }

    
    protected abstract void PerformSort(int[] array);
}


class SelectionSort : SortingAlgorithm
{
    protected override void PerformSort(int[] array)
    {
        int n = array.Length;
        for (int i = 0; i < n - 1; i++)
        {
            int minIndex = i;
            for (int j = i + 1; j < n; j++)
            {
                if (array[j] < array[minIndex])
                {
                    minIndex = j;
                }
            }
            if (minIndex != i)
            {
                int temp = array[i];
                array[i] = array[minIndex];
                array[minIndex] = temp;
            }
        }
    }
}

class MergeSort : SortingAlgorithm
{
    protected override void PerformSort(int[] array)
    {
        MergeSortRecursive(array, 0, array.Length - 1);
    }

    private void MergeSortRecursive(int[] array, int left, int right)
    {
        if (left < right)
        {
            int mid = left + (right - left) / 2;
            MergeSortRecursive(array, left, mid);
            MergeSortRecursive(array, mid + 1, right);
            Merge(array, left, mid, right);
        }
    }

    private void Merge(int[] array, int left, int mid, int right)
    {
        int n1 = mid - left + 1;
        int n2 = right - mid;

        int[] L = new int[n1];
        int[] R = new int[n2];

        Array.Copy(array, left, L, 0, n1);
        Array.Copy(array, mid + 1, R, 0, n2);

        int i = 0, j = 0;
        int k = left;
        while (i < n1 && j < n2)
        {
            if (L[i] <= R[j])
            {
                array[k] = L[i];
                i++;
            }
            else
            {
                array[k] = R[j];
                j++;
            }
            k++;
        }

        while (i < n1)
        {
            array[k] = L[i];
            i++;
            k++;
        }

        while (j < n2)
        {
            array[k] = R[j];
            j++;
            k++;
        }
    }
}

// Concrete class for Shell Sort
class ShellSort : SortingAlgorithm
{
    protected override void PerformSort(int[] array)
    {
        int n = array.Length;
        for (int gap = n / 2; gap > 0; gap /= 2)
        {
            for (int i = gap; i < n; i += 1)
            {
                int temp = array[i];
                int j;
                for (j = i; j >= gap && array[j - gap] > temp; j -= gap)
                {
                    array[j] = array[j - gap];
                }
                array[j] = temp;
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        
        while (true)
        {
            Console.WriteLine("Choose a sorting algorithm:");
            Console.WriteLine("1. Selection Sort");
            Console.WriteLine("2. Merge Sort");
            Console.WriteLine("3. Shell Sort");
            Console.WriteLine("4. Exit");
            Console.Write("Enter your choice: ");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please enter a number.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.Write("Enter the number of random elements to generate: ");
                    int randomCount;
                    if (!int.TryParse(Console.ReadLine(), out randomCount) || randomCount <= 0)
                    {
                        Console.WriteLine("Invalid input. Please enter a positive integer.");
                        continue;
                    }
                    int[] arraySelection = GenerateRandomArray(randomCount);
                    SortingAlgorithm selectionSort = new SelectionSort();
                    selectionSort.Sort((int[])arraySelection.Clone());
                    break;
                case 2:
                    Console.Write("Enter the number of random elements to generate: ");
                    if (!int.TryParse(Console.ReadLine(), out randomCount) || randomCount <= 0)
                    {
                        Console.WriteLine("Invalid input. Please enter a positive integer.");
                        continue;
                    }
                    int[] arrayMerge = GenerateRandomArray(randomCount);
                    SortingAlgorithm mergeSort = new MergeSort();
                    mergeSort.Sort((int[])arrayMerge.Clone());
                    break;
                case 3:
                    Console.Write("Enter the number of random elements to generate: ");
                    if (!int.TryParse(Console.ReadLine(), out randomCount) || randomCount <= 0)
                    {
                        Console.WriteLine("Invalid input. Please enter a positive integer.");
                        continue;
                    }
                    int[] arrayShell = GenerateRandomArray(randomCount);
                    SortingAlgorithm shellSort = new ShellSort();
                    shellSort.Sort((int[])arrayShell.Clone());
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please enter a number between 1 and 4.");
                    break;
            }
        }
    }

    static int[] GenerateRandomArray(int size)
    {
        Random random = new Random();
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = random.Next(); 
        }
        return array;
    }
}
