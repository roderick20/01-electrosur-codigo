package com.agilecorp.electrosur;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.HashMap;
import java.util.Map;

public class Login extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        TextView email = (TextView)this.findViewById(R.id.tv_email);
        email.setText("roderick20@hotmail.com");
        TextView password = (TextView)this.findViewById(R.id.tv_password);
        password.setText("123456");
    }

    public void login(View view) {
        final Intent intent = new Intent(this, MainActivity.class);

        TextView email = (TextView)this.findViewById(R.id.tv_email);
        TextView password = (TextView)this.findViewById(R.id.tv_password);

        RequestQueue queue = Volley.newRequestQueue(this);
        String url = Singleton.getInstance().getUrl()+"api/login";

        Map<String, String> params = new HashMap<String, String>();
        params.put("UserName", email.getText().toString());
        params.put("Password", password.getText().toString());

        Singleton.getInstance().setEmail(email.getText().toString());

        JSONObject jsonObj = new JSONObject(params);

        JsonObjectRequest jsonObjRequest = new JsonObjectRequest
                (Request.Method.POST, url, jsonObj, new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        try {

                            if(response.getString("estado").equals("OK")){
                                Singleton.getInstance().setToken(response.getString("token"));
                                Singleton.getInstance().setName(response.getString("usrNombre"));
                                Singleton.getInstance().setUniqueId(response.getString("usruniqueId"));

                                startActivity(intent);
                            }
                            else{
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

    public void registro(View view) {
        final Intent intent = new Intent(this, Registro.class);
        startActivity(intent);
    }

    public void recuperar_contrasena(View view) {
        final Intent intent = new Intent(this, RecuperarContrasena.class);
        startActivity(intent);
    }
}