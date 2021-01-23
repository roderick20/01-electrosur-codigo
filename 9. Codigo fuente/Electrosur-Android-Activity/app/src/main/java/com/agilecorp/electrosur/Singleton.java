package com.agilecorp.electrosur;

public class Singleton {

    String Name, Email, UniqueId, Token, Url, CodigoCliente;

    String VisaNet_USERNAME, VisaNet_PASSWORD, VisaNet_MERCHANT,VisaNet_VISANET_ENDPOINT_URL;

    Recibo recibo;

    String descripcion, trasaccion, card, brand, ClaveSecretaMovil;

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



}
