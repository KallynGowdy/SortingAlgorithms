// Performs Merge Sort on the given array. lbound is used to determine the lower bound
// of where we should "touch" the array. ubound is used to determine the upper bound of where
// we should "touch" the array.
public void RecMergeSort(int[] tempArray, int lbound, int ubound) 
{
    if (lbound == ubound) // If we've split all the way to the bottom, return
        return;
    else
    {
        int mid = (int)(lbound + ubound) / 2; // Find the middle-most index of the array
        RecMergeSort(tempArray, lbound, mid); // "Split" the array and recurse, using mid as the upper bound
        RecMergeSort(tempArray, mid + 1, ubound); // Take the other end of the array and recurse, using mid + 1 as the lower bound
		
        // Merge the elements using the lower bound as the start of the first list and the mid point as the start of the second list
		// Upper bound is used to draw the line between pairs of lists.
		Merge(tempArray, lbound, mid + 1, ubound);
        this.DisplayElements(); Console.WriteLine(); // Display the elements (duh)
    }
}
public void Merge(int[] tempArray, int lowp, int highp, int ubound)
{
    int lbound = lowp; // Start at the first element of the first list
    int mid = highp - 1; // The end of the first list
    int j = 0;
    int n = (ubound - lbound) + 1; // The number of elements that are in the two lists
	
	// While (the current element of the first list is before or at the end of the first list)
	// and (the first element of the second list is less than our upper bound)
    while ((lowp <= mid) && (highp <= ubound)) 
    {
        if (arr[lowp] < arr[highp]) // if the current point in the first list is less than the current point in the second list
        {
            tempArray[j] = arr[lowp]; // Put the first list value in the temporary array
            j++; // Increment our counter for the temp array index
            lowp++; // Go to the next element in our first list
        }
        else
        {
            tempArray[j] = arr[highp]; // Put the second list value in the temporary array
            j++; // Increment our counter for the temp array index
            highp++; // Go to the next element in our second list
        }
    } 
    while (lowp <= mid) // Iterate through the "hanging" elements in our first list
    {
        tempArray[j] = arr[lowp]; // Put the value from the first list into the next slot in our temp array
        j++; // Increment temp array counter
        lowp++; // Increment first list counter
    }
    while (highp <= ubound) // Iterate through the "hanging" elements in our second list
    {
        tempArray[j] = arr[highp]; // Put the value from the second list into the next slot in our temp array
        j++; // Increment temp array counter
        highp++; // Increment second list counter
    }
    for (j = 0; j <= n - 1; j++)
        arr[lbound + j] = tempArray[j]; // Put everything back into the output array.
}
