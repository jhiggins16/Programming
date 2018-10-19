//James Higgins(15620181) Algorithms and Complexity Assessment Item 2

using System;
using System.Collections.Generic;

public class SearchSort{
	public static void Main(String[] args){
	
	bool valid1 = false;
	int fileLength = 0;
	String fileChoice = "";
	String fileToRead = "";
	String AorD = "";
	
	while(valid1 == false){
	//Gets the user to input the file size they want
	Console.WriteLine("What is the size of the file?");
	Console.WriteLine("1 - 128");
	Console.WriteLine("2 - 256");
	Console.WriteLine("3 - 1024");
	int choice = 0;
	//Gets the choice from the user
	try{
	choice = Int32.Parse(Console.ReadLine());
		valid1 = true;
	}catch(FormatException){
		Console.WriteLine("Error");
		valid1 = false;
	}
	
	if(choice == 1){
		//sets file length
		fileLength = 128;
		
		//gets the file choice from the user
		fileChoice = arrayMenu();
		valid1 = true;
		
	}else if(choice == 2){
		//sets file length
		fileLength = 256;
		//gets the file choice from the user
		fileChoice = arrayMenu();
		valid1 = true;
		
	}else if (choice == 3){
		//sets file length
		fileLength = 1024;
		//gets the file choice from the user
		fileChoice = arrayMenu();
		valid1 = true;
		
	}else{
		valid1 = false;
	}
	}
	
	//Sets the name of the file to be read from the users input
	fileToRead = fileChoice + "_" + fileLength + ".txt";
	double[] f;
	if (fileChoice.Equals("CloseHigh")){
		
		//Creates f with double the length to merge the files
		f = new double[fileLength*2];
		double[] t1 = readFile("Close_"+ fileLength + ".txt");
		double[] t2 = readFile("High_"+ fileLength + ".txt");
		int r;
		//merges the files Close and High
		for(r = 0;r < fileLength-1;r++){
			f[r] = t1[r];
		}
		for(r = 0;r < fileLength-1;r++){
			f[r+fileLength] = t2[r];
		}
	}else{
		//Reads the file
		f = readFile(fileToRead);
	}
	
	String sortType = sortMenu();
	
	bool valid2 = false;
	
	while(valid2 == false){
	//Asks the user whether they want to sort the array into ascending or descending order
	Console.WriteLine("How would you like the array to be sorted:");
	Console.WriteLine("(A)scending");
	Console.WriteLine("(D)escending");
	String direction = Console.ReadLine().ToUpper();
	
	//sets a value so the sorting functions know which way to sort 
	if(direction.Equals("A")){
		AorD = "asc";
		valid2 = true;
	}else if(direction.Equals("D")){
		AorD = "des";
		valid2 = true;
	}else{
		Console.WriteLine("Invalid input");
		valid2 = false;
	}
	}
	int stepCount = 0;
	//switch statement on the users input on what sort they want to do
	switch(sortType){
		case "merge":
			//runs merge sort
			mergeSort(f, 0, f.Length-1, AorD, ref stepCount);
			break;
		case "quick":
			//runs quick sort
			quickSort(f, AorD, ref stepCount);
			break;
		case "bubble":
			//runs bubble sort
			bubbleSort(f, AorD, ref stepCount);
			break;
		case "heap":
			//runs heap sort
			heapSort(f, AorD, ref stepCount);
			break;
	}
	//prints the sorted array to the user
	 printArray(f);
	 Console.WriteLine();
	 //Prints the number of steps taken to sort the file 
	Console.WriteLine("Number of steps taken to sort the file = " + stepCount);
	
	//creates the list for the locations of the searched value
	List<int> found = new List<int>();
	int searchMethod = 0;
	double searchFor = 0.0;
	
	bool valid3 = false;
	while(valid3 == false){
	//Asks the user whether they want to search for a value
	Console.WriteLine("Would you like to search for a value?");
	Console.WriteLine("(Y)es");
	Console.WriteLine("(N)o");
	String toSearch = (Console.ReadLine()).ToUpper();
	
	//if the user enters yes
	if(toSearch.Equals("Y")){
		valid3 = true;
		int numSteps = 0;
		bool valid4 = false;
		while(valid4 == false){
		//asks the user what search method they want to use
		Console.WriteLine("Choose a method:");
		Console.WriteLine("1.Binary Search");
		Console.WriteLine("2.Interpolation Search");
		try{
		//gets input from the user
		searchMethod = Int32.Parse(Console.ReadLine());
		}catch(FormatException){
			valid4 = false;
		}
		
		//if the user wants to use a binary search
		if(searchMethod == 1){
			valid4 = true;
			//gets the value to search from the user
			Console.WriteLine("What value do you want to search for?");
			try{
			searchFor = Convert.ToDouble(Console.ReadLine());
			}catch(FormatException){
				valid4 = false;
			}
			if(AorD.Equals("asc")){
				//if the array is sorted in ascending order run binary search for ascending arrays
				binarySearchAsc(0, f.Length-1, f, searchFor, found, ref numSteps);
			}else{
				//if the array is sorted in descending order run binary search for descending arrays
				binarySearchDes(0, f.Length-1, f, searchFor, found, ref numSteps);	
			}
		//if the user wants to use an interpolation search	
		}else if(searchMethod == 2){
			valid4 = true;
			//gets the value the user wants to search for
			Console.WriteLine("What value do you want to search for?");
			try{
			searchFor = Convert.ToDouble(Console.ReadLine());
			}catch(FormatException){
				valid4 = false;
			}
			if(AorD.Equals("asc")){
				//if the array is sorted in ascending order run the interpolation search for ascending arrays
				interpolationAsc(0, f.Length-1, f, searchFor, found, ref numSteps);
			}else{
				//if the array is sorted in descending order run the interpolation search for descending arrays
				interpolationDes(0, f.Length-1, f, searchFor, found, ref numSteps);	
			}
		}else{
			Console.WriteLine("Invalid input");
			valid4 = false;
		}
			
		}
			//prints the number of steps it took to find the value
			Console.WriteLine("Steps to find value: " + numSteps);
	}else if(toSearch.Equals("N")){
		valid3 = true;
		Console.WriteLine("Thank you for using this application");
	}else{
		valid3 = false;
		Console.WriteLine("Invalid input");
	}
	}
	}
	
