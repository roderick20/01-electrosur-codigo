package com.agilecorp.electrosur;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.Date;
import java.util.HashMap;
import java.util.Map;

public class ResultadomalActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_resultadomal);

        TextView tv_descripcion = (TextView) this.findViewById(R.id.tv_descripcion);
        tv_descripcion.setText(Singleton.getInstance().getDescripcion());

        TextView tv_transaccion = (TextView) this.findViewById(R.id.tv_transaccion);
        tv_transaccion.setText("Número de transacción:" + Singleton.getInstance().getTrasaccion());

        Date now = new Date();
        String fecha = android.text.format.DateFormat.format("yyyy-MM-dd_hh:mm:ss", now).toString();

        TextView tv_fecha = (TextView) this.findViewById(R.id.tv_fecha);
        tv_fecha.setText("Fecha: " + fecha);

        Map<String, String> params = new HashMap<String, String>();
        params.put("usridUsuario", Singleton.getInstance().getUniqueId());
        params.put("opeidOperacion", Singleton.getInstance().getRecibo().getPURCHASE_NUMBER());
        params.put("pagcodigoCliente", Singleton.getInstance().getCodigoCliente());
        params.put("pagcodigoComprobante", Singleton.getInstance().getRecibo().getCodigoComprobante());
        params.put("opemonto", Singleton.getInstance().getRecibo().getMontoAPagarConsulta());
        params.put("opesuministro", Singleton.getInstance().getRecibo().getCodigoSuministro());
        params.put("pagbrand", Singleton.getInstance().getBrand());
        params.put("pagcard", Singleton.getInstance().getCard());
        params.put("pagdescription", Singleton.getInstance().getDescripcion());
        params.put("pagestado", "OK");


        RequestQueue queue = Volley.newRequestQueue(this);
        String url = Singleton.getInstance().getUrl() + "api/pagoactualizar";
        JSONObject jsonObj = new JSONObject(params);

        JsonObjectRequest jsonObjRequest = new JsonObjectRequest
                (Request.Method.POST, url, jsonObj, new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        try {
                            if (response.getString("estado").equals("OK")) {
                            } else {
                            }
                        } catch (JSONException e) {
                        }
                    }
                },
                        new Response.ErrorListener() {
                            @Override
                            public void onErrorResponse(VolleyError error) {

                            }
                        }){
            @Override
            public Map<String, String> getHeaders() throws AuthFailureError {
                Map<String, String> params = new HashMap<String, String>();
                params.put("Authorization", "Bearer " + Singleton.getInstance().getToken());
                return params;
            }
        };
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