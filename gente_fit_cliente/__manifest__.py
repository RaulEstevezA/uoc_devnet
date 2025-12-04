# -*- coding: utf-8 -*-
# Copyright (C) 2025 GenteFit (<gentefit@gentefit.com>)
# License AGPL-3.0 or later (https://www.gnu.org/licenses/agpl.html).

{
    'name': 'GenteFit - Clientes',
    'version': '1.0',
    'summary': 'Módulo de integración para clientes desde GenteFit',
    'description': 'Modelo básico de clientes para integración XML → Python → Odoo',
    'author': 'GenteFit',
    'license': 'AGPL-3',
    'depends': ['base'],
    'data': [
	'security/ir.model.access.csv',
        'views/cliente_views.xml',
    ],
    'installable': True,
    'application': True,
}