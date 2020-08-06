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

public class CambioContrasenaActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_cambio_contrasena);
    }

    public void grabar(View view) {
        final TextView tv_contrasena = (TextView) this.findViewById(R.id.tv_contrasena);
        final TextView captcha = (TextView)this.findViewById(R.id.tv_captcha);

        if(tv_contrasena.getText().toString().equals("")){
            Toast.makeText(getApplicationContext(), "Ingrese correo", Toast.LENGTH_LONG).show();
            return;
        }

        Map<String, String> params = new HashMap<String, String>();
        params.put("contrasena", tv_contrasena.getText().toString());
        params.put("usruniqueid", Singleton.getInstance().getUniqueId());

        RequestQueue queue = Volley.newRequestQueue(this);
        String url = Singleton.getInstance().getUrl() + "api/cambiocontrasena";
        JSONObject jsonObj = new JSONObject(params);

        JsonObjectRequest jsonObjRequest = new JsonObjectRequest
                (Request.Method.POST, url, jsonObj, new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        try {
                            if (response.getString("estado").equals("OK")) {
                                tv_contrasena.setText("");
                                Toast.makeText(getApplicationContext(), "Se contraseña actualizada ", Toast.LENGTH_LONG).show();
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
                        }){
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                Map<String, String> params = new HashMap<String, String>();
                params.put("Authorization", "Bearer " + Singleton.getInstance().getToken());
                return params;
            }
        };;
        queue.add(jsonObjRequest);
    }

    public void inicio(View view) {
        final Intent intent = new Intent(this, MainActivity.class);
        startActivity(intent);
    }

    public void pagar(View view) {
        final Intent intent = new Intent(this, PagarActivity.class);
        startActivity(intent);
    }

    public void historial(View view) {
        final Intent intent = new Intent(this, HistorialActivity.class);
        startActivity(intent);
    }

    public void cambioContrasena(View view) {
        final Intent intent = new Intent(this, CambioContrasenaActivity.class);
        startActivity(intent);
    }
}