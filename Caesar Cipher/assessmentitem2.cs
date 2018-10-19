using System;
using System.IO;

class assessmentitem2{

static void Main (string [] args){
	
string [] letters  = {"a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z"}; 	

int x;
String tocode = "we attack at dawn";
String coded = "";
String todecode = "fn jan anjmh oxa hxda xamnab";
String decoded = "";
int tempindex= 0;

x = 7;
for(int i = 0;i < tocode.Length;i++){
	for(int j = 0; j < 26; j++){
		if(Convert.ToString(tocode[i]).Equals(letters[j])){
			tempindex = j + x;
			if(tempindex > 25){
				tempindex -= 26;
			}
			coded = coded + letters[tempindex];
		}
	}
}
Console.WriteLine("x = " + x);
Console.WriteLine("Plaintext: " + tocode);
Console.WriteLine("Encoded text: " + coded);	
x = 9;
for(int p = 0;p < todecode.Length;p++){
	for(int q = 0; q < 26; q++){
		if(Convert.ToString(todecode[p]).Equals(letters[q])){
			tempindex = q - x;
			if(tempindex < 0){
				tempindex += 26;
			}
			decoded = decoded + letters[tempindex];
		}
	}
}
Console.WriteLine();
Console.WriteLine("x = " + x);
Console.WriteLine("Encoded text: " + todecode);
Console.WriteLine("Plaintext: " + decoded);

String s = (File.ReadAllText("codedtext.txt")).ToLower();
	//Console.WriteLine(s);
	int count = 0;
	int maxfreq = 0;
	String most  = "";
	int mostindex = 0;
	decoded = "";
	for(int i = 0; i < 26;i++){
		count = 0;
		for(int j = 0;j < s.Length;j++){
			if(Convert.ToString(s[j]).Equals(letters[i])){
				count += 1;
			}
		}
		//Console.WriteLine(letters[i] + " appears " + count + " times");
		if (count > maxfreq){
			maxfreq = count;
			most = letters[i];
			mostindex = i;
		}
	}
	
	Console.WriteLine();
	Console.WriteLine("The most frequent letter is " + most + " with a frequency of " + maxfreq);
	x = mostindex - 4;
	Console.WriteLine("Shift is " + x);
	tempindex = 0;
	for(int a = 0; a < s.Length;a++){
		for(int b = 0;b < 26;b++){
			if(Convert.ToString(s[a]).Equals(letters[b])){
				tempindex = b - x;
				if(tempindex < 0){
					tempindex += 26;
				}
				decoded = decoded + letters[tempindex];
			}
		}
		if(Convert.ToString(s[a]).Equals(" ")){
				decoded = decoded + " ";}
}
Console.WriteLine(decoded);
        Console.ReadLine();
}
}
