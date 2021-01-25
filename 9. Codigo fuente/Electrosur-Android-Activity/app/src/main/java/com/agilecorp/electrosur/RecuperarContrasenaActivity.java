package com.agilecorp.electrosur;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.HurlStack;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

import javax.net.ssl.SSLSocketFactory;

public class RecuperarContrasenaActivity extends AppCompatActivity {

    String captchaValor="";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_recuperar_contrasena);

        ImageView img = (ImageView)this.findViewById(R.id.imgCaptcha);
        Captcha c = new TextCaptcha(300, 115, 4);
        img.setImageBitmap(c.image);
        this.captchaValor = c.answer;
    }

    public void refrescarCaptcha(View view) {
        ImageView img = (ImageView)this.findViewById(R.id.imgCaptcha);
        Captcha c = new TextCaptcha(300, 115, 4);
        img.setImageBitmap(c.image);
        this.captchaValor = c.answer;
    }

    public void grabar(View view) {
        final TextView tv_correo = (TextView) this.findViewById(R.id.tv_contrasena);
        final TextView captcha = (TextView)this.findViewById(R.id.tv_captcha);

        if(tv_correo.getText().toString().equals("")){
            Toast.makeText(getApplicationContext(), "Ingrese correo", Toast.LENGTH_LONG).show();
            return;
        }

        if(!this.captchaValor.equals(captcha.getText().toString())){
            Toast.makeText(getApplicationContext(), "Catpcha incorrecto", Toast.LENGTH_LONG).show();
            return;
        }

        Map<String, String> params = new HashMap<String, String>();
        params.put("correo", tv_correo.getText().toString());

        SSLSocketFactory sf = Singleton.getInstance().getSSL(getResources().openRawResource(R.raw.cet));
        RequestQueue queue = Volley.newRequestQueue(this, new HurlStack(null, sf));

        String url = Singleton.getInstance().getUrl() + "api/recuperarcontrasena";
        JSONObject jsonObj = new JSONObject(params);

        JsonObjectRequest jsonObjRequest = new JsonObjectRequest
                (Request.Method.POST, url, jsonObj, new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        try {
                            if (response.getString("estado").equals("OK")) {
                                tv_correo.setText("");
                                Toast.makeText(getApplicationContext(), "Se enviara un correo con su contraseña nueva ", Toast.LENGTH_LONG).show();
                            } else {
                                Toast.makeText(getApplicationContext(), response.getString("mensaje"), Toast.LENGTH_SHORT).show();
                            }
                        } catch (JSONException e) {
                            Toast.makeText(getApplicationContext(), e.getMessage(), Toast.LENGTH_SHORT).show();
                        }
                    }
                },
                new Response.ErrorListener() {
                    @Override
                    public void onErrorResponse(VolleyError error) {
                        Toast.makeText(getApplicationContext(), error.toString(), Toast.LENGTH_SHORT).show();
                    }
                });
        queue.add(jsonObjRequest);
    }
}