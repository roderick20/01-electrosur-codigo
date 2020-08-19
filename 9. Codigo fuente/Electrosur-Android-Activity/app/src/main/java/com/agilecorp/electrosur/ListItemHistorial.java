package com.agilecorp.electrosur;

import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseAdapter;
import android.widget.TextView;

import java.util.ArrayList;

public class ListItemHistorial extends BaseAdapter {
    private Context context;
    private int layout;
    private ArrayList<Recibo> recibos;

    public ListItemHistorial(Context context, int layout, ArrayList<Recibo> names){
        this.context = context;
        this.layout = layout;
        this.recibos = names;
    }

    @Override
    public int getCount() {
        return this.recibos.size();
    }

    @Override
    public Object getItem(int position) {
        return this.recibos.get(position);
    }

    @Override
    public long getItemId(int id) {
        return id;
    }

    @Override
    public View getView(int position, View convertView, ViewGroup viewGroup) {

        View v = convertView;
        LayoutInflater layoutInflater = LayoutInflater.from(this.context);
        v= layoutInflater.inflate(R.layout.list_item_historial, null);

        Recibo recibo  = recibos.get(position);

        TextView tvSuministro = (TextView) v.findViewById(R.id.tvSuministro);
        tvSuministro.setText(recibo.getCodigoSuministro());

        TextView tv_CodigoComprobante = (TextView) v.findViewById(R.id.tv_CodigoComprobante);
        tv_CodigoComprobante.setText(recibo.getCodigoComprobante());

        TextView tv_tarjeta = (TextView) v.findViewById(R.id.tv_tarjeta);
        tv_tarjeta.setText(recibo.getPagcard());

        TextView tvFechaVencimiento = (TextView) v.findViewById(R.id.tvFechaVencimiento);
        String fecha = recibo.getPagcreado().substring(0,10) + "  " + recibo.getPagcreado().substring(11,19);
        tvFechaVencimiento.setText(fecha);

        TextView tvMonto = (TextView) v.findViewById(R.id.tvMonto);
        tvMonto.setText(recibo.getMontoAPagarConsulta());

        TextView tv_descripcion = (TextView) v.findViewById(R.id.tv_descripcion);
        tv_descripcion.setText(recibo.getPagdescription());

        return v;
    }
}
