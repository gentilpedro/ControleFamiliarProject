import { Link, Outlet } from "react-router-dom";

export default function Layout() {
  return (
    <div className="layout">
      <aside className="sidebar">
        <div className="sidebar-title">Financeiro</div>

        <nav className="sidebar-nav">
          <Link className="sidebar-link" to="/">Home</Link>
          <Link className="sidebar-link" to="/pessoas">Pessoas</Link>
          <Link className="sidebar-link" to="/categorias">Categorias</Link>
          <Link className="sidebar-link" to="/transacoes">Transações</Link>
          <Link className="sidebar-link" to="/relatorios">Relatórios</Link>
        </nav>
      </aside>

      <main className="page">
        <Outlet />
      </main>
    </div>
  );
}