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
                        "\n 1. Al hacer uso de esta plataforma, el usuario acepta y por lo tanto autoriza que la información sea guardada en nuestros servidores, información la cual será únicamente para el uso que el usuario de esta plataforma requiera de manera expresa, ya sea por medio de esta u otros canales de atención." +
                        "\n 2. En caso de que el usuario desee eliminar su información deberá de comunicarlo de manera escrita y por mesa de partes de ELECTROSUR." +
                        "\n 3. Para cualquier consulta adicional por favor llame nuestra Central Telefónica de FONOSUR 52-583316(Tacna), 53-584161 (Moquegua), 53-584162 (Ilo). " +
                        "\n\n ACERCA DE LOS MEDIOS DE PAGO." +
                        "\n 4. Actualmente solo se permite el pago por medio de tarjetas de crédito y/o debito de marca VISA, MasterCard." +
                        "\n 5. En caso de extornos, estos deberán ser comunicados el mismo día de efectuada la operación, dentro del horario de Atención al Cliente de ELECTROSUR. No se aceptan extornos posteriores a la fecha de pago." +
                        "\n 6. En caso de requerir otros medios de pago, por favor llamar a la Central Telefónica de FONOSUR 52-583316(Tacna), 53-584161 (Moquegua), 53-584162 (Ilo)." +
                        "\n 7. El cliente no asumirá ningún costo de comisión del servicio por parte de ELECTROSUR." +
                        "\n 8. El cliente puede presentar reclamo por el servicio de acuerdo al procedimiento de reclamos de ELECTROSUR." +
                        "\n 9. El mecanismo de pago a través del uso de tarjetas de crédito/débito es facultativo a elección del cliente." +
                        "\n 10. El uso, las condiciones de pago y otras condiciones aplicables a las tarjetas de crédito, son de exclusiva responsabilidad de la entidad financiera emisora de su tarjeta." +
                        "\n\n DELIMITACION DE RESPONSABILIDADES." +
                        "\n 11. ELECTROSUR S.A. no se hará responsable por daños y perjuicios frente a los usuarios como consecuencia directa o indirecta de la interrupción, suspensión o finalización de los servicios ofrecidos a través de esta pasarela de pagos, siendo esto responsabilidad de la entidad financiera emisora de la tarjeta."+
                                "\n 12. ELECTROSUR S.A., no se hará responsable por los errores que cometa el usuario en el registro de los datos solicitados para el pago de los servicios en esta plataforma virtual, asimismo, antes de proceder con el pago, el usuario tiene el deber de verificar que toda la información registrada sea la correcta."+
                                "\n\n CONSULTAS Y/O RECLAMOS."+
                                "\n 13. Para cualquier consulta relacionada a la operatividad o uso de esta pasarela de pagos, deberá hacerla llegar a nuestra Central Telefónica de FONOSUR 52-583316(Tacna), 53-584161 (Moquegua), 53-584162 (Ilo)."+
                                "\n 14. Los reclamos y/o quejas, relacionadas al no reconocimiento del trámite o errores en los datos consignados para el pago, deberán ser presentados por el trámite correspondiente en Mesa de Partes física o virtual de ELECTROSUR."+
                                "\n 15. Los reclamos relacionados al rechazo o denegatoria de una transacción efectuada o al no reconocimiento de los pagos en línea con tarjetas de crédito/débito, deberán ser presentadas por los usuarios titulares ante las entidades financieras emisoras de estas tarjetas de crédito/débito en las oficinas de atención correspondientes. En conclusión, de encontrarse el usuario (titular de la tarjeta) ante una contingencia respecto al pago efectuado en el Aplicativo web o app móvil para pagos con tarjeta VISA de recibos de energía, deberá presentar su reclamo en la empresa del sistema financiero, indicando las transacciones que no reconoce en su tarjeta, debiendo proporcionar la información que la empresa del sistema financiero le solicite y seguir el procedimiento que ésta le indique. Finalmente, y a mayor abundamiento, los titulares de la tarjeta no asumirán responsabilidad por las transacciones no autorizadas que se hayan realizado con posterioridad a la comunicación del robo o extravío. Sin embargo, las que se realicen con anterioridad a dicha comunicación, serán de responsabilidad exclusiva de los titulares o usuarios."+
                                "\n\n GENERALES."+
                                "\n 16. Estas condiciones pueden cambiar sin previo aviso y estarán publicadas en la página web institucional de ELECTROSUR."                        )
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