	//function to read text file to array
	public static double[] readFile(string name){
		//reads all lines into a string array
		String[] stringArray = System.IO.File.ReadAllLines(name);
		int length = stringArray.Length;
		//creates a double array of the same size as the string array
		double[] fileArray = new double[length];
		//fills in the double array with the values from the string array
		try{
		for(int i = 0;i < length;i++){
			fileArray[i] = Convert.ToDouble(stringArray[i]);
		}
		}catch(FormatException){
		}
		return fileArray;
	}
	
	//menu for which array the user wants to analyse 
	public static String arrayMenu(){
		String userSelection = "";
		//prints the choices for the arrays
		int choice = 0;
		bool valid = false;
		while(valid == false){
		Console.WriteLine("Which array would you like to analyse:");
		Console.WriteLine("1 - Change");
		Console.WriteLine("2 - Close");
		Console.WriteLine("3 - High");
		Console.WriteLine("4 - Low");
		Console.WriteLine("5 - Open");
		Console.WriteLine("6 - Merged Close and High");
		
		try{
		choice = Int32.Parse(Console.ReadLine());
		valid = true;
		}catch(FormatException){
			valid = false;
		}
		//switch statement to get which array needs to be analysed
		switch(choice){
			case 1:
				userSelection = "Change";
				valid = true;
				break;
			case 2:
				userSelection = "Close";
				valid = true;
				break;	
			case 3:
				userSelection = "High";
				valid = true;
				break;
			case 4:
				userSelection = "Low";
				valid = true;
				break;
			case 5:
				userSelection = "Open";
				valid = true;
				break;
			case 6:
				userSelection = "CloseHigh";
				valid = true;
				break;
			default:
				valid = false;
				break;
		}
		}
			return userSelection;
	}
	
