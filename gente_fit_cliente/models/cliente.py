# -*- coding: utf-8 -*-
# Copyright (C) 2025 GenteFit (<gentefit@gentefit.com>)
# License AGPL-3.0 or later (https://www.gnu.org/licenses/agpl.html).

from odoo import models, fields

class GenteFitCliente(models.Model):
    _name = 'gente.fit.cliente'
    _description = 'Cliente importado desde GenteFit'
    _sql_constraints = [
        ('dni_unique', 'unique(dni)', 'El DNI debe ser único para cada cliente.'),
        ('id_cliente_unique', 'unique(id_cliente)', 'El id_cliente debe ser único para cada cliente externo.'),
    ]

    id_cliente = fields.Integer(string='ID Cliente GenteFit', required=True)
    dni = fields.Char(string='DNI', required=True)
    nombre = fields.Char(string='Nombre', required=True)
    apellido1 = fields.Char(string='Primer Apellido', required=True)
    apellido2 = fields.Char(string='Segundo Apellido')
    email = fields.Char(string='Email')