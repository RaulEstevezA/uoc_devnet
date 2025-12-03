import os
import xmlrpc.client
import xml.etree.ElementTree as ET
from dotenv import load_dotenv


def importar_clientes_odoo():

    print("=== INICIO IMPORTACIÓN CLIENTES XML → ODOO ===")

  
    # LOCALIZAR RUTA DEL XML
    base_path = os.path.dirname(os.path.abspath(__file__))      # /GenteFit/python/
    root_path = os.path.dirname(base_path)                      # /GenteFit/
    xml_path = os.path.join(root_path, "xml_data", "clientes.xml")

    print(f"[INFO] Archivo XML: {xml_path}")

    if not os.path.exists(xml_path):
        print(f"[ERROR] No existe el archivo XML: {xml_path}")
        return


    # CONFIGURACIÓN ODOO
    load_dotenv()
    url = os.getenv("ODOO_URL")
    db = os.getenv("ODOO_DB")
    username = os.getenv("ODOO_USER")
    password = os.getenv("ODOO_PASS")

    if not all([url, db, username, password]):
        print("[ERROR] Variables de entorno ODOO_URL ODOO_DB ODOO_USER ODOO_PASS no definidas.")
        return


    # AUTENTICACIÓN XML-RPC
    try:
        common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
        uid = common.authenticate(db, username, password, {})
    except Exception as e:
        print(f"[ERROR] Fallo de conexión con Odoo: {e}")
        return

    if not uid:
        print("[ERROR] No se pudo autenticar con Odoo.")
        return

    print(f"[OK] Autenticado en Odoo como UID={uid}")

    models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")


    # LEER XML
    try:
        tree = ET.parse(xml_path)
        root = tree.getroot()
    except Exception as e:
        print(f"[ERROR] No se pudo leer el XML: {e}")
        return


    # IMPORTAR CLIENTES
    contador = 0

    for cliente in root.findall("Cliente"):

        try:
            id_cliente = int(cliente.find("Id").text or 0)
            dni = cliente.find("Dni").text or ""
            nombre = cliente.find("Nombre").text or ""
            apellido1 = cliente.find("Apellido1").text or ""
            apellido2 = cliente.find("Apellido2").text if cliente.find("Apellido2") is not None else ""
            email = cliente.find("Email").text if cliente.find("Email") is not None else ""

        except Exception as e:
            print(f"[WARN] Error leyendo nodo XML: {e}")
            continue

        vals = {
            "id_cliente": id_cliente,
            "dni": dni,
            "nombre": nombre,
            "apellido1": apellido1,
            "apellido2": apellido2,
            "email": email,
        }

        try:
            models.execute_kw(
                db, uid, password,
                "gente.fit.cliente",
                "create",
                [vals]
            )
            print(f"[OK] Cliente importado: {dni} ({nombre})")
            contador += 1

        except Exception as e:
            print(f"[ERROR] No se pudo insertar cliente {dni}: {e}")

    print("\n=== IMPORTACIÓN FINALIZADA ===")
    print(f"[OK] Total importados: {contador}")
    print("================================\n")


if __name__ == "__main__":
    importar_clientes_odoo()