	//menu for selecting which sorting method to use
	public static String sortMenu(){
		int choice = 0;
		bool valid = false;
		String sortSelection = "";
		while(valid == false){
		Console.WriteLine("Select sorting method:");
		Console.WriteLine("1.Merge");
		Console.WriteLine("2.Quick");
		Console.WriteLine("3.Bubble");
		Console.WriteLine("4.Heap");
		
		try{
		choice = Int32.Parse(Console.ReadLine());
		}catch(FormatException){
			valid = false;
		}
		
		switch(choice){
			case 1:
				sortSelection = "merge";
				valid = true;
				break;
			case 2:
				sortSelection = "quick";
				valid = true;
				break;	
			case 3:
				sortSelection = "bubble";
				valid = true;
				break;
			case 4:
				sortSelection = "heap";
				valid = true;
				break;
			default:
				valid = false;
				break;
		}
		}
		return sortSelection;
	}
	
	//function thats prints the selcted array
	public static void printArray(double[] a){
		String array = "";
		array = array + "[";
		int breakint = 0;
		foreach (double d in a){
			array = array + d + ", ";
			breakint++;
			if(breakint == 16){
				array = array + "\n";
				breakint = 0;
			}
        }
		array = array.Substring(0, array.Length-3);
		array = array + "]";
		Console.WriteLine(array);
	}
	//merge sort function
	public static void mergeSort(double[] numbers, int left, int right, string dir, ref int count){

            int mid;
			//creates the arrays to be merged 
            if (right > left){
				mid = (right + left) / 2;
				mergeSort(numbers, left, mid, dir, ref count);
				count++;
				mergeSort(numbers, (mid + 1), right, dir, ref count);
				count++;
				//runs merge function
                merge(numbers, left, (mid + 1), right, dir, ref count);
				count++;
            }
        }
		
		public static void merge(double[] numbers, int left, int mid, int right, string dir, ref int count){
			
			//creates new temp array
            double[] temp = new double[numbers.Length];
            int i, eol, num, pos;
            eol = (mid - 1);
            pos = left;
            num = (right - left + 1);
			//swaps the values if the array is being sorted in ascending order
			if(dir.Equals("asc")){

            while ((left <= eol) && (mid <= right)){
                if (numbers[left] <= numbers[mid]){
                    temp[pos++] = numbers[left++];
					
				}else{
                   temp[pos++] = numbers[mid++];
				}
				count++;
            }
			//swaps the values if the array is being sorted in descending order
			}else if(dir.Equals("des")){
				while ((left <= eol) && (mid <= right)){
                if (numbers[left] >= numbers[mid]){
                    temp[pos++] = numbers[left++];
					
                }else{
                   temp[pos++] = numbers[mid++];
				}
				count++;
            }
				
			}else{
				
			}
			//fills in teh rest of the array
            while (left <= eol)
                temp[pos++] = numbers[left++];
				count++;

            while (mid <= right)
                temp[pos++] = numbers[mid++];
			count++;

            for (i = 0; i < num; i++){
                numbers[right] = temp[right];
                right--;
				count++;
            }

        }
		//quicksort function
		public static void quickSort(double[] unsorted, String dir, ref int count){
			quick(unsorted, 0, unsorted.Length - 1, dir, ref count);
		}
		
		public static void quick(double[] data, int left, int right, String dir, ref int count){
			int i, j;
			double pivot, temp;
			i = left;
			j = right;
			//creates the pivot value 
			pivot = data[(left + right) / 2];
			//swaps the values for the ascending sort
			if(dir.Equals("asc")){
				
			do{
				//checks whether the value is smaller than the pivot
				while ((data[i] < pivot) && (i < right)) i++;
				//checks whether the value is larger than the pivot
					while ((pivot < data[j]) && (j > left)) j--;
						if (i <= j){
						temp = data[i];
						data[i] = data[j];
						data[j] = temp;
						i++;
						j--;
						count++;
						}
						count++;
					} while (i <= j);
					
			//swaps the values for a descending sort
			}else if(dir.Equals("des")){
				
				do{
					//checks whether the value is smaller than the pivot
				while ((data[i] > pivot) && (i < right)) i++;
					//checks whether the value is larger than the pivot
					while ((pivot > data[j]) && (j > left)) j--;
						if (i <= j){
						temp = data[i];
						data[i] = data[j];
						data[j] = temp;
						i++;
						j--;
						count++;
						}
						count++;
					} while (i <= j);
			}
			//recursively runs the function	and finishes sorting the rest of the values	
			if (left < j){
				count++;
				quick(data, left, j, dir, ref count);
				
			}
			//recursively runs the function	and finishes sorting the rest of the values	
			if (i < right){
				count++;
				quick(data, i, right, dir, ref count);
				
			}
		}
		
