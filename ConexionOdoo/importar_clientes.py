import os
import xmlrpc.client
import xml.etree.ElementTree as ET
from dotenv import load_dotenv


def importar_clientes_odoo():

    print("=== INICIO IMPORTACIÓN CLIENTES XML → ODOO ===")

    # --------------------------------------------
    # LOCALIZAR RUTA DEL XML
    # --------------------------------------------
    base_path = os.path.dirname(os.path.abspath(__file__))  # /ConexionOdoo/
    root_path = os.path.dirname(base_path)                  # /GenteFit/
    xml_path = os.path.join(root_path, "xml_data", "clientes.xml")

    print(f"[INFO] Archivo XML: {xml_path}")

    if not os.path.exists(xml_path):
        print(f"[ERROR] No existe el archivo XML: {xml_path}")
        return

    # --------------------------------------------
    # VARIABLES DE ENTORNO
    # --------------------------------------------
    load_dotenv()
    url = os.getenv("ODOO_URL")
    db = os.getenv("ODOO_DB")
    username = os.getenv("ODOO_USER")
    password = os.getenv("ODOO_PASS")

    if not all([url, db, username, password]):
        print("[ERROR] Variables de entorno no definidas.")
        return

    # --------------------------------------------
    # AUTENTICACIÓN
    # --------------------------------------------
    try:
        common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
        uid = common.authenticate(db, username, password, {})
    except Exception as e:
        print(f"[ERROR] Fallo de conexión con Odoo: {e}")
        return

    if not uid:
        print("[ERROR] No se pudo autenticar.")
        return

    print(f"[OK] Autenticado como UID={uid}")

    models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")

    # --------------------------------------------
    # LEER XML
    # --------------------------------------------
    try:
        tree = ET.parse(xml_path)
        root = tree.getroot()
    except Exception as e:
        print(f"[ERROR] No se pudo leer el XML: {e}")
        return

    # --------------------------------------------
    # IMPORTAR CLIENTES
    # --------------------------------------------
    contador = 0

    for cliente in root.findall("Cliente"):

        vals = {
            "id_cliente": int(cliente.findtext("Id", "0")),
            "dni": cliente.findtext("Dni", ""),
            "nombre": cliente.findtext("Nombre", ""),
            "apellido1": cliente.findtext("Apellido1", ""),
            "apellido2": cliente.findtext("Apellido2", ""),
            "email": cliente.findtext("Email", ""),
        }

        try:
            models.execute_kw(
                db, uid, password,
                "gente.fit.cliente",
                "create",
                [vals]
            )
            print(f"[OK] Cliente importado: {vals['dni']} ({vals['nombre']})")
            contador += 1

        except Exception as e:
            print(f"[ERROR] No se pudo insertar cliente {vals['dni']}: {e}")

    # --------------------------------------------
    # FIN
    # --------------------------------------------
    print("\n=== IMPORTACIÓN FINALIZADA ===")
    print(f"[OK] Total importados: {contador}")
    print("================================\n")


if __name__ == "__main__":
    importar_clientes_odoo()