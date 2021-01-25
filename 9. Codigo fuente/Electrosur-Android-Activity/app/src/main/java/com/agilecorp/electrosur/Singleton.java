package com.agilecorp.electrosur;

import java.io.BufferedInputStream;
import java.io.InputStream;
import java.security.KeyStore;
import java.security.cert.Certificate;
import java.security.cert.CertificateFactory;

import javax.net.ssl.SSLContext;
import javax.net.ssl.SSLSocketFactory;
import javax.net.ssl.TrustManagerFactory;

public class Singleton {

    String Name, Email, UniqueId, Token, Url, CodigoCliente;
    String VisaNet_USERNAME, VisaNet_PASSWORD, VisaNet_MERCHANT,VisaNet_VISANET_ENDPOINT_URL;
    Recibo recibo;
    String descripcion, trasaccion, card, brand, ClaveSecretaMovil;

    String USRCorreoPrimario;
    String UsrtipoDocumento;
    String UsrnumeroDocumento;

    private static final Singleton instance = new Singleton();

    public static Singleton getInstance() {
        return instance;
    }

    public String getClaveSecretaMovil() {
        return ClaveSecretaMovil;
    }

    private Singleton() {
        Url = "https://pruebas.electrosur.com.pe:4333/appapi/";
        //Url = "http://electrosurapi.agilecorp.net.pe/";
        VisaNet_USERNAME = "integraciones.visanet@necomplus.com";
        VisaNet_VISANET_ENDPOINT_URL = "https://apitestenv.vnforapps.com/";
        VisaNet_PASSWORD = "d5e7nk$M";
        VisaNet_MERCHANT = "100128038";
        ClaveSecretaMovil = "zxczc2vz3v215fg17zxczc2vz3v215fg17zxczc2vz3v215fg17zxczc2vz3v215fg17zxczc2vz3v215fg17";
    }

    public String getUrl() {
        return Url;
    }
    public String VisaNetUSERNAME() {
        return VisaNet_USERNAME;
    }
    public String VisaNetPASSWORD() {
        return VisaNet_PASSWORD;
    }
    public String VisaNetMERCHANT() {
        return VisaNet_MERCHANT;
    }
    public String VisaNetVISANETENDPOINTURL() {
        return VisaNet_VISANET_ENDPOINT_URL;
    }

    public void setName(String Name) {
        this.Name = Name;
    }

    public String getName() {
        return Name;
    }

    public void setEmail(String Email) {
        this.Email = Email;
    }

    public String getEmail() {
        return Email;
    }

    public void setUniqueId(String editValue) {
        this.UniqueId = editValue;
    }

    public String getUniqueId() {
        return UniqueId;
    }

    public void setToken(String Token) {
        this.Token = Token;
    }

    public String getToken() {
        return Token;
    }

    public void setRecibo(Recibo recibo) {
        this.recibo = recibo;
    }

    public Recibo getRecibo() {
        return recibo;
    }

    public String getDescripcion() {
        return descripcion;
    }

    public void setDescripcion(String descripcion) {
        this.descripcion = descripcion;
    }

    public String getTrasaccion() {
        return trasaccion;
    }

    public void setTrasaccion(String trasaccion) {
        this.trasaccion = trasaccion;
    }

    public String getCard() {
        return card;
    }

    public void setCard(String card) {
        this.card = card;
    }

    public String getBrand() {
        return brand;
    }

    public void setBrand(String brand) {
        this.brand = brand;
    }

    public String getCodigoCliente() {
        return CodigoCliente;
    }
    public void setCodigoCliente(String CodigoCliente) {
        this.CodigoCliente = CodigoCliente;
    }

    public String getUSRCorreoPrimario() {
        return USRCorreoPrimario;
    }
    public void setUSRCorreoPrimario(String USRCorreoPrimario) {
        this.USRCorreoPrimario = USRCorreoPrimario;
    }

    public String getUsrtipoDocumento() {
        return UsrtipoDocumento;
    }
    public void setUsrtipoDocumento(String UsrtipoDocumento) {
        this.UsrtipoDocumento = UsrtipoDocumento;
    }

    public String getUsrnumeroDocumento() {
        return UsrnumeroDocumento;
    }
    public void setUsrnumeroDocumento(String UsrnumeroDocumento) {
        this.UsrnumeroDocumento = UsrnumeroDocumento;
    }

    public SSLSocketFactory getSSL( java.io.InputStream cert) {

        CertificateFactory cf = null;
        SSLSocketFactory sf = null;
        try {
            cf = CertificateFactory.getInstance("X.509");

            // Generate the certificate using the certificate file under res/raw/cert.cer
            InputStream caInput = new BufferedInputStream(cert);
            Certificate ca = cf.generateCertificate(caInput);
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
            SSLContext context = SSLContext.getInstance("TLS");
            context.init(null, tmf.getTrustManagers(), null);

            sf = context.getSocketFactory();
        }catch (Exception ex)
        {

        }

        return sf;

    }

}
