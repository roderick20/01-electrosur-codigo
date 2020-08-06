package com.agilecorp.electrosur;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.TextView;

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

public class ResultadoMalInvitadoActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_resultadomal_invitado);

        final Intent intent = new Intent(this, InvitadoActivity.class);
        startActivity(intent);

        TextView tv_descripcion = (TextView) this.findViewById(R.id.tv_descripcion);
        tv_descripcion.setText(Singleton.getInstance().getDescripcion());

        TextView tv_transaccion = (TextView) this.findViewById(R.id.tv_transaccion);
        tv_transaccion.setText("Número de transacción:" + Singleton.getInstance().getTrasaccion());

        Date now = new Date();
        String fecha = android.text.format.DateFormat.format("yyyy-MM-dd_hh:mm:ss", now).toString();

        TextView tv_fecha = (TextView) this.findViewById(R.id.tv_fecha);
        tv_fecha.setText("Fecha: " + fecha);

    }

    public void invitado(View view) {
        final Intent intent = new Intent(this, InvitadoActivity.class);
        startActivity(intent);
    }
}