float r = 50;
float theta = 0;
String[] words;
int b = 1;
float x = r*cos(theta);
float y = r*sin(theta);
int z = 0;
int n = 0;
int done = 0;

void setup(){
  
  size(600,600);
  background(5, 250, 152);
  String[] lines = loadStrings("BeeMovieScript.txt");
  String total = join(lines, " ");
  words = split(total, " ");
  translate(width/2,height/2);
  
}

void draw(){
  if (done <= 100)
  {
    for (int i = 0; i < words.length; i++){
      println(words[i]);
      
      
      if(words[i].contains("bee")) {
        stroke(250,250,0);
        fill(250,250,0);
      }
      else if (words[i].length() == 1 || words[i].length() == 2 || words[i].length() == 3){
        stroke(120, 245, 100);
        fill(120, 245, 100);
      }
      else if(words[i].length() == 4 || words[i].length() == 5 || words[i].length() == 6){
        stroke(68, 232, 42);
        fill(68, 232, 42);
      }
      else if(words[i].length() == 7 || words[i].length() == 8 || words[i].length() == 9){
        stroke(59, 222, 33);
        fill(59, 222, 33);
      }
      else if(words[i].length() == 10 || words[i].length() == 11 || words[i].length() == 12){
        stroke(47, 204, 22);
        fill(47, 204, 22);
      }
      else if(words[i].length() == 13 || words[i].length() == 14){
        stroke(38, 186, 15);
        fill(38, 186, 15);
      }
      else if(words[i].length() == 15 || words[i].length() == 16){
        stroke(30, 168, 8);
        fill(30, 168, 8);
      }
      else if(words[i].length() == 17){
        stroke(23, 150, 3);
        fill(23, 150, 3);
      }
      else {
        stroke(40, 232, 9);
        fill(40, 232, 9);
      }
      
      float x = r*cos(theta);
      float y = r*sin(theta);
      ellipse(x,y,r*10%30, r*10%30);
      
      if(words[i].contains("bee")) {
        ellipse(x,y,50, 50);
      }
      
      if (z > 10000){
        
        r = 52 + n;
        theta = 2;
        x = r*cos(theta);
        y = r*sin(theta);
        z = 0;
        n = n + 1;
        
      }
      
      r += -0.1;
      theta += 0.02;
      z = z + 1;
 
      
      if (z > 8000){
        stroke(250, 15, 15);
        fill(250, 15, 15);
        ellipse(x,y,5, 5);
      }
    }
  }
  
  done = done + 1;
  
  
}
