package com.agilecorp.electrosur;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.view.View;
import android.widget.CheckBox;
import android.widget.ImageView;
import android.widget.Spinner;
import android.widget.TextView;
import android.widget.Toast;

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

public class RegistroActivity extends AppCompatActivity {

    String captchaValor="";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_registro);

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

    public void dialogo(View view) {
        new TerminoCondicionesDialog().show(getSupportFragmentManager(), "SimpleDialog");
    }

    public void grabar(View view) {
        Spinner sp_tipoDocumento = (Spinner) this.findViewById(R.id.sp_tipoDocumento);
        final TextView tv_numeroDocumento = (TextView) this.findViewById(R.id.tv_numeroDocumento);
        final TextView tv_nombres = (TextView) this.findViewById(R.id.tv_nombres);
        final TextView tv_apellidoPaterno = (TextView) this.findViewById(R.id.tv_apellidoPaterno);
        final TextView tv_apellidoMaterno = (TextView) this.findViewById(R.id.tv_apellidoMaterno);
        final TextView tv_correoPrimario = (TextView) this.findViewById(R.id.tv_correoPrimario);
        final TextView tv_correoSecundario = (TextView) this.findViewById(R.id.tv_correoSecundario);
        final TextView tv_numeroTelefono = (TextView) this.findViewById(R.id.tv_numeroTelefono);
        final TextView tv_contrasena = (TextView) this.findViewById(R.id.tv_contrasena);
        final CheckBox cb_Terminos = (CheckBox) this.findViewById(R.id.cb_Terminos);
        final TextView captcha = (TextView)this.findViewById(R.id.tv_captcha);



        if(tv_numeroDocumento.getText().toString().equals("")){
            Toast.makeText(getApplicationContext(), "Ingrese número de documento ", Toast.LENGTH_LONG).show();
            return;
        }

        if(tv_nombres.getText().toString().equals("")){
            Toast.makeText(getApplicationContext(), "Ingrese nombres ", Toast.LENGTH_LONG).show();
            return;
        }

        if(tv_apellidoPaterno.getText().toString().equals("")){
            Toast.makeText(getApplicationContext(), "Ingrese apellido paterno ", Toast.LENGTH_LONG).show();
            return;
        }

        if(tv_apellidoMaterno.getText().toString().equals("")){
            Toast.makeText(getApplicationContext(), "Ingrese apellido materno ", Toast.LENGTH_LONG).show();
            return;
        }

        if(tv_numeroDocumento.getText().toString().equals("")){
            Toast.makeText(getApplicationContext(), "Ingrese número de documento ", Toast.LENGTH_LONG).show();
            return;
        }

        if(tv_correoPrimario.getText().toString().equals("")){
            Toast.makeText(getApplicationContext(), "Ingrese correo principal", Toast.LENGTH_LONG).show();
            return;
        }

        if(tv_numeroTelefono.getText().toString().equals("")){
            Toast.makeText(getApplicationContext(), "Ingrese número teléfono", Toast.LENGTH_LONG).show();
            return;
        }

        if(tv_contrasena.getText().toString().equals("")){
            Toast.makeText(getApplicationContext(), "Ingrese contraseña", Toast.LENGTH_LONG).show();
            return;
        }

        if(!cb_Terminos.isChecked()){
            Toast.makeText(getApplicationContext(), "Acepte terminos y condiciones", Toast.LENGTH_LONG).show();
            return;
        }

        if(!this.captchaValor.equals(captcha.getText().toString())){
            Toast.makeText(getApplicationContext(), "Catpcha incorrecto", Toast.LENGTH_LONG).show();
            return;
        }


        Map<String, String> params = new HashMap<String, String>();
        params.put("tipoDocumento", sp_tipoDocumento.getSelectedItem().toString());
        params.put("numeroDocumento", tv_numeroDocumento.getText().toString());
        params.put("nombres", tv_nombres.getText().toString());
        params.put("apellidoPaterno", tv_apellidoPaterno.getText().toString());
        params.put("apellidoMaterno", tv_apellidoMaterno.getText().toString());
        params.put("correoPrimario", tv_correoPrimario.getText().toString());
        params.put("correoSecundario", tv_correoSecundario.getText().toString());
        params.put("numeroTelefono", tv_numeroTelefono.getText().toString());
        params.put("contrasena", tv_contrasena.getText().toString());

        RequestQueue queue = Volley.newRequestQueue(this);
        String url = Singleton.getInstance().getUrl() + "api/registrarse";
        JSONObject jsonObj = new JSONObject(params);

        JsonObjectRequest jsonObjRequest = new JsonObjectRequest
                (Request.Method.POST, url, jsonObj, new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        try {
                            if (response.getString("estado").equals("OK")) {
                                tv_numeroDocumento.setText("");
                                tv_nombres.setText("");
                                tv_apellidoPaterno.setText("");
                                tv_apellidoMaterno.setText("");
                                tv_correoPrimario.setText("");
                                tv_correoSecundario.setText("");
                                tv_numeroTelefono.setText("");
                                tv_contrasena.setText("");
                                Toast.makeText(getApplicationContext(), "Gracias por registrarse, se envia un correo para confirmación ", Toast.LENGTH_LONG).show();
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