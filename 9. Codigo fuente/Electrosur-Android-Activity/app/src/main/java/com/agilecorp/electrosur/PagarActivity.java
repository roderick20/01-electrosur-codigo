package com.agilecorp.electrosur;

import androidx.appcompat.app.AppCompatActivity;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;

import androidx.annotation.Nullable;

import android.view.View;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
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
import java.util.Map;

import lib.visanet.com.pe.visanetlib.VisaNet;
import lib.visanet.com.pe.visanetlib.presentation.custom.VisaNetViewAuthorizationCustom;

public class PagarActivity extends AppCompatActivity {

    private ListView listview;
    private ArrayList<Recibo> recibos = new ArrayList<Recibo>();

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_pagar);

        final EditText tvNumeroContrato = this.findViewById(R.id.NumeroContrato);
        final Button botonBuscar = (Button) this.findViewById(R.id.btnSearch);
        listview = (ListView) this.findViewById(R.id.listview);

        botonBuscar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if (tvNumeroContrato.getText().toString().equals("")) {
                    Toast.makeText(getApplicationContext(), "Ingrese número de contrato", Toast.LENGTH_SHORT).show();
                    return;
                }

                RequestQueue queue = Volley.newRequestQueue(getApplicationContext());

                JSONObject jsonObject = new JSONObject();
                String jsonString = "";

                Crypto cryto = new Crypto();
                try {
                    jsonObject.put("codigocliente", tvNumeroContrato.getText().toString());
                    jsonObject.put("usruniqueid", Singleton.getInstance().getUniqueId());
                    jsonString = jsonObject.toString();
                    jsonString = cryto.encrypt(jsonString, Singleton.getInstance().getClaveSecretaMovil());
                } catch (JSONException e) {
                    e.printStackTrace();
                }


                Map<String, String> params = new HashMap<String, String>();
                params.put("d", jsonString);
                String url = Singleton.getInstance().getUrl() + "api/Pagos";

                JSONObject paramsJsonObject = new JSONObject(params);
                recibos = new ArrayList<Recibo>();

                ListItemPagos myAdapter = new ListItemPagos(getApplicationContext(), R.layout.list_item_pagos, recibos);
                listview.setAdapter(myAdapter);

                JsonObjectRequest jsonObjRequest = new JsonObjectRequest
                        (Request.Method.POST, url, paramsJsonObject, new Response.Listener<JSONObject>() {
                            @Override
                            public void onResponse(JSONObject response) {
                                try {

                                    recibos = new ArrayList<Recibo>();

                                    if (response.getString("estado").equals("OK")) {

                                        Singleton.getInstance().setCodigoCliente(tvNumeroContrato.getText().toString());

                                        JSONArray jrecibos = response.getJSONArray("recibos");
                                        for (int i = 0; i < jrecibos.length(); i++) {
                                            JSONObject object = jrecibos.getJSONObject(i);

                                            Recibo recibo = new Recibo();

                                            recibo.setCodigoComprobante(object.getString("codigoComprobante"));
                                            recibo.setCodigoSuministro(object.getString("codigoSuministro"));
                                            recibo.setNombreCliente(object.getString("nombreCliente"));
                                            recibo.setFechaEmision(object.getString("fechaEmision"));

                                            if(object.getString("fechaVencimiento").equals("")){
                                                recibo.setFechaVencimiento("Recibo vencido");
                                            }
                                            else{
                                                recibo.setFechaVencimiento(object.getString("fechaVencimiento"));
                                            }

                                            recibo.setDetalleConsulta(object.getString("detalleConsulta"));
                                            recibo.setMontoAPagarConsulta(object.getString("montoAPagarConsulta"));
                                            recibo.setDireccionCliente(object.getString("direccionCliente"));
                                            recibo.setPURCHASE_NUMBER(object.getString("purcharseNumber"));
                                            recibo.setIdentificadorEntidadConsulta(object.getString("identificadorEntidadConsulta"));

                                            recibos.add(recibo);
                                        }

                                        ListItemPagos myAdapter = new ListItemPagos(getApplicationContext(), R.layout.list_item_pagos, recibos);
                                        listview.setAdapter(myAdapter);
                                    } else {
                                        Toast.makeText(getApplicationContext(), response.getString("mensaje"), Toast.LENGTH_LONG).show();
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
        });

        listview.setOnItemClickListener(new AdapterView.OnItemClickListener() {
            @Override
            public void onItemClick(AdapterView<?> adapterView, View view, int position, long id) {

                Recibo recibo = recibos.get(position);

                Singleton.getInstance().setRecibo(recibo);

                Map<String, Object> data = new HashMap<>();

                data.put(VisaNet.VISANET_CHANNEL, VisaNet.Channel.MOBILE);
                data.put(VisaNet.VISANET_COUNTABLE, true);
                data.put(VisaNet.VISANET_USERNAME, Singleton.getInstance().VisaNet_USERNAME);
                data.put(VisaNet.VISANET_PASSWORD, Singleton.getInstance().VisaNet_PASSWORD);
                data.put(VisaNet.VISANET_MERCHANT, Singleton.getInstance().VisaNet_MERCHANT);
                data.put(VisaNet.VISANET_PURCHASE_NUMBER, recibo.getPURCHASE_NUMBER());//"102560" );
                data.put(VisaNet.VISANET_AMOUNT, recibo.MontoAPagarConsulta);
                data.put(VisaNet.VISANET_SHOW_AMOUNT, true);
                data.put(VisaNet.VISANET_USER_TOKEN, "Pago electrosur");

                HashMap<String, String> MDDdata = new HashMap<String, String>();
                //MDDdata.put("4", "mail@electrosur.com.pe");
                MDDdata.put("19", "LIM");
                MDDdata.put("20", "AQP");
                MDDdata.put("21", "AFKI345");
                MDDdata.put("94", "ABC123DEF");
                data.put(VisaNet.VISANET_MDD, MDDdata);

                data.put(VisaNet.VISANET_ENDPOINT_URL, Singleton.getInstance().VisaNetVISANETENDPOINTURL());
                VisaNetViewAuthorizationCustom custom = new VisaNetViewAuthorizationCustom();
                custom.setLogoTextMerchant(true);
                custom.setLogoTextMerchantText("Electrosur");
                custom.setLogoTextMerchantTextColor(R.color.colorAzulOscuro);
                custom.setLogoTextMerchantTextSize(50);
                custom.setLogoImage(R.drawable.ic_logo);
                custom.setButtonColorMerchant(R.color.visanet_red);
                custom.setInputCustom(true);

                try {
                    VisaNet.authorization(PagarActivity.this, data, custom);
                } catch (Exception e) {
                    Toast.makeText(getApplicationContext(), "Error: " + e.getMessage(), Toast.LENGTH_LONG).show();
                }
            }
        });


    }

    @Override
    public void onActivityResult(int requestCode, int resultCode, @Nullable Intent data) {
        super.onActivityResult(requestCode, resultCode, data);
        if (requestCode == VisaNet.VISANET_AUTHORIZATION) {
            if (data != null) {
                if (resultCode == Activity.RESULT_OK) {
                    String JSONString = data.getExtras().getString("keySuccess");

                    try {
                        JSONObject obj = new JSONObject(JSONString);
                        JSONObject objdataMap = obj.getJSONObject("dataMap");

                        Singleton.getInstance().setDescripcion(objdataMap.getString("ACTION_DESCRIPTION"));
                        Singleton.getInstance().setTrasaccion(objdataMap.getString("TRANSACTION_ID"));
                        Singleton.getInstance().setCard(objdataMap.getString("CARD"));
                        Singleton.getInstance().setBrand(objdataMap.getString("BRAND"));

                    } catch (JSONException e) {
                        e.printStackTrace();
                    }

                    final Intent intent = new Intent(this, ResultadookActivity.class);
                    startActivity(intent);

                } else {
                    String JSONString = data.getExtras().getString("keyError");
                    try {
                        JSONObject obj = new JSONObject(JSONString);
                        JSONObject objdataMap = obj.getJSONObject("data");

                        Singleton.getInstance().setDescripcion(objdataMap.getString("ACTION_DESCRIPTION"));
                        Singleton.getInstance().setTrasaccion(objdataMap.getString("TRANSACTION_ID"));
                        Singleton.getInstance().setCard(objdataMap.getString("CARD"));

                    } catch (JSONException e) {
                        e.printStackTrace();
                    }


                    final Intent intent = new Intent(this, ResultadomalActivity.class);
                    startActivity(intent);
                    /*String JSONString = data.getExtras().getString("keyError");
                    JSONString = JSONString != null ? JSONString : "";
                    Toast toast1 = Toast.makeText(getApplicationContext(), JSONString, Toast.LENGTH_LONG);
                    toast1.show();*/
                }
            } else {
                Toast toast1 = Toast.makeText(getApplicationContext(), "Cancel...", Toast.LENGTH_LONG);
                toast1.show();
            }
        }
    }


    public void inicio(View view) {
        final Intent intent = new Intent(this, MainActivity.class);
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