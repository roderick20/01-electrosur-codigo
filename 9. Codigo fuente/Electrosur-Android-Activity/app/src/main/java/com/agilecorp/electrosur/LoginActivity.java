package com.agilecorp.electrosur;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Intent;
import android.os.Bundle;
import android.view.View;
import android.widget.ImageView;
import android.widget.TextView;
import android.widget.Toast;

import com.android.volley.Request;
import com.android.volley.RequestQueue;
import com.android.volley.Response;
import com.android.volley.VolleyError;
import com.android.volley.toolbox.HurlStack;
import com.android.volley.toolbox.JsonObjectRequest;
import com.android.volley.toolbox.Volley;

import org.json.JSONException;
import org.json.JSONObject;

import java.io.BufferedInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.net.HttpURLConnection;
import java.net.URL;
import java.security.KeyManagementException;
import java.security.KeyStore;
import java.security.KeyStoreException;
import java.security.NoSuchAlgorithmException;
import java.security.cert.Certificate;
import java.security.cert.CertificateFactory;
import java.security.cert.X509Certificate;
import java.util.HashMap;
import java.util.Map;

import javax.net.ssl.HostnameVerifier;
import javax.net.ssl.HttpsURLConnection;
import javax.net.ssl.SSLContext;
import javax.net.ssl.SSLSession;
import javax.net.ssl.SSLSocketFactory;
import javax.net.ssl.TrustManagerFactory;
import javax.security.cert.CertificateException;

import lib.visanet.com.pe.visanetlib.util.TLSSocketFactory;

public class LoginActivity extends AppCompatActivity {

