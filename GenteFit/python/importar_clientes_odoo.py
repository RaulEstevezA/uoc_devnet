import os
import xmlrpc.client
import xml.etree.ElementTree as ET

# -------------------------------------------------
# LOCALIZAR LA RUTA REAL DEL XML EXPORTADO POR .NET
# -------------------------------------------------
base_path = os.path.dirname(os.path.abspath(__file__))      # /GenteFit/python/
root_path = os.path.dirname(base_path)                      # /GenteFit/
xml_path = os.path.join(root_path, "xml_data", "clientes.xml")

print(f"Usando archivo XML en: {xml_path}")


# CONFIGURACIÓN ODOO
url = "http://localhost:8069"
db = "gente_fit_db"             
username = "admin@example.com"
password = "admin"              

# Autenticación XML-RPC
common = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/common")
uid = common.authenticate(db, username, password, {})

if not uid:
    raise Exception("Error al autenticar con Odoo. Revisa las credenciales.")

models = xmlrpc.client.ServerProxy(f"{url}/xmlrpc/2/object")


# LEER XML EXPORTADO POR .NET
if not os.path.exists(xml_path):
    raise FileNotFoundError(f"ERROR: No existe el archivo XML: {xml_path}")

tree = ET.parse(xml_path)
root = tree.getroot()


# IMPORTAR CLIENTES A ODOO
contador = 0

for cliente in root.findall("cliente"):

    vals = {
        "dni": cliente.find("dni").text,
        "nombre": cliente.find("nombre").text,
        "apellido1": cliente.find("apellido1").text,
        "apellido2": (cliente.find("apellido2").text 
                      if cliente.find("apellido2") is not None else ""),
        "email": cliente.find("email").text,
        "activo": cliente.find("activo").text.lower() == "true",
    }

    models.execute_kw(
        db, uid, password,
        "gente.fit.cliente",
        "create",
        [vals]
    )

    contador += 1

print(f"Importación completada: {contador} clientes cargados en Odoo.")
