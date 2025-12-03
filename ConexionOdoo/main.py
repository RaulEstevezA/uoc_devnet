import sys
from importar_clientes import importar_clientes_odoo
from exportar_clientes import exportar_clientes


def main():
    """
    Punto de entrada unificado.
    Recibe un argumento que puede ser:
        - importar
        - exportar
    Ejecuta el flujo correspondiente.
    """

    # -----------------------------------
    # Validar argumento
    # -----------------------------------
    if len(sys.argv) < 2:
        print("[ERROR] Debes indicar una acción: importar | exportar")
        print("Uso: python main.py <accion>")
        sys.exit(1)

    accion = sys.argv[1].lower()

    # -----------------------------------
    # IMPORTAR: XML -> Odoo
    # -----------------------------------
    if accion == "importar":
        print("\n=== IMPORTANDO CLIENTES XML → ODOO ===")

        try:
            importar_clientes_odoo()
            print("\n[OK] Importación de clientes finalizada correctamente.")
            sys.exit(0)

        except Exception as e:
            print(f"[ERROR] Fallo durante importación: {e}")
            sys.exit(1)

    # -----------------------------------
    # EXPORTAR: Odoo -> XML
    # -----------------------------------
    elif accion == "exportar":
        print("\n=== EXPORTANDO CLIENTES ODOO → XML ===")

        try:
            exportar_clientes()
            print("\n[OK] Exportación de clientes finalizada correctamente.")
            sys.exit(0)

        except Exception as e:
            print(f"[ERROR] Fallo durante exportación: {e}")
            sys.exit(1)

    # -----------------------------------
    # Acción inválida
    # -----------------------------------
    else:
        print(f"[ERROR] Acción desconocida: {accion}")
        print("Opciones válidas: importar | exportar")
        sys.exit(1)


if __name__ == "__main__":
    main()