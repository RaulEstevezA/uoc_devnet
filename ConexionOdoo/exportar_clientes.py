import os
import xmlrpc.client
import xml.etree.ElementTree as ET
from dotenv import load_dotenv

# -------------------------------
# RUTA DE SALIDA DEL XML
# -------------------------------
base_path = os.path.dirname(os.path.abspath(__file__))      # /GenteFit/python/
root_path = os.path.dirname(base_path)                      # /GenteFit/
xml_folder = os.path.join(root_path, "xml_data")
os.makedirs(xml_folder, exist_ok=True)
xml_path = os.path.join(xml_folder, "clientes.xml")

print(f"Exportando clientes a: {xml_path}")

# CONFIGURACIóN ODOO
load_dotenv()
url = os.getenv("ODOO_URL")
db = os.getenv("ODOO_DB")
username = os.getenv("ODOO_USER")
password = os.getenv("ODOO_PASS")

# Autenticaci�n XML-RPC
common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
uid = common.authenticate(db, username, password, {})

if not uid:
    raise Exception("Error al autenticar con Odoo. Revisa las credenciales.")

models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")

# CONSULTAR CLIENTES EN ODOO
fields = ["Id", "dni", "nombre", "apellido1", "apellido2", "email"]

# Leer todos los clientes
clientes = models.execute_kw(
    db, uid, password,
    "gente.fit.cliente", "search_read",
    [[]],  # Sin filtro, todos los clientes
    {"fields": fields}
)

print(f"Clientes encontrados: {len(clientes)}")

# CREAR XML
root = ET.Element("clientes")

for cliente in clientes:
    elem = ET.SubElement(root, "cliente")
    for campo in fields:
        sub = ET.SubElement(elem, campo)
        sub.text = str(cliente.get(campo, "") if cliente.get(campo) is not None else "")

tree = ET.ElementTree(root)
tree.write(xml_path, encoding="utf-8", xml_declaration=True)

print(f"Exportaci�n completada: {len(clientes)} clientes exportados a {xml_path}")