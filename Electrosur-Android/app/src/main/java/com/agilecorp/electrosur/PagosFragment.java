package com.agilecorp.electrosur;

import android.app.Activity;
import android.content.Intent;
import android.os.Bundle;

import androidx.annotation.Nullable;
import androidx.fragment.app.Fragment;

import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.AdapterView;
import android.widget.Button;
import android.widget.EditText;
import android.widget.ListView;
import android.widget.Switch;
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
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import lib.visanet.com.pe.visanetlib.VisaNet;
import lib.visanet.com.pe.visanetlib.presentation.custom.VisaNetViewAuthorizationCustom;




public class PagosFragment extends Fragment {

    public PagosFragment() {
    }

    @Override
    public void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        //Intent intent = new Intent(getContext(), MainActivity.class);
        //super.onActivityResult(1,1,null);
    }

    @Override
    public View onCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState) {
        View view = inflater.inflate(R.layout.fragment_pagos, container, false);
        return view;
    }

    private ListView listview;
    private ArrayList<Recibo> recibos = new ArrayList<Recibo>();

    @Override
    public void onViewCreated(View view, @Nullable Bundle savedInstanceState) {

        final EditText tvNumeroContrato = getView().findViewById(R.id.NumeroContrato);
        final Button botonBuscar = (Button) getView().findViewById(R.id.btnSearch);
        listview = (ListView) getView().findViewById(R.id.listview);

        botonBuscar.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View v) {
                if(tvNumeroContrato.getText().toString().equals("")){
                    Toast.makeText(getActivity(), "Ingrese número de contrato", Toast.LENGTH_SHORT).show();
                    return;
                }

                RequestQueue queue = Volley.newRequestQueue(getActivity());
                Map<String, String> params = new HashMap<String, String>();
                params.put("Codigocliente", tvNumeroContrato.getText().toString());
                String url = Singleton.getInstance().getUrl() + "api/Pagos";

                JSONObject jsonObj = new JSONObject(params);

                JsonObjectRequest jsonObjRequest = new JsonObjectRequest
                        (Request.Method.GET, url, jsonObj, new Response.Listener<JSONObject>() {
                            @Override
                            public void onResponse(JSONObject response) {
                                try {
                                    JSONArray jrecibos = response.getJSONArray("recibos");
                                    for (int i = 0; i < jrecibos.length(); i++) {
                                        JSONObject object = jrecibos.getJSONObject(i);

                                        Recibo recibo = new Recibo();

                                        recibo.setCodigoComprobante(object.getString("codigoComprobante"));
                                        recibo.setCodigoSuministro(object.getString("codigoSuministro"));
                                        recibo.setNombreCliente(object.getString("nombreCliente"));
                                        recibo.setFechaEmision(object.getString("fechaEmision"));
                                        recibo.setFechaVencimiento(object.getString("fechaVencimiento"));
                                        recibo.setDetalleConsulta(object.getString("detalleConsulta"));
                                        recibo.setMontoAPagarConsulta(object.getString("montoAPagarConsulta"));
                                        recibo.setDireccionCliente(object.getString("direccionCliente"));
                                        recibo.setPURCHASE_NUMBER(object.getString("purcharseNumber"));

                                        recibos.add(recibo);
                                    }

                                    ListItemPagos myAdapter = new ListItemPagos(getActivity(), R.layout.list_item_pagos, recibos);
                                    listview.setAdapter(myAdapter);

                                } catch (JSONException e) {
                                    e.printStackTrace();
                                }
                            }
                        },
                        new Response.ErrorListener() {
                            @Override
                            public void onErrorResponse(VolleyError error) {
                                Toast.makeText(getActivity(), error.toString(), Toast.LENGTH_SHORT).show();
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

                Map<String, Object> data = new HashMap<>();

                data.put(VisaNet.VISANET_CHANNEL, VisaNet.Channel.MOBILE);
                data.put(VisaNet.VISANET_COUNTABLE, true);
                data.put(VisaNet.VISANET_USERNAME, Singleton.getInstance().VisaNet_USERNAME);
                data.put(VisaNet.VISANET_PASSWORD, Singleton.getInstance().VisaNet_PASSWORD);
                data.put(VisaNet.VISANET_MERCHANT, Singleton.getInstance().VisaNet_MERCHANT);
                data.put(VisaNet.VISANET_PURCHASE_NUMBER, recibo.getPURCHASE_NUMBER());
                data.put(VisaNet.VISANET_AMOUNT, recibo.MontoAPagarConsulta);
                data.put(VisaNet.VISANET_SHOW_AMOUNT, true);
                //data.put(VisaNet.VISANET_USER_TOKEN, "");

                HashMap<String, String> MDDdata = new HashMap<String, String>();
                MDDdata.put("4", "mail@electrosur.com.pe");
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
                    //Intent intent = new Intent(getActivity(), MainActivity.class);
                    //getActivity().startActivity(intent);
                    getActivity().startActivityForResult(getActivity().getIntent(),VisaNet.VISANET_AUTHORIZATION);
                    //Intent intent = new Intent(Intent.ACTION_GET_CONTENT);
                    //getActivity().startActivity(getActivity().getIntent());
                    //startActivity(new Intent(getActivity(), lib.visanet.com.pe.visanetlib.presentation.presenter.VisaNetAuthorizationActivityPresenter.class));


                    VisaNet.authorization(getActivity(), data, custom);
                } catch (Exception e) {
                    Toast.makeText(getActivity(), "Error: "+ e.getMessage() , Toast.LENGTH_LONG).show();
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
                    //Toast toast1 = Toast.makeText(getApplicationContext(), JSONString, Toast.LENGTH_LONG);
                    //toast1.show();
                } else {
                    String JSONString = data.getExtras().getString("keyError");
                    JSONString = JSONString != null ? JSONString : "";
                    //Toast toast1 = Toast.makeText(getApplicationContext(), JSONString, Toast.LENGTH_LONG);
                    //toast1.show();
                }
            } else {
                //Toast toast1 = Toast.makeText(getApplicationContext(), "Cancel...", Toast.LENGTH_LONG);
                //toast1.show();
            }
        }
    }


}