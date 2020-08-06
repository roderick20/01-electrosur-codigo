package com.agilecorp.electrosur;


import android.app.Dialog;
import android.content.DialogInterface;
import android.os.Bundle;
import androidx.appcompat.app.AlertDialog;
import androidx.fragment.app.DialogFragment;


public class TerminoCondicionesDialog extends DialogFragment {

    public TerminoCondicionesDialog() {
    }

    @Override
    public Dialog onCreateDialog(Bundle savedInstanceState) {
        return createSimpleDialog();
    }

    public AlertDialog createSimpleDialog() {
        AlertDialog.Builder builder = new AlertDialog.Builder(getActivity());

        builder.setTitle("TÉRMINOS Y CONDICIONES")
                .setMessage("USO DE LA PLATAFORMA WEB Y APP PARA COBRANZA DE RECIBOS MEDIANTE TARJETAS DE CRÉDITO/DEBITO" +
                        "\n\n ACERCA DEL USO DE LA PLATAFORMA." +
                        "\n 1. Al  hacer  uso  de  esta  plataforma,  el  usuario  acepta  y  por  lo  tanto  autoriza  que  la información sea guardada en nuestros servidores, información la cual será únicamente para  el  uso  que  el  usuario  de estaplataforma  requiera  de  manera  expresa,  ya  sea  por medio de esta uotros canales de atención." +
                        "\n 2. En  caso  que  el  usuario  desee  eliminar  su  información  deberá  de  comunicarlo  de manera escrita y por mesa de partes." +
                        "\n 3. Para  cualquier  consulta  adicional  por  favor  llame  anuestra  Central  Telefónica  de FONOSUR 52-583316." +
                        "\n\n ACERCA DE LOS MEDIOS DE PAGO." +
                        "\n 4. Actualmente solo se permite el pago por medio de tarjetas de crédito y/o debito de marca VISA, MasterCard." +
                        "\n 5.  En  caso  de  extornos,  estos  deberán  ser  comunicados  el  mismo  día  de  efectuada  la operación,  dentro  del horario  de  Atención  al  Cliente  del  Comercio.  No  se  aceptan extornos posteriores a la fecha de pago." +
                        "\n 6. En caso de requerir otros medios de pago, por favor llamar al Servicio de Cobranza al 52-583316." +
                        "\n 7.  El  cliente  podrá  cancelar  la  deuda  del  mes  anterior  que  aún  no  haya  vencido.  Al momento del pago, el sistema mostrarálas facturaciones que puede cancelar." +
                        "\n 8. El cliente no asumirá ningún costo de comisión del servicio por parte del Comercio." +
                        "\n 9. El  cliente  puede  presentar  reclamo  por  el  servicio  de  acuerdo  al  procedimiento  de reclamos del Comercio." +
                        "\n 10. El mecanismo de pago a través del uso de tarjetas de crédito/débito es facultativo aelección del cliente" +
                        "\n\n GENERALES." +
                        "\n 11. Estas condiciones pueden cambiar sinprevio aviso y estarán publicadas en la página web institucional.")
                .setPositiveButton("Cerrar",
                        new DialogInterface.OnClickListener() {
                            @Override
                            public void onClick(DialogInterface dialog, int which) {
                                // Acciones
                            }
                        });

        return builder.create();
    }
}