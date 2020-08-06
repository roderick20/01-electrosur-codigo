package com.agilecorp.electrosur;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;

public class MainActivity extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_main);
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