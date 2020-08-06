package com.agilecorp.electrosur;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ArrayAdapter;
import android.widget.ListView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.AuthFailureError;
import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

public class HistorialActivity extends AppCompatActivity {

    private ListView listview;
    private ArrayList<Recibo> recibos = new ArrayList<Recibo>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_historial);
    }

    public void buscar(View view) {

        final TextView tv_anyo = (TextView)this.findViewById(R.id.tv_anyo);
        final Spinner sp_mes = (Spinner) findViewById(R.id.sp_mes);
        listview = (ListView) this.findViewById(R.id.listview_historial);

        String mes_texto = sp_mes.getSelectedItem().toString();
        int mes_numero = 0;
        switch (mes_texto){
            case "Enero" : mes_numero = 1; break;
            case "Febrero" : mes_numero = 2; break;
            case "Marzo" : mes_numero = 3; break;
            case "Abril" : mes_numero = 4; break;
            case "Mayo" : mes_numero = 5; break;
            case "Junio" : mes_numero = 6; break;
            case "Julio" : mes_numero = 7; break;
            case "Agosto" : mes_numero = 8; break;
            case "Setiembre" : mes_numero = 9; break;
            case "Octubre" : mes_numero = 10; break;
            case "Noviembre" : mes_numero = 11; break;
            case "Diciembre" : mes_numero = 12; break;
        }


        RequestQueue queue = Volley.newRequestQueue(getApplicationContext());
        Map<String, String> params = new HashMap<String, String>();
        params.put("usruniqueid", Singleton.getInstance().getUniqueId());
        params.put("anyo", tv_anyo.getText().toString());
        params.put("mes", String.valueOf(mes_numero));

        String url = Singleton.getInstance().getUrl() + "api/historial";

        JSONObject jsonObj = new JSONObject(params);

        JsonObjectRequest jsonObjRequest = new JsonObjectRequest
                (Request.Method.POST, url, jsonObj, new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        try {

                            if (response.getString("estado").equals("OK")) {


                                JSONArray jrecibos = response.getJSONArray("recibos");
                                for (int i = 0; i < jrecibos.length(); i++) {
                                    JSONObject object = jrecibos.getJSONObject(i);

                                    Recibo recibo = new Recibo();


                                    recibo.setCodigoComprobante(object.getString("pagcodigoComprobante"));//
                                    recibo.setCodigoSuministro(object.getString("opesuministro"));//
                                    //recibo.setNombreCliente(object.getString("nombreCliente"));
                                    //recibo.setFechaEmision(object.getString("fechaEmision"));
                                    recibo.setPagcreado(object.getString("pagcreado"));//
                                    recibo.setPagbrand(object.getString("pagbrand"));
                                    recibo.setPagcard(object.getString("pagcard"));
                                    recibo.setMontoAPagarConsulta("S/. "+object.getString("opemonto"));//
                                    recibo.setPagdescription(object.getString("pagdescription"));
                                    //recibo.set(object.getString("purcharseNumber"));

                                    recibos.add(recibo);
                                }

                                ListItemHistorial myAdapter = new ListItemHistorial(getApplicationContext(), R.layout.list_item_historial, recibos);
                                listview.setAdapter(myAdapter);
                            } else {
                                Toast.makeText(getApplicationContext(), "Error: " + response.getString("mensaje"), Toast.LENGTH_SHORT).show();
                            }

                        } catch (JSONException e) {
                            e.printStackTrace();
                        }
                    }
                },
                        new Response.ErrorListener() {
                            @Override
                            public void onErrorResponse(VolleyError error) {
                                Toast.makeText(getApplicationContext(), error.toString(), Toast.LENGTH_SHORT).show();
                            }
                        }) {
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

    public void cambioContrasena(View view) {
        final Intent intent = new Intent(this, CambioContrasenaActivity.class);
        startActivity(intent);
    }
}