    String captchaValor="";

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_login);

        TextView email = (TextView)this.findViewById(R.id.tv_email);
        email.setText("roderick20@hotmail.com");
        TextView password = (TextView)this.findViewById(R.id.tv_password);
        password.setText("7MX3");

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

    /*private SSLSocketFactory pinnedSSLSocketFactory() {
        try {
            return new TLSSocketFactory(PUBLIC_KEY);
        } catch (KeyManagementException e) {
            e.printStackTrace();
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        }
        return null;
    }*/

    public void login(View view) {
        final Intent intent = new Intent(this, MainActivity.class);

        final TextView email = (TextView)this.findViewById(R.id.tv_email);
        final TextView password = (TextView)this.findViewById(R.id.tv_password);
        final TextView captcha = (TextView)this.findViewById(R.id.tv_captcha);

        if(email.getText().toString().equals("")){
            Toast.makeText(getApplicationContext(), "Ingrese email", Toast.LENGTH_LONG).show();
            return;
        }

        if(password.getText().toString().equals("")){
            Toast.makeText(getApplicationContext(), "Ingrese password", Toast.LENGTH_LONG).show();
            return;
        }

        if(!this.captchaValor.equals(captcha.getText().toString())){
            Toast.makeText(getApplicationContext(), "Catpcha incorrecto", Toast.LENGTH_LONG).show();
            return;
        }


        /*RequestQueue queue = null;
        try {
            queue = getPinnedRequestQueue();
        } catch (CertificateException e) {
            e.printStackTrace();
        } catch (IOException e) {
            e.printStackTrace();
        } catch (NoSuchAlgorithmException e) {
            e.printStackTrace();
        } catch (KeyStoreException e) {
            e.printStackTrace();
        } catch (KeyManagementException e) {
            e.printStackTrace();
        } catch (java.security.cert.CertificateException e) {
            e.printStackTrace();
        }*/
        RequestQueue queue = Volley.newRequestQueue(this);
        String url = Singleton.getInstance().getUrl()+"api/login";

        JSONObject jsonObject = new JSONObject();
        String jsonString = "";

        Crypto cryto = new Crypto();
        try {
            jsonObject.put("UserName", email.getText().toString());
            jsonObject.put("Password", password.getText().toString());
            jsonString = jsonObject.toString();
            jsonString = cryto.encrypt(jsonString, Singleton.getInstance().getClaveSecretaMovil());
        } catch (JSONException e) {
            e.printStackTrace();
        }

        Map<String, String> params = new HashMap<String, String>();
        params.put("d", jsonString);

        Singleton.getInstance().setEmail(email.getText().toString());

        JSONObject jsonObj = new JSONObject(params);
        JsonObjectRequest jsonObjRequest = new JsonObjectRequest
                (Request.Method.POST, url, jsonObj, new Response.Listener<JSONObject>() {
                    @Override
                    public void onResponse(JSONObject response) {
                        try {

                            if(response.getString("estado").equals("OK")){
                                Crypto cryto = new Crypto();
                                String jsonString = cryto.decrypt(response.getString("d"), Singleton.getInstance().getClaveSecretaMovil());
                                JSONObject jsonObject = new JSONObject(jsonString);

                                Singleton.getInstance().setToken(jsonObject.getString("Token"));
                                Singleton.getInstance().setName(jsonObject.getString("USRNombre"));
                                Singleton.getInstance().setUniqueId(jsonObject.getString("UsruniqueId"));

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

    private RequestQueue getPinnedRequestQueue() throws CertificateException, IOException, NoSuchAlgorithmException,
            KeyStoreException, KeyManagementException, java.security.cert.CertificateException {

        CertificateFactory cf = CertificateFactory.getInstance("X.509");

        // Generate the certificate using the certificate file under res/raw/cert.cer
        InputStream caInput = new BufferedInputStream(getApplicationContext().getResources().openRawResource(R.raw.cert_app_prueba));
        final Certificate ca = cf.generateCertificate(caInput);
        caInput.close();

        // Create a KeyStore containing our trusted CAs
        String keyStoreType = KeyStore.getDefaultType();
        KeyStore trusted = KeyStore.getInstance(keyStoreType);
        trusted.load(null, null);
        trusted.setCertificateEntry("ca", ca);

        // Create a TrustManager that trusts the CAs in our KeyStore
        String tmfAlgorithm = TrustManagerFactory.getDefaultAlgorithm();
        TrustManagerFactory tmf = TrustManagerFactory.getInstance(tmfAlgorithm);
        tmf.init(trusted);

        // Create an SSLContext that uses our TrustManager
        SSLContext sslContext = SSLContext.getInstance("TLSV1.2");
        sslContext.init(null, tmf.getTrustManagers(), null);

        SSLSocketFactory sf = sslContext.getSocketFactory();

        HurlStack hurlStack = new HurlStack(null, sf) {
            @Override
            protected HttpURLConnection createConnection(URL url) throws IOException {
                //LogUtil.info(TAG, "Before createConnection");
                HttpsURLConnection httpsURLConnection = (HttpsURLConnection) super.createConnection(url);
                //LogUtil.info(TAG, "After createConnection");
                httpsURLConnection.setHostnameVerifier(new HostnameVerifier() {

                    //@DebugLog
                    @Override
                    public boolean verify(String hostName, SSLSession sslSession) {
                        String certificateDomainName = ((X509Certificate) ca).getSubjectDN().toString();
                        //LogUtil.info(TAG, "Index : " + certificateDomainName.indexOf("CN=") + " Len : " + certificateDomainName.codePointCount(certificateDomainName.indexOf("CN="), certificateDomainName.indexOf(",")));
                        String certificateName = certificateDomainName.substring(certificateDomainName.indexOf("CN="), certificateDomainName.codePointCount(certificateDomainName.indexOf("CN="), certificateDomainName.indexOf(",")));
                        certificateName = certificateName.replace("CN=", "");
                        //LogUtil.info(TAG, "hostName : " + hostName + " certificateName : " + certificateName);
                        if (certificateName.isEmpty())
                            return false;
                        return certificateName.equals(hostName);
                    }
                });
                return httpsURLConnection;
            }
        };

        return new Volley().newRequestQueue(this, hurlStack);
    }


    public void registro(View view) {
        final Intent intent = new Intent(this, RegistroActivity.class);
        startActivity(intent);
    }

    public void recuperarContrasena(View view) {
        final Intent intent = new Intent(this, RecuperarContrasenaActivity.class);
        startActivity(intent);
    }


}