		//bubble sort function
		public static void bubbleSort(double[] a, String dir, ref int count){
			//iterates through array
			for (int i = 0; i < a.Length; i++){
				for (int j = 0; j < a.Length - i - 1; j++){
					//swap for ascending sort
					if(dir.Equals("asc")){
					//if value is bigger than the next swap them
					if (a[j] > a[j + 1]){
						double temp = a[j];
						a[j] = a[j + 1];
						a[j + 1] = temp;
						
					}
					count++;
					//swap for descending sort
					}else if(dir.Equals("des")){
						//if value is bigger than the next swap them
						if (a[j] < a[j + 1]){
						double temp = a[j];
						a[j] = a[j + 1];
						a[j + 1] = temp;
						
					}
					count++;
					}
				}
			}
		}
		//heap sort function
		public static void heapSort(double[] Heap, String dir, ref int count){
 
            int HeapSize = Heap.Length;
			int i; 
			//builds heap
			for (i = (HeapSize - 1) / 2; i >= 0; i--){
				heapify(Heap, HeapSize, i, dir, ref count);
				count++;
				}                 
 
			//performs sort
            for (i = Heap.Length - 1; i > 0; i--){
				double temp = Heap[i];
				Heap[i] = Heap[0];
				Heap[0] = temp; 
                HeapSize--;	
				heapify(Heap, HeapSize, 0, dir, ref count);
				count++;
				}
			}
		//heapify function
		 private static void heapify(double[] Heap, int HeapSize, int Index, String dir, ref int count){
			 int Left = (Index + 1) * 2 - 1;
			 int Right = (Index + 1) * 2;
			 int largest = 0; 
			 //runs swap for ascending sort
			 if(dir.Equals("asc")){
 
            if (Left < HeapSize && Heap[Left] > Heap[Index]){
				largest = Left;
				}else{
					largest = Index;
					}               
			count++;
            if (Right < HeapSize && Heap[Right] > Heap[largest]){
				largest = Right;
				}  
			count++;
			//runs swap for descending sort
			 }else if(dir.Equals("des")){
				 
				 if (Left < HeapSize && Heap[Left] < Heap[Index]){
				largest = Left;
				}else{
					largest = Index;
					}               
			count++;
            if (Right < HeapSize && Heap[Right] < Heap[largest]){
				largest = Right;
				}
				
			 }
			count++;
            if (largest != Index){
				double temp = Heap[Index];
				Heap[Index] = Heap[largest];
				Heap[largest] = temp;
				heapify(Heap, HeapSize, largest, dir, ref count);
				count++;
				}
			}
	
