import os
import sys
import xmlrpc.client
import xml.etree.ElementTree as ET
from dotenv import load_dotenv

def exportar_clientes():

    print("\n=== INICIANDO EXPORTACIÓN ODOO -> XML ===")

    # -------------------------------------
    # RUTAS (carpeta del ejecutable o script)
    # -------------------------------------
    if getattr(sys, 'frozen', False):
        # Ejecutable (PyInstaller)
        base_path = os.path.dirname(sys.executable)
    else:
        # Script .py
        base_path = os.path.dirname(os.path.abspath(__file__))
    xml_folder = os.path.join(base_path, "xml_data")
    os.makedirs(xml_folder, exist_ok=True)
    xml_path = os.path.join(xml_folder, "clientes.xml")

    print(f"[INFO] Archivo destino: {xml_path}")

    # -------------------------------------
    # VARIABLES ENTORNO
    # -------------------------------------
    load_dotenv()
    url = os.getenv("ODOO_URL")
    db = os.getenv("ODOO_DB")
    username = os.getenv("ODOO_USER")
    password = os.getenv("ODOO_PASS")

    if not all([url, db, username, password]):
        print("[ERROR] Variables de entorno no definidas.")
        return 1

    # -------------------------------------
    # AUTENTICACIÓN
    # -------------------------------------
    try:
        common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
        uid = common.authenticate(db, username, password, {})
    except Exception as e:
        print(f"[ERROR] No se pudo conectar con Odoo: {e}")
        return 1

    if not uid:
        print("[ERROR] No se pudo autenticar.")
        return 1

    print(f"[OK] Autenticado en Odoo con UID={uid}")

    models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")

    # -------------------------------------
    # CONSULTAR CLIENTES
    # -------------------------------------
    fields = ["id_cliente", "dni", "nombre", "apellido1", "apellido2", "email"]

    try:
        clientes = models.execute_kw(
            db, uid, password,
            "gente.fit.cliente", "search_read",
            [[]],
            {"fields": fields}
        )
    except Exception as e:
        print(f"[ERROR] No se pudo consultar clientes: {e}")
        return 1

    print(f"[OK] Clientes recibidos: {len(clientes)}")

    # -------------------------------------
    # GENERAR XML
    # -------------------------------------
    root = ET.Element("Clientes")

    for cliente in clientes:
        elem = ET.SubElement(root, "Cliente")

        # ID → desde id_cliente
        ET.SubElement(elem, "Id").text = str(cliente.get("id_cliente", "") or "")

        # Campos de texto en formato C# (capitalizados)
        ET.SubElement(elem, "Dni").text = cliente.get("dni", "") or ""
        ET.SubElement(elem, "Nombre").text = cliente.get("nombre", "") or ""
        ET.SubElement(elem, "Apellido1").text = cliente.get("apellido1", "") or ""
        ET.SubElement(elem, "Apellido2").text = cliente.get("apellido2", "") or ""
        ET.SubElement(elem, "Email").text = cliente.get("email", "") or ""

    tree = ET.ElementTree(root)
    tree.write(xml_path, encoding="utf-8", xml_declaration=True)

    print("[OK] Exportación completada correctamente.")
    print("==========================================\n")

    return 0


if __name__ == "__main__":
    exportar_clientes()