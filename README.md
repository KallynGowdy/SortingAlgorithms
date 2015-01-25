## Kallyn Gowdy - Bubble Sort - 2015



```
#!javascript



// 1. Get the array (This may or may not be already sorted, but it is probably unsorted)
var unsortedArray = [...]

// 2. Create the output array. This is to prevent modifying the original unsorted array
var output = copy(unsortedArray)

// 3. Set up a flag that is turned on when we swap a value on our way through the array
var haveTwoValuesBeenSwapped = false

// 4. Start a Do-While loop that causes the program to keep looping through the array as long as we have swapped two values at least oncellchange
do {

	// 5. Reset the swap variable at each iteration of the  `for` loop. 
	//    This is to make sure that we don't set the swap to `true` and cause the do-while loop to go on forever.
	haveTwoValuesBeenSwapped = false

	// 6. Loop through the array and compare the current element to the next to see if they should be swapped
	for(var i = 0; 
		i < output.length - 1; // Because we are comparing the current element to the next, we need to make sure that we don't "walk" off the array and cause an exception
		i++) {
		
		var currentValue = output[i]
		var nextValue = output[i + 1] // This is safe to do because i is always at least 2 less than output.length (because of the `i < output.length - 1` condition)
		
		// 7. Swap the values if the first is greater than the second
		if(currentValue > nextValue)	{ 
			output[i] = nextValue
			output[i + 1] = currentValue
			
			// 8. Set the swap flag to `true` to let us know that we should keep looping
			haveTwoValuesBeenSwapped = true
		}
		
	}

} while(haveTwoValuesBeenSwapped)

// 9. Enjoy your sorted array (output is sorted)

```

# Kallyn Gowdy - Selection Sort - 2015


```
#!javascript

// 1. Get the array
var unsortedArray = [...]

// 2. Create output array
var output = copy(unsortedArray)

// 3. Loop for how many elements there are in the array
for(var i = 0;
	i < output.length;
	i++){
	
	var lowest = i // 4. Create a variable that stores the index of the lowest value
	
	// 5. Loop again, but start at i + 1. This loop is to find the lowest value
	for(var c = i + 1;
		c < output.length;
		c++){
		if(output[c] < output[lowest]) {
			lowest = c // 6. Set the lowest value
		}
	}
	
	// 7. Swap
	swap(lowest).with(output[i])
	
}

// 8. Enjoy your sorted array

```