	//binary search for arrays sorted in ascending order
	public static void binarySearchAsc(int min, int max, double[] a, double target, List<int> locations, ref int numSteps){
			int mid = 0;
			int x = 0;
			double nearest = 1000;
			//Checks if target value is inside minimum and maximum values
			if(target >= a[min] && target <= a[max]){
				while(min <= max){
				//creates middle value
				mid = min + ((max-min)/2);
				numSteps++;
				//if target value is the middle value
				if(target == a[mid]){
					locations.Add(mid);
					numSteps++;
					x = mid-1;
					//looks through the array to the left to find other instances of the target value
						if(x >= 0){
						while(a[x] == target && x >= 0){
						locations.Add(x);
						x--;
						numSteps++;
						}
						}
					x = mid+1;
					//looks through the array to the right to find other instances of the target value
						if(x < a.Length){
						while(a[x] == target && x < a.Length){
						//Console.WriteLine(x);
						locations.Add(x);
						x++;
						numSteps++;
						}
						}
						
						break;
				//if the target is bigger than the middle change min value
				}else if(target > a[mid]){
					min = mid+1;
					//if the target is smaller than the middle change max value
				}else if(target < a[mid]){
					max = mid-1;
				}
				
				numSteps++;
			}
			}
			//set nearest value to minimum if target is too small
			if(target < a[min]){
				nearest = a[min];
				//set nearest value to maximum if target is too big 
			}else if(target > a[max]){
				nearest = a[max];
			}else{
				nearest = a[mid];
			}
			numSteps++;
			String message = "";
			//if no values have been found
			if(locations.Count == 0){
				//print error message that value not found but will use nearest value
				Console.WriteLine("The value " + target + " is unable to be found");
				Console.WriteLine("The value " + nearest + " will be used instead as it is the closest value");
				Console.WriteLine();
				numSteps++;
				//run search function again but with nearest value 
				binarySearchAsc(0, a.Length-1, a, nearest, locations, ref numSteps); 
				
			}else{
				//print locations of value to the user 
				message = "The value " + target + " was found at location(s)" ;
				foreach(int i in locations){
					message = message + " " + i + ",";
				}
				message = message.Remove(message.Length-1);
				Console.WriteLine(message);
			}
		}
	//binary search for arrays sorted in descending order
	public static void binarySearchDes(int min, int max, double[] a, double target, List<int> locations, ref int numSteps){
			int mid = 0;
			int x = 0;
			double nearest = 1000;
			//Checks if target value is inside minimum and maximum values
			if(target <= a[min] && target >= a[max]){
				while(min <= max){
				//creates middle value
				mid = min + ((max-min)/2);
				numSteps++;
				//if target value is the middle value
				if(target == a[mid]){
					
					locations.Add(mid);
					numSteps++;
					x = mid-1;
					//looks through the array to the left to find other instances of the target value
						if(x >= 0){
						while(a[x] == target && x >= 0){
						
						locations.Add(x);
						x--;
						numSteps++;
						}
						}
					x = mid+1;
					//looks through the array to the right to find other instances of the target value
						if(x < a.Length){
						while(a[x] == target){
						
						locations.Add(x);
						x++;
						numSteps++;
						}
						}
						break;
				//if the target is smaller than the middle change min value
				}else if(target < a[mid]){
					min = mid+1;
					//if the target is bigger than the middle change max value
				}else if(target > a[mid]){
					max = mid-1;
				}
				numSteps++;
			}
			}
			//set nearest value to minimum if target is too big
			if(target > a[min]){
				nearest = a[min];
				//set nearest value to maximum if target is too small
			}else if(target < a[max]){
				nearest = a[max];
			}else{
				nearest = a[mid];
			}
			numSteps++;
			String message = "";
			//if no values have been found
			if(locations.Count == 0){
				//print error message that value not found but will use nearest value
				Console.WriteLine("The value " + target + " is unable to be found");
				Console.WriteLine("The value " + nearest + " will be used instead as it is the closest value");
				Console.WriteLine();
				numSteps++;
				//run search function again but with nearest value 
				binarySearchDes(0, a.Length-1, a, nearest, locations, ref numSteps); 
			}else{
				
				message = "The value " + target + " was found at location(s)" ;
				foreach(int i in locations){
					message = message + " " + i + ",";
				}
				//print locations of value to the user 
				message = message.Remove(message.Length-1);
				Console.WriteLine(message);
			}
		}
//interpolation search for ascending arrays
public static void interpolationAsc(int p, int n, double[] S, double target, List<int> l, ref int numSteps) {
		int low, high;
		int mid = -1;
		double denominator; 
		low = p; 
		high = n; 
		int i = -1; 
		int x = 0;	
		double nearest = 1000;
		//Checks if target value is inside minimum and maximum values
		if (S[low] <= target && target <= S[high]){ 
			while (low <= high && i==-1) {
				denominator = S[high] - S[low]; 
				if (denominator == 0) {
					mid = low;
					
				}else {
					//creates a more accurate value for mid than the binary search 
					mid = Convert.ToInt32(Math.Floor(low + (((target-S[low])*(high-low))/denominator)));
					
				}
				numSteps++;
				//if the target value is the middle value
				if (target==S[mid]){ 
					i = mid; 
					l.Add(i);
					
					
					x = mid-1;
						//looks through the array to the left to find other instances of the target value
						if(x >= 0){
						while(S[x] == target && x >= 0 ){
						
						l.Add(x);
						x--;
						numSteps++;
						}
						}
					x = mid+1;
					//looks through the array to the right to find other instances of the target value
						if(x< S.Length){
						while(S[x] == target && x < S.Length){
						
						l.Add(x);
						x++;
						numSteps++;
						}
						}
						break;
					//if target is smaller than middle change high value 
				}else if (target < S[mid]) {
					high = mid - 1;
					//if target is larger than middle change low value 
				}else {
					low = mid + 1;
				}

				numSteps++;
			}
		}
		//if target is smaller than the smallest value nearest is min value
		if(target < S[low]){
				nearest = S[low];
				//if target is bigger than the largest value nearest is max value
			}else if(target > S[high]){
				nearest = S[high];
			}else{
				nearest = S[mid];
			}
			numSteps++;
			String message = "";
			//if no values are found
			if(l.Count == 0){
				//print message that target could not be found but its nearest value will be used
				Console.WriteLine("The value " + target + " is unable to be found");
				Console.WriteLine("The value " + nearest + " will be used instead as it is the closest value");
				Console.WriteLine();
				numSteps++;
				//run function again but with nearest value
				interpolationAsc(0, S.Length-1, S, nearest, l, ref numSteps); 
				
			}else{
				//print locations of where value was found
				message = "The value " + target + " was found at location(s)" ;
				foreach(int q in l){
					message = message + " " + q + ",";
				}
				message = message.Remove(message.Length-1);
				Console.WriteLine(message);
			}
		}

