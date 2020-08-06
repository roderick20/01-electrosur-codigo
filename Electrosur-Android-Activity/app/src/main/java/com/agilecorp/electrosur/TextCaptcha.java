package com.agilecorp.electrosur;

import java.io.CharArrayWriter;
import java.util.ArrayList;
import java.util.Random;

import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.LinearGradient;
import android.graphics.Paint;
import android.graphics.Shader;
import android.graphics.Bitmap.Config;

public class TextCaptcha extends Captcha {

	private int wordLength;
	private char mCh;

	public TextCaptcha(int width, int height, int wordLength){
	    setHeight(height);
	    setWidth(width);
	    usedColors = new ArrayList<Integer>();
	    this.wordLength = wordLength;
	    this.image = image();
	}

	@Override
	protected Bitmap image() {
		int color = color();
	    LinearGradient gradient = new LinearGradient(0, 0, getWidth() / this.wordLength, getHeight() / 2, color, color, Shader.TileMode.MIRROR);
	    Paint p = new Paint();
	    p.setDither(true);
	    p.setShader(gradient);
	    Bitmap bitmap = Bitmap.createBitmap(getWidth(), getHeight(), Config.ARGB_8888);
	    Canvas c = new Canvas(bitmap);
	    c.drawRect(0, 0, getWidth(), getHeight(), p);
	    Paint tp = new Paint();
	    tp.setDither(true);
	    tp.setTextSize(getWidth() / getHeight() * 40);

	    Random r = new Random(System.currentTimeMillis());
	    CharArrayWriter cab = new CharArrayWriter();
	    this.answer = "";
	    for(int i = 0; i < this.wordLength; i ++){
	        char ch = generarCodigoCaptcha();
	        cab.append(ch);
	        this.answer += ch;
	    }

	    char[] data = cab.toCharArray();
	    for (int i=0; i<data.length; i++) {
	        this.x += (30 - (3 * this.wordLength)) + (Math.abs(r.nextInt()) % (65 - (1.2 * this.wordLength)));
	        this.y = 50 + Math.abs(r.nextInt()) % 50;
	        Canvas cc = new Canvas(bitmap);
	        //tp.setTextSkewX(r.nextFloat() - r.nextFloat());
	        tp.setColor(LetraColor());
	        cc.drawText(data, i, 1, this.x, this.y, tp);
	        tp.setTextSkewX(0);
	    }
	    return bitmap;
	}

	String Letters = "2346789ABCDEFGHJKLMNPRTUVWXYZ";

	public char generarCodigoCaptcha()
	{
		Random rand = new Random();
		int maxRand = this.Letters.length() - 1;
		int index = rand.nextInt(maxRand);
		return Letters.charAt(index);
	}

}
