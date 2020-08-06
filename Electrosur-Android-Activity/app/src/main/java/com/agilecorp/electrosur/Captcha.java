package com.agilecorp.electrosur;

import java.util.List;
import java.util.Random;

import android.graphics.Bitmap;
import android.graphics.Color;

public abstract class Captcha {
	protected Bitmap image;
	protected String answer = "";
	private int width;
	private int height;
	protected int x = 0;
	protected int y = 0;
	protected static List<Integer> usedColors;
	
	protected abstract Bitmap image();

	public static int  LetraColor()
	{
		Random rand = new Random();
		int redlow = 160, greenLow = 100, blueLow = 160;
		return Color.rgb(rand.nextInt(redlow), rand.nextInt(greenLow), rand.nextInt(blueLow));
	}

	public static int color(){
		Random rand = new Random();

		int low = 180, high = 255;

		int nRend = rand.nextInt(high) % (high - low) + low;
		int nGreen = rand.nextInt(high) % (high - low) + low;
		int nBlue = rand.nextInt(high) % (high - low) + low;

		return Color.rgb(nRend, nGreen, nBlue);
	}
    
    public int getWidth(){
    	return this.width;
    }
    
    public void setWidth(int width){
    	if(width > 0 && width < 10000){
    		this.width = width;
    	}else{
    		this.width = 300;
    	}
    }
    
    public int getHeight(){
    	return this.height;
    }
    
    public void setHeight(int height){
    	if(height > 0 && height < 10000){
    		this.height = height;
    	}else{
    		this.height = 100;
    	}
    }
    
	public Bitmap getImage() {
		return this.image;
	}

	public boolean checkAnswer(String ans) {
		return ans.equals(this.answer);
	}
}
