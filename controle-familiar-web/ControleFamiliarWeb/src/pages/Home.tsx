import { Link } from "react-router-dom";

export default function Home() {

  return (

    <>
      <div className="page-header">

        <div>
          <h1 className="page-title">Controle Financeiro Familiar</h1>

          <p className="page-subtitle">
            Gerencie receitas, despesas e acompanhe o desempenho financeiro da família.
          </p>
        </div>

      </div>


      {/* ATALHOS */}
      <div className="summary-grid">

        <Link to="/pessoas" className="summary-card">

          <div className="summary-label">
            Pessoas
          </div>

          <div className="summary-value saldo">
            👥
          </div>

        </Link>


        <Link to="/categorias" className="summary-card">

          <div className="summary-label">
            Categorias
          </div>

          <div className="summary-value saldo">
            🏷️
          </div>

        </Link>


        <Link to="/transacoes" className="summary-card">

          <div className="summary-label">
            Transações
          </div>

          <div className="summary-value saldo">
            💰
          </div>

        </Link>

      </div>


      {/* INFORMAÇÕES */}
      <div className="card">

        <h2 className="section-title">
          Sobre o sistema
        </h2>

        <p className="page-subtitle">

          Este sistema permite registrar receitas e despesas vinculadas às pessoas da família.
          Através dos relatórios é possível visualizar gráficos e acompanhar a evolução financeira.

        </p>

        <br/>

        <p className="page-subtitle">

          Utilize o menu lateral para acessar as funcionalidades principais do sistema.

        </p>

      </div>

    </>

  );

}