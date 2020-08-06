package com.agilecorp.electrosur;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ImageView;
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

import java.util.Date;
import java.util.HashMap;
import java.util.Map;

public class ResultadoOKInvitadoActivity extends AppCompatActivity {

    private View main;
    private ImageView imageView;

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_resultadook_invitado);

        TextView tv_descripcion = (TextView)this.findViewById(R.id.tv_descripcion);
        tv_descripcion.setText(Singleton.getInstance().getDescripcion());

        TextView tv_transaccion = (TextView)this.findViewById(R.id.tv_transaccion);
        tv_transaccion.setText("Número de transacción:"+Singleton.getInstance().getTrasaccion());

        Date now = new Date();
        String fecha = android.text.format.DateFormat.format("yyyy-MM-dd_hh:mm:ss", now).toString();

        TextView tvFecha = (TextView)this.findViewById(R.id.tv_fecha);
        tvFecha.setText("Fecha: " + fecha);

        TextView tv_contrato = (TextView)this.findViewById(R.id.tv_contrato);
        tv_contrato.setText("Número de contrato: " +Singleton.getInstance().getCodigoCliente());

        TextView tv_recibo = (TextView)this.findViewById(R.id.tv_recibo);
        tv_recibo.setText("Código de comprobante: "+Singleton.getInstance().getRecibo().getCodigoComprobante());

        TextView tv_monto = (TextView)this.findViewById(R.id.tv_monto);
        tv_monto.setText("Monto pagado: "+Singleton.getInstance().getRecibo().getMontoAPagarConsulta());

        Crypto _crypto = new Crypto();

        Map<String, String> params = new HashMap<String, String>();
        params.put("usrnumerodocumento", Singleton.getInstance().getUniqueId());
        params.put("opeidOperacion", Singleton.getInstance().getRecibo().getPURCHASE_NUMBER());
        params.put("pagcodigoCliente", Singleton.getInstance().getCodigoCliente());
        params.put("pagcodigoComprobante", Singleton.getInstance().getRecibo().getCodigoComprobante());
        params.put("opemonto", Singleton.getInstance().getRecibo().getMontoAPagarConsulta());
        params.put("opesuministro", Singleton.getInstance().getRecibo().getCodigoSuministro());
        params.put("pagbrand", Singleton.getInstance().getBrand());
        params.put("pagcard", Singleton.getInstance().getCard());
        params.put("pagdescription", Singleton.getInstance().getDescripcion());
        params.put("pagestado", "OK");
        params.put("DetalleConsulta", Singleton.getInstance().getRecibo().getDetalleConsulta());
        params.put("FechaEmision", Singleton.getInstance().getRecibo().getFechaEmision());
        params.put("FechaVencimiento", Singleton.getInstance().getRecibo().getFechaVencimiento());
        params.put("NombreCliente", Singleton.getInstance().getRecibo().getNombreCliente());
        params.put("DireccionCliente", Singleton.getInstance().getRecibo().getDireccionCliente());
        params.put("identificadorintidadionsulta", _crypto.encrypt(Singleton.getInstance().getRecibo().getIdentificadorEntidadConsulta(),
                Singleton.getInstance().getClaveSecretaMovil()));

        RequestQueue queue = Volley.newRequestQueue(this);
        String url = Singleton.getInstance().getUrl() + "api/pagoactualizarinvitado";
        JSONObject jsonObj = new JSONObject(params);

        JsonObjectRequest jsonObjRequest = new JsonObjectRequest
                (Request.Method.POST, url, jsonObj, new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        try {
                            if (response.getString("estado").equals("OK")) {
                            } else {
                                Toast.makeText(getApplicationContext(), response.getString("mensaje"), Toast.LENGTH_LONG).show();
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
                        }) ;
        queue.add(jsonObjRequest);
    }


    public void invitado(View view) {
        final Intent intent = new Intent(this, InvitadoActivity.class);
        startActivity(intent);
    }
}