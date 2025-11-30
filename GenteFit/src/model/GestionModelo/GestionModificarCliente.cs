using GenteFit.src.DAO;
using GenteFit.src.model.entity;

namespace GenteFit.src.model.GestionModelo
{
	public static class GestionModificarCliente
	{
		public static void ModificarCliente(Cliente cliente)
		{
			var clienteDao = FactoryDAO.GetClienteDAO();
			clienteDao.Update(cliente);
		}
	}
}