		public static void interpolationDes(int p, int n, double[] S, double target, List<int> l, ref int numSteps) {
		int low, high;
		int mid = -1;
		double denominator; 
		low = p; 
		high = n; 
		int i = -1; 
		int x = 0;	
		double nearest = 1000;
		//Checks if target value is inside minimum and maximum values
		if (S[low] >= target && target >= S[high]){ 
			while (low <= high && i==-1) {
				denominator = S[high] - S[low]; 
				if (denominator == 0) {
					mid = low;
					
				}else {
					//creates a more accurate value for mid than the binary search 
					mid = Convert.ToInt32(Math.Floor(low + ((target-S[low])*(high-low))/denominator));
				}
				numSteps++;
				//if the target value is the middle value
				if (target==S[mid]){ 
					i = mid; 
					l.Add(i);
					
					x = mid-1;
						//looks through the array to the left to find other instances of the target value
						if(x >= 0){
						while(S[x] == target && x >= 0 ){
						
						l.Add(x);
						x--;
						numSteps++;
						}
						}
					x = mid+1;
					//looks through the array to the right to find other instances of the target value
						if(x< S.Length){
						while(S[x] == target && x < S.Length){
						l.Add(x);
						x++;
						numSteps++;
						}
						}
						break;
					//if target is bigger than middle change high value 
				}else if (target>S[mid]) {
					high = mid - 1;
					//if target is smaller than middle change low value
				}else {
					low = mid + 1;
				}
				
				numSteps++;
			}
		}
		//if target is larger than the largest value nearest is low value
		if(target > S[low]){
				nearest = S[low];
				//if target is smaller than the smallest value nearest is high value
			}else if(target < S[high]){
				nearest = S[high];
			}else{
				nearest = S[mid];
			}
			numSteps++;
			String message = "";
			//if no values are found
			if(l.Count == 0){
				//print message that target could not be found but its nearest value will be used
				Console.WriteLine("The value " + target + " is unable to be found");
				Console.WriteLine("The value " + nearest + " will be used instead as it is the closest value");
				Console.WriteLine();
				numSteps++;
				//run function again but with nearest value
				interpolationDes(0, S.Length-1, S, nearest, l, ref numSteps); 
				
			}else{
				message = "The value " + target + " was found at location(s)" ;
				foreach(int q in l){
					message = message + " " + q + ",";
				}
				//print locations of where value was found
				message = message.Remove(message.Length-1);
				Console.WriteLine(message);
			}
			}
}
	